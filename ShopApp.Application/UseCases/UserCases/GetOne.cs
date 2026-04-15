using ShopApp.Domain.Interfaces;
using ShopApp.Application.Exceptions;
using AutoMapper;
using ShopApp.Application.DTOs.UserDTOs;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetOneUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        public async Task<UserResponseDTO> Execute(int id)
        {
            var user = await userRepository.GetOneAsync(id);
            if (user == null) throw new UserNotFoundException(id);

            return mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> Execute(string email)
        {
            var user = await userRepository.GetOneAsync(email);
            if (user == null) throw new UserNotFoundException(email);

            return mapper.Map<UserResponseDTO>(user);
        }
    }
}