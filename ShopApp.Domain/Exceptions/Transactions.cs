namespace ShopApp.Application.Exceptions
{
    public class PaidTransactionNotFoundException : Exception
    {
        public PaidTransactionNotFoundException(int orderId)
            : base($"Paid transaction with such order id {orderId} was not found")
        {
        }
    }
}