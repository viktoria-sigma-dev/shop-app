# UsersApp

## Overview

**UsersApp** is an ASP.NET Core Web API project that manages **users**, **orders**, **transactions**, and **products**.  
It uses a relational database table **`OrdersDB`** (and related tables) for persistence.  

The application includes:
- CRUD operations for users and orders
- Business rules for changing order statuses
- Transaction creation and refund logic
- Product stock management based on orders
- Middleware / filters for centralized error handling
- Swagger for API documentation

---

## Layers

- **Controllers** — Handle HTTP requests and map DTOs to responses  
- **Use Cases** — Contain business logic for specific actions (e.g., `UpdateStatusOrderUseCase`)  
- **Repositories** — Manage data persistence via EF Core  
- **Entities** — Represent domain models (`User`, `Order`, `Transaction`, `Product`)  
- **Enums** — Represent status values for orders and transactions  
- **Middlewares / Filters** — Global exception handling for consistent HTTP responses  

---

## Database

The application uses a relational database with multiple tables:

- **`OrdersDB`** — Stores order information (OrderId, UserId, Status, CreatedAt, etc.)
- **`UsersDB`** — Stores user information (UserId, Name, Email, etc.)
- **`OrderItemsDB`** — Stores individual items within an order (OrderItemId, OrderId, ProductId, Quantity, etc.)
- **`ProductsDB`** — Stores product information (ProductId, Name, Price, StockAmount, etc.)
- **`TransactionsDB`** — Stores payment and refund transactions (TransactionId, OrderId, Status, Total, CreatedAt, etc.)

### Foreign key relationships:
- `Order` → `User` (`UserId`)  
- `OrderItem` → `Order` (`OrderId`)  
- `OrderItem` → `Product` (`ProductId`)  
- `Transaction` → `Order` (`OrderId`)  

---

### Business Rules for Order Status Updates

Rules:

- **Initiated → Packed**  
  Can only pack if the current status is Initiated  
  Otherwise returns HTTP 400 (`BadHttpRequestException`)

- **Packed → Delivered**  
  Can only deliver if the current status is Packed  
  Otherwise returns HTTP 400

---

### Transaction Logic

When declining or refunding an order:
- Finds the paid transaction for the order
- Creates a refunded transaction with the same amount
- Updates product stock amounts based on returned items

---

### Technologies

- .NET 8.0  
- ASP.NET Core Web API  
- Entity Framework Core  
- Swagger  
- System.Text.Json for serialization  
- Dependency Injection (`AddScoped`, `AddSingleton`) for services & repositories  

---

### Run the Application

```bash
dotnet run