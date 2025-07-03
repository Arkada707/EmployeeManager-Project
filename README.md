# EmployeeManager

A simple .NET-based employee management system with authentication and role-based access.

[![Download ZIP](https://img.shields.io/badge/Download-ZIP-green?style=for-the-badge)](https://github.com/Arkada707/EmployeeManager/archive/refs/heads/main.zip)
[![Clone Repo](https://img.shields.io/badge/Clone-GitHub-blue?logo=github)](https://github.com/Arkada707/EmployeeManager)

---

## 📋 Table of Contents

- [Setup Instructions](#-setup-instructions)
- [Day 1 Summary](#net-developer-prep--day-1-summary-employee-management-app)
- [Day 2 Summary](#net-developer-prep--day-2-summary-authentication--authorization)


# 📦 Setup Instructions

This guide will help you get the **EmployeeManager** project up and running locally for testing, development, or demonstration purposes.

---

## ✅ Requirements

- [.NET SDK 9 (Preview)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
  Make sure to install the latest .NET 9 SDK for both API and UI to run successfully.

---

## 📁 Download the Project

You can get the source code in two ways:

### 🔽 Option A: Download as ZIP
1. Go to the [GitHub Repository](https://github.com/yourusername/EmployeeManager)
2. Click the green **`Code`** button
3. Select **Download ZIP**
4. Extract the contents to a local folder

### 🖥 Option B: Clone with GitHub Desktop
1. Open **GitHub Desktop**
2. Click **File > Clone Repository**
3. Paste the repo URL:  
   ```
   https://github.com/yourusername/EmployeeManager.git
   ```
4. Choose a local path to clone the project

---

## 💻 Run the Project

### Step 1: Open Terminal / Command Line

Open a terminal in the root project folder, or open separate terminals for API and UI.

---

### Step 2: Run the API

```bash
cd EmployeeManager.API
dotnet restore
dotnet run
```

Once running, access the API via:

```
https://localhost:5001/swagger
```

---

### Step 3: Run the UI

In a **new terminal window/tab**, run the UI:

```bash
cd EmployeeManager.UI
dotnet restore
dotnet run
```

Once running, access the UI via:

```
https://localhost:7012/Login
```

---

## 🔐 Trust HTTPS Certificate (Important!)

If you're on **macOS** or encounter HTTPS issues:

```bash
dotnet dev-certs https --trust
```

If you’ve run into issues before or changed machines, you can clean and re-trust:

```bash
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

---

## ✅ Login Credentials

Use the following accounts to test:

| Username | Password | Role   |
|----------|----------|--------|
| `admin`  | `1234`   | Admin  |
| `viewer` | `1234`   | Viewer |

---

## 📌 Useful URLs

| Feature        | URL                            |
|----------------|---------------------------------|
| API Swagger    | `https://localhost:5001/swagger` |
| UI Login Page  | `https://localhost:7012/Login`  |
| UI Employees   | `https://localhost:7012/Employees/Employees` |

---

Feel free to edit or expand this guide as needed for your team or demo!

```

```
# .NET Developer Prep — Day 2 Summary (Authentication & Authorization)

## 📅 Date: 2025-07-03

## 🎯 Goal

Implement functional JWT authentication and role-based access with login/logout support for the Employee Manager app.

---

## ✅ Completed Today

### 🛠️ Edit Feature Fix (Detailed)

**Problem:** Clicking "Edit" on a row cleared the entire employee list or caused the table to disappear. Submitting an edit caused a 400 Bad Request. ModelState validation errors triggered even without form submission.

**Cause:**

- Razor markup structured the `<form>` incorrectly across `<tr>` and `<td>`, violating HTML rules.
- Missing Anti-Forgery token in form.
- Razor Pages validation for `NewEmployee` was being incorrectly applied when editing due to shared ModelState.

**Fixes Applied:**

- **UI:** Refactored `Employees.cshtml` to:
  - Use `<td colspan="4">` when showing Edit form within a row.
  - Avoid form tag wrapping `<tr>` directly.
  - Add `@Html.AntiForgeryToken()` to Edit form.
  - Render Add and Edit forms separately and conditionally.
- **Backend:**
  - Added `ModelState.Clear()` in `OnPostEditAsync` to prevent validation errors from unrelated form fields.
  - Ensured `EditEmployeeId` is tracked to toggle the UI view for edit mode.

**Result:** Edit functionality now properly populates form with selected employee data, updates via PUT, and refreshes the list.

**Bonus:** ModelState validation messages now displayed clearly if something goes wrong.

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
