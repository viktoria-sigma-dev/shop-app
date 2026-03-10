using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Enums;
using UsersApp.src.Infrastructure.Repositories;

namespace UsersApp.src.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetOneAsync(int id);
        Task<Product?> GetOneAsync(string name);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}