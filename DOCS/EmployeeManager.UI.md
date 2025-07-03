# EmployeeManager.UI Documentation

## Overview
EmployeeManager.UI is the frontend web application for the Employee Manager system. It provides a user interface for managing employees, including listing, adding, editing, and deleting employee records. The UI is built with ASP.NET Core Razor Pages.

## Project Structure
- **Pages/**: Contains Razor Pages for the UI, including:
  - `Employees/Employees.cshtml`: Main page for managing employees.
  - `Shared/`, `_ViewImports.cshtml`, `_ViewStart.cshtml`: Shared layout and configuration.
- **wwwroot/**: Contains static files (CSS, JS, images, libraries).
- **Program.cs**: Configures and starts the ASP.NET Core web application.
- **appsettings.json**: Configuration file for application settings.

## Key Components
- **Employees Page**: Main interface for listing, adding, editing, and deleting employees.
- **Forms**: Used for adding and editing employee data.
- **Table**: Displays the list of employees and provides action buttons.

## How It Works
- The UI interacts with the EmployeeManager.API backend via HTTP requests.
- Users can add, edit, or delete employees using the provided forms and buttons.
- The UI updates dynamically based on user actions and API responses.

## How to Run
1. Ensure you have the .NET SDK installed.
2. Navigate to the `EmployeeManager.UI` directory.
3. Run the UI:
   ```sh
   dotnet run
   ```
4. The application will be available at `https://localhost:<port>/Employees/Employees`.

---
For more details, see the source code and page documentation. 