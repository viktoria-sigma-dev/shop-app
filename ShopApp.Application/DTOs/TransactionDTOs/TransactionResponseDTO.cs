using ShopApp.Domain.Enums;

namespace ShopApp.Application.DTOs.TransactionDTOs
{
    public record TransactionResponseDTO
    {
        public int Id { get; set; }
        public required TransactionStatus Status { get; init; }
        public required decimal Total { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}