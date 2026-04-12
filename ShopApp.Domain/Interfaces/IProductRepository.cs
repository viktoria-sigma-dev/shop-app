using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<IReadOnlyCollection<Product>> GetAllAsync(IReadOnlyCollection<int> ids);
        Task<Product?> GetOneAsync(int id);
        Task<Product?> GetOneAsync(string name);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}