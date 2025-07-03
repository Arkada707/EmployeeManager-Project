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

## Key Components
- **EmployeesController**: Handles CRUD operations for employees via `/api/employees` endpoints.
- **AuthController**: Handles authentication-related endpoints (if implemented).
- **AppDbContext**: Entity Framework Core context for database operations.
- **Employee Model**: Represents employee data (Id, Name, Position, Salary).

## How It Works
- The API exposes endpoints for managing employees (GET, POST, PUT, DELETE).
- Uses Entity Framework Core to interact with a SQLite database.
- Supports migrations for database schema changes.

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