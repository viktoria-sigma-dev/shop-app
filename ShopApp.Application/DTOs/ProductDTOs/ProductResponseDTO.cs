namespace ShopApp.Application.DTOs.ProductDTOs
{
    public record ProductResponseDTO
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public decimal Price { get; init; }
        public int StockAmount { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}