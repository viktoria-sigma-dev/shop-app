using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.UserCases
{
    public class DeleteUserUseCase(IUserRepository userRepository)
    {
        public IUserRepository _userRepository = userRepository;

        public async Task<bool> Execute(int id)
        {
            var deleted = await _userRepository.DeleteAsync(id);
            if (!deleted) throw new KeyNotFoundException($"User with such id {id} does not exist!");
            return true;
        }
    }
}