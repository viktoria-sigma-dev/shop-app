using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.UserCases
{
    public class GetBalanceUserUseCase(IUserRepository userRepository)
    {
        public IUserRepository _userRepository = userRepository;

        public async Task<int> Execute(int id)
        {
            var user = await _userRepository.GetOneAsync(id);
            if (user == null) throw new KeyNotFoundException($"User with such id {id} was not found");

            return user.Balance;
        }
    }
}