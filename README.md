# ShopApp

## Overview

**ShopApp** is an ASP.NET Core Web API project that manages **users**, **orders**, **transactions**, and **products**.  
It uses a relational database (**`OrdersAppDB`**) for persistent storage via Entity Framework Core.

The application includes:
- CRUD operations for users, products, and orders
- Business rules for validating and changing order statuses
- Transaction creation logic for payments and refunds
- User balance management (increase/decrease based on transactions)
- Product stock management based on order activity
- Centralized error handling via middleware and filters
- Swagger integration for API documentation and testing

---

## Architecture

The solution is organized into multiple projects, each representing a distinct layer of **Clean Architecture**:

- **ShopApp.Domain**  
  Contains the core business entities (`User`, `Order`, `Transaction`, `Product`, `OrderItem`), value objects, enums, and domain-specific exceptions.  
  This layer is independent of any external frameworks.

- **ShopApp.Application**  
  Contains use cases (application services) such as `CreateOrderUseCase` and `UpdateStatusOrderUseCase`.  
  Defines interfaces for repositories that the domain layer depends on.  
  Handles application-specific business logic.

- **ShopApp.Infrastructure**  
  Implements repository interfaces using Entity Framework Core.  
  Contains database configurations, migrations, and integration with the MySQL provider (`Pomelo.EntityFrameworkCore.MySql`).  
  Manages persistence and external service integrations.

- **ShopApp.WebApi**  
  The presentation layer (ASP.NET Core Web API).  
  Contains controllers, request/response DTOs, dependency injection setup, middleware, filters, and Swagger configuration.

---

## Key Principles

- **Dependency Inversion** — Higher-level layers depend on abstractions, not concrete implementations.
- **Separation of Concerns** — Each layer has a single responsibility.
- **Framework Independence** — The core business logic (Domain) does not depend on ASP.NET Core, EF Core, or any external libraries.
- **Testability** — Each layer can be tested independently.

---

## Database

The application uses a relational database with multiple tables:

- **`OrdersDB`** — Stores order information (OrderId, UserId, Status, CreatedAt, etc.)
- **`UsersDB`** — Stores user information (UserId, Name, Email, etc.)
- **`OrderItemsDB`** — Stores individual items within an order (OrderItemId, OrderId, ProductId, Quantity, etc.)
- **`ProductsDB`** — Stores product information (ProductId, Name, Price, StockAmount, etc.)
- **`TransactionsDB`** — Stores payment and refund transactions (TransactionId, OrderId, Status, Total, CreatedAt, etc.)

### Foreign key relationships:
- **`Order` → `User` (`UserId`)**  
  - One `User` can have many `Orders` (`User.Orders`)  
  - Cascade delete: deleting a `User` deletes all related `Orders`

- **`Order` → `OrderItems` (`OrderId`)**  
  - One `Order` can have many `OrderItems` (`Order.OrderItems`)  

- **`Order` → `Transactions` (`OrderId`)**  
  - One `Order` can have many `Transactions` (`Order.Transactions`)  
  - Cascade delete: deleting an `Order` deletes all related `Transactions` and `OrderItems`

- **`OrderItem` → `Product` (`ProductId`)**  
  - Each `OrderItem` references a single `Product`  
  - Cascade delete: deleting a `Product` deletes related `OrderItems`

---

### Business Rules for Order Status Updates

Rules:

- **Initiated → Packed**  
  Can only pack if the current status is Initiated  
  Otherwise returns HTTP 400 (`InvalidOrderStatusException`)

- **Initiated → Declined**  
  Can only pack if the current status is Initiated  
  Otherwise returns HTTP 400 (`InvalidOrderStatusException`)

- **Packed → Delivered**  
  Can only deliver if the current status is Packed  
  Otherwise returns HTTP 400 (`InvalidOrderStatusException`)

---

### Transaction Logic

#### When creating an order:
- Starts a database transaction (`BeginTransactionAsync`)
- Finds the user by ID  
  - Throws `UserNotFoundException` if the user does not exist
- Loads all products referenced in the order items  
  - Throws `ProductNotFoundInOrderException` if any product ID is missing
- Validates order item quantities  
  - Throws `ProductInvalidQuantityException` if quantity is zero  
  - Throws `ProductQuantityExceededException` if duplicate product IDs
- Creates an order with status `Initiated`
- Creates `OrderItem` entities and links them to the order
- Creates a `Paid` transaction with the total price of all order items (`GetTotal`)
- Deducts the transaction total from the user's balance (`DecreaseBalance`)
- Persists the order and its transaction in the database
- Commits the database transaction (`CommitAsync`)
- Rolls back the database transaction (`RollbackAsync`) if an exception occurs

#### When declining or refunding an order:
- Finds the order and its paid transaction
- Changes the order status to `Declined`  
  - Throws `InvalidOrderStatusException` if status change is not allowed
- Creates a `Refunded` transaction with the same amount as the original paid transaction
- Increases the user's balance by the transaction total (`IncreaseBalance`)
- Increases stock quantity for each product in the order (`IncreaseStock`)

---

### Technologies

- **.NET 8.0** — main platform
- **ASP.NET Core Web API** — REST API implementation
- **Entity Framework Core (EF Core)** — ORM for database operations
- **Pomelo.EntityFrameworkCore.MySql** — EF Core provider for MySQL
- **Swagger / Swashbuckle** — API documentation and testing
- **System.Text.Json** — JSON serialization/deserialization
- **Dependency Injection** — (`AddScoped`, `AddSingleton`) for services and repositories


---

### Run the Application

```bash
dotnet run
```
After running, the application will be available at:
`http://localhost:5102`

### Run migrations

Create migration:
```bash
cd ./ShopApp.WebApi
dotnet ef migrations add InitialCreate --project ../ShopApp.Infrastructure --startup-project .
dotnet ef database update --project ../ShopApp.Infrastructure --startup-project .
dotnet run
```

Apply existing migration:
```bash
cd ./ShopApp.WebApi
dotnet ef database update --project ../ShopApp.Infrastructure --startup-project .
dotnet run
```

### API Testing
Open Swagger UI to test the API:
`http://localhost:5102/swagger/index.html`