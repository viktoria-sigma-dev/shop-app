using UsersApp.src.Application.DTOs.OrderDTOs;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Enums;
using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Infrastructure.DBContext;

namespace UsersApp.src.Application.UseCases.OrderCases
{
    public class CreateOrderUseCase(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository, ITransactionRepository transactionRepository, IUserRepository userRepository, ApplicationDbContext context)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private IOrderItemRepository _orderItemRepository = orderItemRepository;
        private IProductRepository _productRepository = productRepository;
        private IUserRepository _userRepository = userRepository;
        private ITransactionRepository _transactionRepository = transactionRepository;
        private ApplicationDbContext _context = context;
        public async Task<Order> Execute(CreateOrderDTO orderDto)
        {
            decimal total = 0;
            await using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = await CreateOrder(orderDto);
                foreach (var orderItemDto in orderDto.OrderItems)
                {
                    var updatedProduct = await UpdateProductOrder(orderItemDto);
                    var orderItem = await CreateOrderItem(order, orderItemDto, updatedProduct);
                    order.OrderItems.Add(orderItem);
                    total += updatedProduct.GetTotal(orderItemDto.Quantity);
                }

                var transaction = new Transaction
                {
                    OrderId = order.OrderId,
                    Order = order,
                    Status = TransactionStatus.Paid,
                    Total = total,
                };
                await _transactionRepository.Create(transaction);
                order.Transactions.Add(transaction);

                await _orderRepository.UpdateOrder(order);
                await dbTransaction.CommitAsync();

                return order;
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }
        private async Task<Order> CreateOrder(CreateOrderDTO orderDto)
        {
            var user = await _userRepository.GetOneAsync(orderDto.UserId);
            if (user == null) throw new KeyNotFoundException($"User with such id {orderDto.UserId} was not found");
            var order = new Order
            {
                UserId = user.UserId,
                User = user,
                Status = OrderStatus.Initiated,
            };
            return await _orderRepository.CreateOrder(order);
        }
        private async Task<Product> UpdateProductOrder(CreateOrderItemDTO orderItemDto)
        {
            var product = await _productRepository.GetOneAsync(orderItemDto.ProductId);
            if (product == null) throw new KeyNotFoundException($"Product with id {orderItemDto.ProductId} not found");
            if (product.StockAmount < orderItemDto.Quantity) throw new BadHttpRequestException($"Only {product.StockAmount} products left in the stock");
            product.StockAmount -= orderItemDto.Quantity;
            return await _productRepository.UpdateAsync(product);
        }


        private async Task<OrderItem> CreateOrderItem(Order order, CreateOrderItemDTO orderItemDto, Product product)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.OrderId,
                Order = order,
                ProductId = orderItemDto.ProductId,
                Product = product,
                Quantity = orderItemDto.Quantity,
            };
            return await _orderItemRepository.Create(orderItem);
        }
    }
}