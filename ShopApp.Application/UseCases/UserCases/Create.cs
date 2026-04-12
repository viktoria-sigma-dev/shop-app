using ShopApp.Application.DTOs.UserDTOs;
using ShopApp.Domain.Interfaces;
using ShopApp.Domain.Entities;
using ShopApp.Application.Exceptions;
using AutoMapper;

namespace ShopApp.Application.UseCases.UserCases
{
    public class CreateUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        public IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserResponseDTO> Execute(CreateUserDTO dto)
        {
            var existingUser = await _userRepository.GetOneAsync(dto.Email);
            if (existingUser != null) throw new DuplicatedUserEmailException(dto.Email);

            var user = new User
            (new CreateUserDto
            {
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Email = dto.Email.Trim(),
                DateOfBirth = dto.DateOfBirth?.Trim(),
            });

            var createdUser = await _userRepository.CreateAsync(user);

            return _mapper.Map<UserResponseDTO>(createdUser);
        }
    }
}