using ShopApp.Application.DTOs.ProductDTOs;

namespace ShopApp.Application.DTOs.OrderItemDTOs
{
    public record OrderItemResponseDTO
    {
        public int Id { get; init; }
        public required ProductInOrderResponseDTO Product { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
    }
}