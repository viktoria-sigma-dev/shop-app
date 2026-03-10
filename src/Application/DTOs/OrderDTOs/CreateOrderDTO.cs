using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UsersApp.src.Application.DTOs.OrderDTOs
{
    public record CreateOrderItemDTO
    {
        [Required]
        public required int ProductId { get; set; }
        [Required]
        public required int Quantity { get; set; }
    }
    public record CreateOrderDTO
    {
        [Required]
        public required int UserId { get; set; }

        [Required]
        public required List<CreateOrderItemDTO> OrderItems { get; set; }
    }
}