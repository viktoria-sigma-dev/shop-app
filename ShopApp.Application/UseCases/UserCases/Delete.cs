using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class DeleteUserUseCase(IUserRepository userRepository)
    {
        public IUserRepository _userRepository = userRepository;

        public async Task<bool> Execute(int id)
        {
            var deleted = await _userRepository.DeleteAsync(id);
            if (!deleted) throw new UserNotFoundException(id);
            return true;
        }
    }
}