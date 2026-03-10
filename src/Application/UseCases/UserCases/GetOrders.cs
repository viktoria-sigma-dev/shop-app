using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.UserCases
{
    public class GetOrdersUserUseCase(IOrderRepository orderRepository, IUserRepository userRepository)
    {
        public IOrderRepository _orderRepository = orderRepository;
        public IUserRepository _userRepository = userRepository;
        public async Task<List<Order>> Execute(int userId)
        {
            var user = await _userRepository.GetOneAsync(userId);
            if (user == null) throw new KeyNotFoundException($"User with such id {userId} was not found");
            return await _orderRepository.GetAllAsync(userId);
        }
    }
}