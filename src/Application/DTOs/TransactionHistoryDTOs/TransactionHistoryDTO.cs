using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UsersApp.src.Application.DTOs.TransactionHistoryDTOs
{
    public enum TransactionType { Charged, Refunded }
    public record TransactionDTO
    {
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
    public record TransactionHistoryDTO
    {
        public ICollection<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
    }
}