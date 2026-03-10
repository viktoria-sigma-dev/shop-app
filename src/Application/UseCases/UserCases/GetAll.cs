using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.UserCases
{
    public class GetAllUserUseCase(IUserRepository userRepository)
    {
        public IUserRepository _userRepository = userRepository;

        public Task<List<User>> Execute()
        {
            return _userRepository.GetAllAsync();
        }
    }
}