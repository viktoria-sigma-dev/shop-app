using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApp.src.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int StockAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal GetTotal(int quantity)
        {
            return this.Price * quantity;
        }
    }
}