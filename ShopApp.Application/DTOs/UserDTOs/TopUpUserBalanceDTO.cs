using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Application.DTOs.UserDTOs
{
    public record TopUpUserBalanceDTO
    {
        public decimal Amount { get; set; }
    }
}