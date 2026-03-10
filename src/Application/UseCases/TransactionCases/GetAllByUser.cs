using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Infrastructure.Repositories;

namespace UsersApp.src.Application.UseCases.TransactionCases
{
    public class GetAllByUserTransactionUseCase(IOrderRepository orderRepository, IUserRepository userRepository)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private IUserRepository _userRepository = userRepository;
        public async Task<List<Transaction>> Execute(int userId)
        {
            var user = await _userRepository.GetOneAsync(userId);
            if (user == null) throw new KeyNotFoundException($"User with such userId {userId} was not found!");
            return await _orderRepository.GetUserTransactionsAsync(userId);
        }
    }
}