using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Application.DTOs.UserDTOs;
using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Domain.Entities;

namespace UsersApp.src.Application.UseCases.UserCases
{
    public class CreateUserUseCase(IUserRepository userRepository)
    {
        public IUserRepository _userRepository = userRepository;

        public async Task<User> Execute(CreateUserDTO dto)
        {
            var existingUser = await _userRepository.GetOneAsync(dto.Email);
            if (existingUser != null) throw new BadHttpRequestException($"User with such email {dto.Email} already exists!");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
            };

            return await _userRepository.CreateAsync(user);
        }
    }
}