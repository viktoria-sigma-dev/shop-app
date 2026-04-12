namespace ShopApp.Domain.Exceptions
{
     public class InvalidUserValueException : Exception
    {
        public InvalidUserValueException(string fieldName)
            : base($"{fieldName} cannot be empty of null!")
        {
        }
    }

    public class InvalidUserBalanceException : Exception
    {
        public InvalidUserBalanceException()
            : base("User balance cannot be less than 0!")
        {
        }
    }
    public class InsufficientUserBalanceException : Exception
    {
        public InsufficientUserBalanceException(decimal balance)
            : base($"User has insufficient balance to buy products. Current balance: {balance:F2}.")
        {
        }
    }
}