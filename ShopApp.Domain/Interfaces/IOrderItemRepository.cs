using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAllAsync(int orderId);
        Task<OrderItem> Create(OrderItem orderItem);
    }
}