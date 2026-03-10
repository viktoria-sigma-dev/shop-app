using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Enums;

namespace UsersApp.src.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public required User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}