using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Interfaces;
using ShopApp.Infrastructure.DBContext;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Enums;

namespace ShopApp.Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        private readonly ApplicationDbContext _context = context;
        public Task<List<Product>> GetAllAsync()
        {
            return _context.Products.ToListAsync();
        }
        public async Task<IReadOnlyCollection<Product>> GetAllAsync(IReadOnlyCollection<int> ids)
        {
            return await _context.Products.Where(p => ids.Contains(p.ProductId)).ToListAsync();
        }

        public Task<Product?> GetOneAsync(int id)
        {
            return _context.Products.FirstOrDefaultAsync(product => product.ProductId == id);
        }
        public Task<Product?> GetOneAsync(string name)
        {
            var normalizedName = name.Trim();
            return _context.Products.FirstOrDefaultAsync(product => product.Name.Trim() == normalizedName);
        }
        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}