using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Application.DTOs.UserDTOs;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.UserCases
{
    public class UpdateUserUseCase(IUserRepository userRepository)
    {
        public IUserRepository _userRepository = userRepository;

        public async Task<User> Execute(int id, UpdateUserDTO dto)
        {
            var user = await _userRepository.GetOneAsync(id);
            if (user == null) throw new KeyNotFoundException($"User with such id {id} does not exist!");

            if (dto.FirstName != null) user.FirstName = dto.FirstName;
            if (dto.LastName != null) user.LastName = dto.LastName;
            if (dto.DateOfBirth != null) user.DateOfBirth = dto.DateOfBirth;

            return await _userRepository.UpdateAsync(user);
        }
    }
}