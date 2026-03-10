using UsersApp.src.Domain.Entities;

namespace UsersApp.src.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync(int orderId);
        Task<List<Transaction>> GetAllPaidAsync();
        Task<Transaction?> GetPaidByOrderIdAsync(int orderId);
        Task<Transaction> Create(Transaction transaction);
    }
}