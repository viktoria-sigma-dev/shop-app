namespace ShopApp.Domain.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; init; }
        public int OrderId { get; init; }
        public Order Order { get; init; } = null!;
        public int ProductId { get; init; }
        public Product Product { get; init; } = null!;
        public int Quantity { get; init; }
        public decimal Price { get; init; }

        private OrderItem() { }

        private OrderItem(
            Order order,
            Product product,
            int quantity)
        {
            ProductId = product.ProductId;
            Product = product;
            Order = order;
            OrderId = order.OrderId;
            Quantity = quantity;
            Price = product.Price;
        }

        public static OrderItem Create(
            Order order,
            Product product,
            int quantity)
        {
            product.DecreaseStock(quantity);
            
            return new OrderItem(order, product, quantity)
            {
                OrderItemId = 0,
            };
        }
    }
}