# EmployeeManager.API Documentation

## Overview
EmployeeManager.API is the backend RESTful API for the Employee Manager application. It provides endpoints for managing employee data, including creating, reading, updating, and deleting employee records. The API is built with ASP.NET Core and uses Entity Framework Core for data access.

## Project Structure
- **Controllers/**: Contains API controllers (e.g., `EmployeesController`, `AuthController`) that define the HTTP endpoints.
- **Models/**: Contains data models such as `Employee` that represent the application's core entities.
- **Data/**: Contains the `AppDbContext` class, which manages database access using Entity Framework Core.
- **Migrations/**: Contains EF Core migration files for database schema management.
- **employees.db**: SQLite database file used for data storage.
- **Program.cs**: Configures and starts the ASP.NET Core application.
- **appsettings.json**: Configuration file for application settings (e.g., connection strings).

## Authentication & Authorization
- **JWT Authentication**: Users authenticate via `/api/auth/login` with hardcoded credentials:
  - `admin` / `1234` (Role: Admin)
  - `viewer` / `1234` (Role: Viewer)
- **JWT Token**: On successful login, the API returns a JWT token containing the user's role claim. The secret key for signing must be at least 32 characters (256 bits) for HS256.
- **Endpoint Protection**:
  - All endpoints in `EmployeesController` require authentication (`[Authorize]`).
  - Create, Edit, and Delete endpoints require the `Admin` role (`[Authorize(Roles = "Admin")]`).
  - GET endpoints are accessible to both Admin and Viewer roles.

## Key Components
- **EmployeesController**: Handles CRUD operations for employees via `/api/employees` endpoints, protected by role-based authorization.
- **AuthController**: Handles authentication and JWT issuance at `/api/auth/login`.
- **AppDbContext**: Entity Framework Core context for database operations.
- **Employee Model**: Represents employee data (Id, Name, Position, Salary).

## How It Works
- The API exposes endpoints for managing employees (GET, POST, PUT, DELETE).
- Uses Entity Framework Core to interact with a SQLite database.
- Supports migrations for database schema changes.
- All requests (except login) require a valid JWT in the Authorization header.

## How to Run
1. Ensure you have the .NET SDK installed.
2. Navigate to the `EmployeeManager.API` directory.
3. Run the API:
   ```sh
   dotnet run
   ```
4. The API will be available at `https://localhost:<port>/api/employees`.

---
For more details, see the source code and controller documentation. 