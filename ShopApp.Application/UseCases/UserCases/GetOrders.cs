using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetOrdersUserUseCase(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
    {
        public async Task<List<OrderResponseDTO>> Execute(int userId)
        {
            var user = await userRepository.GetOneAsync(userId);
            if (user == null) throw new UserNotFoundException(userId);
            var orders = await orderRepository.GetAllAsync(userId);

            return mapper.Map<List<OrderResponseDTO>>(orders);
        }
    }
}