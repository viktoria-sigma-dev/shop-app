using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Domain.Entities;

namespace UsersApp.src.Application.UseCases.TransactionCases
{
    public class GetAllByOrderTransactionUseCase(ITransactionRepository transactionRepository, IOrderRepository orderRepository)
    {
        private ITransactionRepository _transactionRepository = transactionRepository;
        private IOrderRepository _orderRepository = orderRepository;
        public async Task<List<Transaction>> Execute(int orderId)
        {
            var order = await _orderRepository.GetOneAsync(orderId);
            if (order == null) throw new KeyNotFoundException($"Order with such id {orderId} wasn't found!");
            return await _transactionRepository.GetAllAsync(orderId);
        }
    }
}