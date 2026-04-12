using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Domain.Exceptions
{
    public class NegativeProductAmountException : Exception
    {
        public NegativeProductAmountException()
            : base("Product amount cannot be negative!")
        {
        }
    }
    public class NegativeProductPriceException : Exception
    {
        public NegativeProductPriceException()
            : base("Product price cannot be negative!")
        {
        }
    }

    public class InsufficientProductAmountException : Exception
    {
        public InsufficientProductAmountException(int stockAmount, string name, int amount)
            : base($"Only {stockAmount} \"{name}\" products left in stock, cannot decrease by {amount}!")
        {
        }
    }
}