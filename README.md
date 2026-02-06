# UsersApp

## Overview
**UsersApp** is a simple ASP.NET Core Web API project that manages **users** and their **comments**.  
It uses **JSON file storage** (via `FileService`) instead of a database, making it lightweight and easy to run locally.  
The application includes:
- CRUD operations for users and comments
- Middleware for centralized error handling
- Swagger/OpenAPI for API documentation

---

## Layers:
Controllers — Handle HTTP requests and responses
Services — Contain business logic
Repositories — Manage data persistence
FileService — Reads/writes JSON files
Middlewares — Global exception handling

## Technologies
.NET 10.0
ASP.NET Core Web API
Swagger
System.Text.Json for serialization
Dependency Injection (AddScoped, AddSingleton) for services & repositories

## Run the application
dotnet run

## API Documentation
Swagger UI is available at:

http://localhost:5244/swagger/index.html

# API Endpoints
## Users
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/Users` | Get all users |
| GET    | `/api/Users?extended=true` | Get all users with extended details |
| POST   | `/api/Users` | Create a new user |
| DELETE | `/api/Users/{userId}` | Delete a user |

## Comments
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/Comments/{userId}` | Get all comments for a user |
| POST   | `/api/Comments/{userId}` | Add a comment for a user |
| DELETE | `/api/Comments/{userId}/{commentId}` | Delete a comment |

## Error Handling
The application uses ErrorHandlingMiddleware to catch unhandled exceptions and return:
`{"error": "An unexpected error occurred."}`
with a proper HTTP status code.

## File Storage
Data is stored in: `public/data.json`.
Created automatically on first run
Initialized with [] (empty JSON array)
Read/written via FileService