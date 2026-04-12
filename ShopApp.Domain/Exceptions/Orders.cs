using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Domain.Exceptions
{
    public class InvalidOrderStatusException : Exception
    {
        public InvalidOrderStatusException(string errorMessage)
            : base(errorMessage)
        {
        }
    }

    public class ProductInvalidQuantityException : Exception
    {
        public ProductInvalidQuantityException()
            : base("Order must contain at least one item.")
        {
        }
    }

    public class ProductQuantityExceededException : Exception
    {
        public ProductQuantityExceededException()
            : base("Your order contains 1 or more products with the same productId")
        {
        }
    }

    public class ProductNotFoundInOrderException : Exception
    {
        public ProductNotFoundInOrderException(List<int> missingIds)
            : base($"Product ids [{string.Join(", ", missingIds)}] were not found in available products list.")
        {
        }
    }
}