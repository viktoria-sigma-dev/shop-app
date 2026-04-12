using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;
using ShopApp.Infrastructure.DBContext;

namespace ShopApp.Infrastructure.Repositories
{
    public class OrderItemRepository(ApplicationDbContext context) : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task<List<OrderItem>> GetAllAsync(int orderId)
        {
            return _context.OrderItems.Where(orderItem => orderItem.OrderId == orderId).Include(oi => oi.Product).ToListAsync();
        }

        public async Task<OrderItem> Create(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
    }
}