using ShopApp.Domain.Interfaces;
using ShopApp.Application.Exceptions;
using AutoMapper;
using ShopApp.Application.DTOs.TransactionDTOs;

namespace ShopApp.Application.UseCases.TransactionCases
{
    public class GetAllByUserTransactionUseCase(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
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