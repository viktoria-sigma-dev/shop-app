using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetOrdersUserUseCase(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
    {
        public IOrderRepository _orderRepository = orderRepository;
        public IUserRepository _userRepository = userRepository;
        public IMapper _mapper = mapper;
        public async Task<List<OrderResponseDTO>> Execute(int userId)
        {
            var user = await _userRepository.GetOneAsync(userId);
            if (user == null) throw new UserNotFoundException(userId);
            var orders = await _orderRepository.GetAllAsync(userId);

            return _mapper.Map<List<OrderResponseDTO>>(orders);
        }
    }
}