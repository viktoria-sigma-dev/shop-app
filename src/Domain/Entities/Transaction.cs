using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Enums;

namespace UsersApp.src.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public required TransactionStatus Status { get; set; }
        public required decimal Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int OrderId { get; set; }
        public required Order Order { get; set; }
    }
}