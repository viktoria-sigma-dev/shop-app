using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Enums;

namespace UsersApp.src.Application.DTOs.OrderDTOs
{
    public record OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Status { get; set; }
    }
}