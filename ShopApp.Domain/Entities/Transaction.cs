using System.ComponentModel.DataAnnotations;
using ShopApp.Domain.Enums;

namespace ShopApp.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; init; }
        public required TransactionStatus Status { get; set; }
        public required decimal Total { get; init; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int OrderId { get; set; }
        public required Order Order { get; set; }
    }
}