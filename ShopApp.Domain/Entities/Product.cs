using ShopApp.Domain.Exceptions;

namespace ShopApp.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; init; } = null!;
        public decimal Price { get; init; }
        public int StockAmount { get; private set; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public Product() { }
        public Product(string name, decimal price, int stockAmount)
        {
            if (decimal.IsNegative(price)) throw new NegativeProductPriceException();
            if (int.IsNegative(stockAmount)) throw new NegativeProductAmountException();
            Name = name;
            Price = price;
            StockAmount = stockAmount;
        }

        public decimal IncreaseStock(int amount)
        {
            if (int.IsNegative(amount)) throw new NegativeProductAmountException();
            StockAmount += amount;
            return StockAmount;
        }
        public decimal DecreaseStock(int amount)
        {
            if (int.IsNegative(amount)) throw new NegativeProductAmountException();
            if (StockAmount < amount) throw new InsufficientProductAmountException(StockAmount, Name, amount);
            StockAmount -= amount;
            return StockAmount;
        }

        public decimal GetTotal(int amount)
        {
            if (int.IsNegative(amount)) throw new NegativeProductAmountException();
            return amount * Price;
        }
    }
}