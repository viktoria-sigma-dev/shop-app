using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApp.src.Application.DTOs.UserDTOs
{
    public record UserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = String.Empty;
        public string? LastName { get; set; } = String.Empty;
        public string? Email { get; set; } = String.Empty;
        public string? DateOfBirth { get; set; } = String.Empty;
    }
}