namespace ShopApp.Application.DTOs.UserDTOs
{
    public record UserResponseDTO
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = String.Empty;
        public string LastName { get; init; } = String.Empty;
        public string Email { get; init; } = String.Empty;
        public decimal Balance { get; init; } = 0;
        public string? DateOfBirth { get; init; } = String.Empty;
    }
}