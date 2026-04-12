using AutoMapper;
using ShopApp.Application.DTOs.UserDTOs;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetAllUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        public IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<UserResponseDTO>> Execute()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<List<UserResponseDTO>>(users);
        }
    }
}