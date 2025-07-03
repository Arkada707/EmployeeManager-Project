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

> 🚀 Great progress today. Next step: make it secure, styled, and user-aware!

---

# .NET Developer Prep — Day 2 Summary (Authentication & Authorization)

## 📅 Date: 2025-07-03

## 🎯 Goal

Implement functional JWT authentication and role-based access with login/logout support for the Employee Manager app.

---

## ✅ Completed Today

### ✅ JWT Authentication

- Created `/api/auth/login` endpoint in API with hardcoded users:
  - `admin` / `1234`
  - `viewer` / `1234`
- Issued JWT token on successful login with user role in the claims.
- Used 32+ character secret key to avoid token generation errors.

### ✅ API Protection

- Protected `EmployeeController` endpoints using `[Authorize]` attribute.
- Configured JWT authentication in `Program.cs`:

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("MY_SUPER_SECRET_KEY_12345678901234567890"))
        };
    });

app.UseAuthentication();
app.UseAuthorization();
```

### ✅ UI Login & Session Handling

- Created login page in Razor Pages.
- Stored token in session upon successful login.
- Parsed JWT from session to extract user role.
- Conditionally rendered UI (e.g., hide Edit/Delete for Viewer).

### ✅ Authorization Logic

- Used `IsAdmin` and `IsViewer` flags in Razor code-behind.
- Redirected to Login page if token not found in session.
- Logout clears session and redirects.

### ✅ Navigation

- Redirected root `/` to `/Employees/Employees`.
- Removed Home and Privacy links for clarity.
- Added Logout link to Employees page.

---

## 🐛 Issues & Fixes

### ❌ JWT key too short

**Error:**

```
IDX10720: Encryption key must be at least 256 bits
```

**Fix:** Extended key to 32+ characters

---

### ❌ Duplicate Bearer Authentication Scheme

**Fix:** Removed second `AddAuthentication("Bearer")` block in `Program.cs`

---

### ❌ UI Edit Feature Bug

**Problem:** Edit button cleared employee list or triggered errors  
**Fix:** Cleaned Razor markup, added `ModelState.Clear()`, and fixed form bindings

---

### ❌ Session token not used consistently

**Fix:** Wrapped all UI API calls to use the session token in request headers

---

## ✅ Checklist (Updated)

- [x] Setup project structure and dependencies
- [x] Build API with full CRUD
- [x] Connect Razor UI to API
- [x] Implement create/edit/delete on same page
- [x] Add JWT login with hardcoded users
- [x] Implement role-based UI (Admin/Viewer)
- [x] Protect API with `[Authorize]`
- [x] Redirect unauthenticated users
- [x] Add logout support
- [ ] Add Bootstrap or Tailwind styling
- [ ] Add search and sort
- [ ] Hook to a real DB with user table (optional)

---

> ✅ Day 2 wraps up secure access and role protection — ready for polishing and enhancements next!