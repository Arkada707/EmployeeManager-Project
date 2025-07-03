// Pages/Employees/Employees.cshtml.cs
using EmployeeManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace EmployeeManager.UI.Pages.Employees
{
    public class EmployeesModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public List<Employee> Employees { get; set; } = new();

        [BindProperty] public Employee NewEmployee { get; set; } = new Employee
        {
            Name = string.Empty,
            Position = string.Empty,
            Salary = 0
        };
        [BindProperty] public Employee EditEmployee { get; set; } = new Employee
        {
            Name = string.Empty,
            Position = string.Empty,
            Salary = 0
        };
        [BindProperty] public int EditEmployeeId { get; set; } = -1;

        public bool IsAdmin { get; set; } = false;
        public bool IsViewer { get; set; } = false;

        public EmployeesModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private void AttachJwtToken(HttpClient client)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private IActionResult? RedirectIfNotAuthenticated()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }
            return null;
        }

        private void SetUserRoleFromToken()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var role = jwt.Claims.FirstOrDefault(c => c.Type == "role" || c.Type.EndsWith("/role"))?.Value;
                IsAdmin = role == "Admin";
                IsViewer = role == "Viewer";
            }
            else
            {
                IsAdmin = false;
                IsViewer = false;
            }
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            var redirect = RedirectIfNotAuthenticated();
            if (redirect != null) return redirect;
            SetUserRoleFromToken();
            ModelState.Clear();
            var client = _clientFactory.CreateClient("API");
            AttachJwtToken(client);
            var response = await client.GetAsync($"/api/employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Employee>();
                if (result != null)
                {
                    EditEmployee = result;
                    EditEmployeeId = result.Id; // track current edit
                    await LoadEmployeesAsync();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditConfirmAsync()
        {
            var redirect = RedirectIfNotAuthenticated();
            if (redirect != null) return redirect;
            SetUserRoleFromToken();
            var client = _clientFactory.CreateClient("API");
            AttachJwtToken(client);
            var response = await client.PutAsJsonAsync($"/api/employees/{EditEmployee.Id}", EditEmployee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage(); // refresh the list
            }
            return Page(); // handle failure
        }

        public async Task OnGetAsync()
        {
            var redirect = RedirectIfNotAuthenticated();
            if (redirect != null) { Response.Redirect("/Login"); return; }
            SetUserRoleFromToken();
            await LoadEmployeesAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            var redirect = RedirectIfNotAuthenticated();
            if (redirect != null) return redirect;
            SetUserRoleFromToken();
            var client = _clientFactory.CreateClient("API");
            AttachJwtToken(client);
            var content = new StringContent(JsonSerializer.Serialize(NewEmployee), Encoding.UTF8, "application/json");
            await client.PostAsync("api/employees", content);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var redirect = RedirectIfNotAuthenticated();
            if (redirect != null) return redirect;
            SetUserRoleFromToken();
            var client = _clientFactory.CreateClient("API");
            AttachJwtToken(client);
            await client.DeleteAsync($"api/employees/{id}");
            return RedirectToPage();
        }

        private async Task LoadEmployeesAsync()
        {
            var client = _clientFactory.CreateClient("API");
            AttachJwtToken(client);
            var response = await client.GetAsync("api/employees");
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<List<Employee>>(stream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                Employees = data ?? new List<Employee>();
            }
        }
    }
}
