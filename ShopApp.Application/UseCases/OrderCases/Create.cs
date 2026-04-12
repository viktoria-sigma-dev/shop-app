using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;
using ShopApp.Domain.ValueObjects;

namespace ShopApp.Application.UseCases.OrderCases
{
    public class CreateOrderUseCase(IOrderRepository orderRepository, IProductRepository productRepository, ITransactionRepository transactionRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private IProductRepository _productRepository = productRepository;
        private IUserRepository _userRepository = userRepository;
        private ITransactionRepository _transactionRepository = transactionRepository;
        private IUnitOfWork _unitOfWork = unitOfWork;
        private IMapper _mapper = mapper;
        public async Task<OrderResponseDTO> Execute(CreateOrderDTO orderDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var user = await _userRepository.GetOneAsync(orderDto.UserId);
                if (user is null) throw new UserNotFoundException(orderDto.UserId);

                var products = await _productRepository.GetAllAsync(orderDto.OrderItems.Select(oi => oi.ProductId).ToList());
                var orderItems = orderDto.OrderItems
                    .Select(oi => new OrderItemRequest(oi.ProductId, oi.Quantity))
                    .ToList();

                var order = Order.Create(
                    user,
                    products,
                    orderItems
                );

                await _orderRepository.CreateOrder(order);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<OrderResponseDTO>(order);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}