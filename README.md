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
  ```

/EmployeeManager ├── EmployeeManager.API └── EmployeeManager.UI

````
- Renamed `.csproj` file from `EmployeeManager.csproj` to `EmployeeManager.API.csproj`

### ✅ Backend (API)
- Implemented EmployeeController with basic CRUD operations
- Configured EF Core + SQLite in `Program.cs`
- Integrated Swagger for API testing
- Implemented JWT Bearer authentication (token not yet issued via login)

### ✅ Frontend (UI)
- Configured Razor Pages project
- Set up HttpClient to connect to API
- Displayed Employees in a table view
- Built a single-page CRUD interface:
- Add new employee
- Edit existing employee
- Delete employee

---

## 🐛 Issues & Troubleshooting

### ❌ Error: `dotnet-ef does not exist`
**Solution:** Installed EF Core tools using:
```bash
dotnet tool install --global dotnet-ef
````

### ❌ SSL Error on API requests

**Error:** `SSL_ERROR_RX_RECORD_TOO_LONG` / `UntrustedRoot` **Solution Options:**

1. Trusted ASP.NET Dev cert using:

```bash
dotnet dev-certs https --trust
```

2. Switched to `http://localhost:5000` for development

### ❌ Razor Page: Model Binding Error for Required Properties

**Error:**

```
Required member 'Name' must be set in the object initializer...
```

**Solution:** Initialized properties explicitly:

```csharp
NewEmployee = new Employee { Name = "", Position = "", Salary = 0 };
```

---

## 🛠 Tools Used

- IDE: Cursor
- Language: C#
- Framework: ASP.NET Core (.NET 9)
- API testing: Swagger UI
- Database: SQLite (via EF Core)

---

## 📌 Next Steps

1. Implement Bootstrap or CSS styling for cleaner UI
2. Add search + sort functionality to the employee table
3. Add JWT Login (email/username + password)
4. Add Admin/Viewer role handling
5. Protect routes and UI buttons based on roles
6. (Optional) Create WinForms version of the same frontend

---

## 🔥 Highlights

- Fully working full-stack CRUD from scratch
- Razor Pages calling protected API
- Clean project separation
- Debugged HTTPS, certificate, and C# 11 issues successfully

---

> Great progress today — next goal: secure login, role-based actions, and styling!

