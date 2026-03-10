using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApp.src.Application.DTOs.TransactionDTOs
{
    public record TransactionDTO
    {
        public int TransactionId { get; init; }
        public DateTime CreatedAt { get; init; }
        public required string Status { get; init; }
        public decimal Total { get; init; }
        public int OrderId { get; init; }
    }
}