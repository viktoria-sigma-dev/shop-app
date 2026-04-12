using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync(int orderId);
        Task<List<Transaction>> GetAllPaidAsync();
        Task<Transaction> Create(Transaction transaction);
    }
}