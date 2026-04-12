using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopApp.Application.DTOs.UserDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class TopUpBalanceUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        public IUserRepository _userRepository = userRepository;
        public IMapper _mapper = mapper;
        public async Task<UserResponseDTO> Execute(int userId, decimal amount)
        {
            var user = await _userRepository.GetOneAsync(userId);

            if (user == null) throw new UserNotFoundException(userId);

            user.IncreaseBalance(amount);

            var updatedUser = await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserResponseDTO>(updatedUser);
        }
    }
}