using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Application.DTOs.ProductDTOs
{
    public record CreateProductDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required decimal Price { get; set; }
        [Required]
        public required int StockAmount { get; set; }
    }
}