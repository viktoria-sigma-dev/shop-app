using AutoMapper;
using ShopApp.Application.DTOs.UserDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class UpdateUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        public IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserResponseDTO> Execute(int id, UpdateUserDTO dto)
        {
            var user = await _userRepository.GetOneAsync(id);
            if (user == null) throw new UserNotFoundException(id);

            if (!string.IsNullOrEmpty(dto.FirstName)) user.SetFirstName(dto.FirstName);
            if (!string.IsNullOrEmpty(dto.LastName)) user.SetLastName(dto.LastName);
            if (!string.IsNullOrEmpty(dto.DateOfBirth)) user.SetDateOfBirth(dto.DateOfBirth);

            var updatedUser = await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserResponseDTO>(updatedUser);
        }
    }
}