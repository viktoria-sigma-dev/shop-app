using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;
using ShopApp.Domain.ValueObjects;

namespace ShopApp.Application.UseCases.OrderCases
{
    public class CreateOrderUseCase(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<OrderResponseDTO> Execute(CreateOrderDTO orderDto)
        {
            await unitOfWork.BeginTransactionAsync();
            try
            {
                var user = await userRepository.GetOneAsync(orderDto.UserId);
                if (user is null) throw new UserNotFoundException(orderDto.UserId);

                var products = await productRepository.GetAllAsync(orderDto.OrderItems.Select(oi => oi.ProductId).ToList());
                var orderItems = orderDto.OrderItems
                    .Select(oi => new OrderItemRequest(oi.ProductId, oi.Quantity))
                    .ToList();

                var order = Order.Create(
                    user,
                    products,
                    orderItems
                );

                await orderRepository.CreateOrder(order);

                await unitOfWork.CommitAsync();

                return mapper.Map<OrderResponseDTO>(order);
            }
            catch
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}