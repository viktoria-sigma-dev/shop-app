using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetAllAsync(int userId);
        Task<List<Transaction>> GetUserTransactionsAsync(int userId);
        Task<Order?> GetOneWithOrderItemsAsync(int orderId);
        Task<Order?> GetOneWithTransactionsAsync(int orderId);
        Task<Order?> GetOneFullAsync(int orderId);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(Order order);
    }
}