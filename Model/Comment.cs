using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApp.Model
{
    public class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Comment text is required.")]
        [MinLength(1, ErrorMessage = "Comment text cannot be empty.")]
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}