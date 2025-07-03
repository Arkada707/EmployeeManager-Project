using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EmployeeManager.UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        public LoginModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty] public string Username { get; set; } = string.Empty;
        [BindProperty] public string Password { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient("API");
            var content = new StringContent(JsonSerializer.Serialize(new { Username, Password }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var token = doc.RootElement.GetProperty("token").GetString();
                HttpContext.Session.SetString("JWToken", token!);
                return RedirectToPage("/Employees/Employees");
            }
            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
} 