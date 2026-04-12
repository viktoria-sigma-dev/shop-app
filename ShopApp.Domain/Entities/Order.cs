using ShopApp.Domain.Exceptions;
using ShopApp.Domain.Enums;
using ShopApp.Domain.ValueObjects;
using ShopApp.Application.Exceptions;

namespace ShopApp.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; init; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        private Order() { }

        private Order(
                OrderStatus status,
                User user
                )
        {
            Status = status;
            CreatedAt = DateTime.UtcNow;
            UserId = user.UserId;
            User = user;
        }

        public static Order Create(
            User user,
            IReadOnlyCollection<Product> products,
            List<OrderItemRequest> orderItems
        )
        {
            var orderItemsDict = GetOrderItemsDictionary(orderItems);
            var productsDict = products.ToDictionary(p => p.ProductId, p => p);
            var missingIds = orderItemsDict.Keys.Where(id => !productsDict.ContainsKey(id)).ToList();

            if (missingIds.Any())
            {
                throw new ProductNotFoundInOrderException(missingIds);
            }

            var order = new Order(OrderStatus.Initiated, user);

            var createdOrderItems = orderItemsDict.Select(oi => OrderItem.Create(
                    order,
                    productsDict[oi.Key],
                    oi.Value
                )).ToList();


            order.OrderItems = createdOrderItems;
            order.CreatePaidTransaction();

            return order;
        }

        public decimal GetTotal()
        {
            return OrderItems.Sum(oi => oi.Quantity * oi.Price);
        }

        public Transaction CreatePaidTransaction()
        {
            var transaction = new Transaction
            {
                OrderId = OrderId,
                Order = this,
                Status = TransactionStatus.Paid,
                Total = GetTotal(),
            };
            Transactions.Add(transaction);
            User.DecreaseBalance(transaction.Total);
            return transaction;
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            var allowedStatusTransition = AllowedStatusTransitions[Status];
            if (!allowedStatusTransition.Contains(newStatus))
            {
                throw new InvalidOrderStatusException($"Cannot change order status from '{Status}' to '{newStatus}'");
            }
            Status = newStatus;
        }
        public void DeclineOrder()
        {
            var paidTransaction = Transactions.SingleOrDefault(t => t.Status == TransactionStatus.Paid);
            if (paidTransaction == null) throw new PaidTransactionNotFoundException(OrderId);

            ChangeStatus(OrderStatus.Declined);
            
            var transaction = new Transaction
            {
                Status = TransactionStatus.Refunded,
                OrderId = OrderId,
                Order = this,
                Total = paidTransaction.Total,
            };
            Transactions.Add(transaction);
            User.IncreaseBalance(paidTransaction.Total);
            foreach (var orderItem in OrderItems)
            {
                orderItem.Product.IncreaseStock(orderItem.Quantity);
            }
        }

        private static readonly Dictionary<OrderStatus, List<OrderStatus>> AllowedStatusTransitions = new()
        {
            { OrderStatus.Initiated, new List<OrderStatus>{OrderStatus.Packed, OrderStatus.Declined} },
            { OrderStatus.Packed, new List<OrderStatus>{OrderStatus.Delivered} },
            { OrderStatus.Delivered, new List<OrderStatus>() },
            { OrderStatus.Declined, new List<OrderStatus>() }
        };

        private static Dictionary<int, int> GetOrderItemsDictionary(List<OrderItemRequest> orderItems)
        {
            if (orderItems == null || orderItems.Count == 0 || orderItems.Any(oi => oi.Quantity == 0))
                throw new ProductInvalidQuantityException();

            try
            {
                return orderItems.ToDictionary(oi => oi.ProductId, oi => oi.Quantity);
            }
            catch (ArgumentException)
            {
                throw new ProductQuantityExceededException();
            }
        }
    }
}