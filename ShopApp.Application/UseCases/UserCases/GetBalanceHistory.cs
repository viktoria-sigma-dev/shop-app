using AutoMapper;
using ShopApp.Application.DTOs.TransactionDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetBalanceHistoryUserUseCase(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
    {
        public async Task<List<TransactionResponseDTO>> Execute(int userId)
        {
            var user = await userRepository.GetOneAsync(userId);
            if (user == null) throw new UserNotFoundException(userId);
            var transactions = await orderRepository.GetUserTransactionsAsync(userId);

            return mapper.Map<List<TransactionResponseDTO>>(transactions);
        }
    }
}