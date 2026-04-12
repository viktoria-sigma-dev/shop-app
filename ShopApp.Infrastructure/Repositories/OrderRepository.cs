using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Interfaces;
using ShopApp.Infrastructure.DBContext;
using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : IOrderRepository
    {
        private readonly ApplicationDbContext _context = context;
        public Task<List<Order>> GetAllAsync()
        {
            return _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();
        }
        public Task<List<Order>> GetAllAsync(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .ToListAsync();
        }
        public Task<List<Transaction>> GetUserTransactionsAsync(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .SelectMany(o => o.Transactions)
                .ToListAsync();
        }
        public Task<Order?> GetOneWithTransactionsAsync(int orderId)
        {
            return _context.Orders
            .Include(o => o.Transactions)
            .FirstOrDefaultAsync(order => order.OrderId == orderId);
        }
        public Task<Order?> GetOneWithOrderItemsAsync(int orderId)
        {
            return _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(order => order.OrderId == orderId);
        }
        public Task<Order?> GetOneFullAsync(int orderId)
        {
            return _context.Orders
            .Include(o => o.User)
            .Include(o => o.Transactions)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(order => order.OrderId == orderId);
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