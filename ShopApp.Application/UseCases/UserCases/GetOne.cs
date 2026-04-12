using ShopApp.Domain.Interfaces;
using ShopApp.Application.Exceptions;
using AutoMapper;
using ShopApp.Application.DTOs.UserDTOs;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetOneUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        public IUserRepository _userRepository = userRepository;
        public IMapper _mapper = mapper;

        public async Task<UserResponseDTO> Execute(int id)
        {
            var user = await _userRepository.GetOneAsync(id);
            if (user == null) throw new UserNotFoundException(id);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> Execute(string email)
        {
            var user = await _userRepository.GetOneAsync(email);
            if (user == null) throw new UserNotFoundException(email);

            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}