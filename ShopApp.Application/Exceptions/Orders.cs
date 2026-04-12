using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Application.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int id)
            : base($"Order with id '{id}' was not found.")
        {
        }
    }
}