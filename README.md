# UsersApp

## Overview
**UsersApp** is a simple ASP.NET Core Web API project that manages **users** and their **comments**.  
It uses **JSON file storage** (via `FileService`) instead of a database, making it lightweight and easy to run locally.  

The application includes:
- CRUD operations for users and comments
- Middleware for centralized error handling
- Swagger for API documentation

---

## Layers
- **Controllers** — Handle HTTP requests and responses  
- **Services** — Contain business logic  
- **Repositories** — Manage data persistence  
- **FileService** — Reads/writes JSON files  
- **Middlewares** — Global exception handling  

---

## Technologies
- **.NET 10.0**
- **ASP.NET Core Web API**
- **Swagger**
- **System.Text.Json** for serialization
- **Dependency Injection** (`AddScoped`, `AddSingleton`) for services & repositories

---

## Run the Application
```bash
dotnet run