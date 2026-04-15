using AutoMapper;
using ShopApp.Application.DTOs.UserDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class TopUpBalanceUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        public async Task<UserResponseDTO> Execute(int userId, decimal amount)
        {
            var user = await userRepository.GetOneAsync(userId);

            if (user == null) throw new UserNotFoundException(userId);

            user.IncreaseBalance(amount);

            var updatedUser = await userRepository.UpdateAsync(user);

            return mapper.Map<UserResponseDTO>(updatedUser);
        }
    }
}