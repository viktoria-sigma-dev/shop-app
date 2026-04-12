using System.ComponentModel.DataAnnotations;

namespace ShopApp.Application.DTOs.UserDTOs
{
    public record CreateUserDTO
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public string? DateOfBirth { get; set; } = String.Empty;
    }
}