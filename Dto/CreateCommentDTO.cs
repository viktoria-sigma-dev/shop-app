using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UsersApp.Dto
{
    public record CreateCommentDTO
    {
        [Required]
        public required string Text { get; set; }
    }
}