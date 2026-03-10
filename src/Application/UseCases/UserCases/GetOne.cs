using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Domain.Entities;

namespace UsersApp.src.Application.UseCases.UserCases
{
    public class GetOneUserUseCase(IUserRepository userRepository)
    {
        public IUserRepository _userRepository = userRepository;

        public async Task<User> Execute(int id)
        {
            var user = await _userRepository.GetOneAsync(id);
            if (user == null) throw new KeyNotFoundException($"User with such id {id} was not found");

            return user;
        }

        public async Task<User> Execute(string email)
        {
            var user = await _userRepository.GetOneAsync(email);
            if (user == null) throw new KeyNotFoundException($"User with such email {email} was not found");

            return user;
        }
    }
}