namespace ShopApp.Domain.ValueObjects
{
    public record OrderItemRequest(int ProductId, int Quantity);
}