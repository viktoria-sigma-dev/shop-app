using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Infrastructure.DBContext;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Enums;

namespace UsersApp.src.Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        private readonly ApplicationDbContext _context = context;
        public Task<List<Product>> GetAllAsync()
        {
            return _context.Products.ToListAsync();
        }

        public Task<Product?> GetOneAsync(int id)
        {
            return _context.Products.FirstOrDefaultAsync(product => product.ProductId == id);
        }
        public Task<Product?> GetOneAsync(string name)
        {
            return _context.Products.FirstOrDefaultAsync(product => product.Name == name);
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