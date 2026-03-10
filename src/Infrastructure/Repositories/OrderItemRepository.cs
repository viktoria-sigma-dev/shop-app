using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Infrastructure.DBContext;

namespace UsersApp.src.Infrastructure.Repositories
{
    public class OrderItemRepository(ApplicationDbContext context) : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task<List<OrderItem>> GetAllAsync(int orderId)
        {
            return _context.OrderItems.Where(orderItem => orderItem.OrderId == orderId).ToListAsync();
        }

        public async Task<OrderItem> Create(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
    }
}