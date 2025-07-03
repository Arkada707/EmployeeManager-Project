# EmployeeManager.UI Documentation

## Overview
EmployeeManager.UI is the frontend web application for the Employee Manager system. It provides a user interface for managing employees, including listing, adding, editing, and deleting employee records. The UI is built with ASP.NET Core Razor Pages.

## Project Structure
- **Pages/**: Contains Razor Pages for the UI, including:
  - `Employees/Employees.cshtml`: Main page for managing employees (now the home page).
  - `Login.cshtml`: Login page for user authentication.
  - `Logout.cshtml`: Logs out the user and clears the session.
  - `Shared/`, `_ViewImports.cshtml`, `_ViewStart.cshtml`: Shared layout and configuration.
- **wwwroot/**: Contains static files (CSS, JS, images, libraries).
- **Program.cs**: Configures and starts the ASP.NET Core web application.
- **appsettings.json**: Configuration file for application settings.

## Authentication & User Flow
- **Login**: Users log in via the Login page. Credentials are sent to the API, and a JWT token is stored in session on success.
- **Logout**: Users can log out via a Logout link, which clears the session and redirects to Login.
- **JWT Storage**: The JWT token is stored in session and attached to all outgoing API requests.
- **Authentication Enforcement**: All Employees page actions check for a valid JWT and redirect to Login if not authenticated.

## Role-Based UI
- **Admin**: Can add, edit, and delete employees. Sees the Add form and action buttons.
- **Viewer**: Can only view the employee table. Add/Edit/Delete buttons and forms are hidden.
- The UI adapts automatically based on the user's role (parsed from the JWT token).

## Navigation
- The Employees page is now the home page. Navigating to `/` or running the UI project redirects to `/Employees/Employees`.
- The navigation bar only shows the Employees link for simplicity.

## Key Components
- **Employees Page**: Main interface for listing, adding, editing, and deleting employees.
- **Forms**: Used for adding and editing employee data.
- **Table**: Displays the list of employees and provides action buttons.

## How It Works
- The UI interacts with the EmployeeManager.API backend via HTTP requests, attaching the JWT token for authentication.
- Users are redirected to Login if not authenticated.
- After login, the UI adapts to the user's role.

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