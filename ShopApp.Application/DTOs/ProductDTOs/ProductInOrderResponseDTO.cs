namespace ShopApp.Application.DTOs.ProductDTOs
{
    public record ProductInOrderResponseDTO
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}