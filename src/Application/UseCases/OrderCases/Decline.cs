using Microsoft.VisualBasic;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Enums;
using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Infrastructure.DBContext;

namespace UsersApp.src.Application.UseCases.OrderCases
{
    public class DeclineOrderUseCase(IOrderRepository orderRepository, ITransactionRepository transactionRepository, IProductRepository productRepository, ApplicationDbContext context)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private ITransactionRepository _transactionRepository = transactionRepository;
        private IProductRepository _productRepository = productRepository;
        private ApplicationDbContext _context = context;
        public async Task<Order> Execute(int id)
        {
            await using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = await _orderRepository.GetOneAsync(id);
                if (order == null) throw new KeyNotFoundException($"Order with such id {id} was not found");
                if (order.Status != OrderStatus.Initiated) throw new BadHttpRequestException("Order in such a status cannot be declined.");
                order.Status = OrderStatus.Declined;

                var transaction = await _transactionRepository.GetPaidByOrderIdAsync(id);
                if (transaction == null) throw new Exception($"Transaction with such id {id} was not found");
                var refundedTransaction = new Transaction
                {
                    Status = TransactionStatus.Refunded,
                    OrderId = order.OrderId,
                    Order = order,
                    Total = transaction.Total,
                };

                foreach (var orderItem in order.OrderItems)
                {
                    var product = await _productRepository.GetOneAsync(orderItem.ProductId);
                    if (product == null) throw new KeyNotFoundException($"Product {orderItem.ProductId} in such order id {id} was not found");

                    product.StockAmount += orderItem.Quantity;

                    await _productRepository.UpdateAsync(product);
                }

                await _transactionRepository.Create(refundedTransaction);
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
    }
}