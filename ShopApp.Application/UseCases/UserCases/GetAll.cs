using AutoMapper;
using ShopApp.Application.DTOs.UserDTOs;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetAllUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        public async Task<List<UserResponseDTO>> Execute()
        {
            var users = await userRepository.GetAllAsync();

            return mapper.Map<List<UserResponseDTO>>(users);
        }
    }
}