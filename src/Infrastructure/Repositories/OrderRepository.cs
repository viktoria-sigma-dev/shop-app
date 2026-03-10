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
    public class OrderRepository(ApplicationDbContext context) : IOrderRepository
    {
        private readonly ApplicationDbContext _context = context;
        public Task<List<Order>> GetAllAsync()
        {
            return _context.Orders.ToListAsync();
        }
        public Task<List<Order>> GetAllAsync(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
        public Task<List<Transaction>> GetUserTransactionsAsync(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .SelectMany(o => o.Transactions)
                .ToListAsync();
        }
        public Task<Order?> GetOneAsync(int orderId)
        {
            return _context.Orders.Include(t => t.OrderItems).FirstOrDefaultAsync(order => order.OrderId == orderId);
        }
        public async Task<Order> CreateOrder(Order Order)
        {
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();
            return Order;
        }
        public async Task<Order> UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}