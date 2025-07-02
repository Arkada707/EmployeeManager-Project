# .NET Developer Prep — Day 1 Summary (Employee Management App)

## 📅 Date: 2025-07-02

## 🎯 Goal

Build a full-stack Employee Management System using:

- ASP.NET Core Web API (backend)
- Razor Pages (frontend)
- Cursor IDE (instead of VS Code)
- SQLite (database)
- JWT for authentication (planned)
- Role-based access (planned)

---

## ✅ Completed Today

### ✅ Project Structure

- Restructured project layout into:

```
/EmployeeManager
├── EmployeeManager.API
└── EmployeeManager.UI
```

- Renamed `.csproj` file from `EmployeeManager.csproj` to `EmployeeManager.API.csproj`

### ✅ Backend (API)

- Implemented `EmployeeController` with full CRUD operations.
- Configured Entity Framework Core with SQLite.
- Added Swagger support in development mode.
- Implemented JWT Bearer authentication (basic setup).

**Commands used:**

```bash
dotnet new webapi -n EmployeeManager.API
cd EmployeeManager.API
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

### ✅ Frontend (UI)

- Created Razor Pages project and connected to the API.
- Set up HTTP client with JWT auth and base URL.
- Displayed employees in a table.
- Integrated add, edit, delete (CRUD) operations on a single page.

**Commands used:**

```bash
dotnet new razor -n EmployeeManager.UI
cd EmployeeManager.UI
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet run
```

### ✅ Other Setup

- Used `launchSettings.json` to align ports (API: 5000, UI: 5155).
- Configured `Program.cs` in both projects for routing, auth, services.

---

## 🐛 Issues & Troubleshooting

### ❌ `dotnet-ef does not exist`

**Cause:** EF tools not installed globally. **Solution:**

```bash
dotnet tool install --global dotnet-ef
```

---

### ❌ SSL Error (`SSL_ERROR_RX_RECORD_TOO_LONG`, `UntrustedRoot`)

**Cause:** HTTPS not trusted locally **Solution:**

```bash
dotnet dev-certs https --trust
```

Or switch API base URL to `http://localhost:5000`

---

### ❌ Razor Page Binding Error (Required Fields)

**Error:**

```
Required member 'Name' must be set in the object initializer...
```

**Cause:** C# 11 `required` fields not initialized **Solution:** Manually initialize properties in code:

```csharp
NewEmployee = new Employee { Name = "", Position = "", Salary = 0 };
EditEmployee = new Employee { Name = "", Position = "", Salary = 0 };
```

---

### ❌ Cursor IDE crash when pushing to GitHub

**Solution:** Used GitHub Desktop and CLI instead:

```bash
git init
git add .
git commit -m "Initial commit"
git remote add origin https://github.com/USERNAME/REPO.git
git branch -M main
git push -u origin main
```

---

### ❌ Namespace not found: 'Employee'

**Cause:** `using EmployeeManager.Models;` missing in Razor Page or project references not aligned. **Solution:**

- Added correct using statement.
- Ensured `EmployeeManager.API` is compiled correctly.

---

## 🛠 Tools Used

- IDE: Cursor
- Language: C#
- Framework: ASP.NET Core (.NET 8 / .NET 9)
- API testing: Swagger UI
- Database: SQLite (via EF Core)
- Source Control: Git, GitHub

---

## 📌 Next Steps

1. Add Bootstrap or Tailwind CSS for better UI
2. Implement search and sort on the employee table
3. Add JWT login UI (form + token handling)
4. Add Admin/Viewer role-based access control
5. Protect UI actions based on user roles
6. (Optional) Build a WinForms version for local desktop GUI

---

## 🔥 Highlights

- Full-stack CRUD completed with API and Razor Pages
- Cleanly separated UI and backend logic
- All key setup, errors, and integrations resolved
- Solid foundation for adding JWT, roles, and advanced UI

---

> 🚀 Great progress today. Next step: make it secure, styled, and user-aware!

