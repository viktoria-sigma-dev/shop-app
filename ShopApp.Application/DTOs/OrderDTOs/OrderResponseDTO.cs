using ShopApp.Application.DTOs.OrderItemDTOs;
using ShopApp.Domain.Enums;

namespace ShopApp.Application.DTOs.OrderDTOs
{
    public record OrderResponseDTO
    {
        public int Id { get; init; }
        public OrderStatus Status { get; init; }
        public DateTime CreatedAt { get; init; }
        public int UserId { get; init; }
        public required ICollection<OrderItemResponseDTO> OrderItems { get; init; }
    }
}