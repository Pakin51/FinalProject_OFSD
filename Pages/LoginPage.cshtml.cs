using Final_new.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_new.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly DemoDataService _demoData;

        public LoginPageModel(DemoDataService demoData)
        {
            _demoData = demoData;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            // Default demo username suggestion
            if (string.IsNullOrEmpty(Username))
            {
                Username = "";
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                errorMessage = "Username is required.";
                return Page();
            }

            // Interactive demo validation
            if (_demoData.ValidateUser(Username, Password))
            {
                HttpContext.Session.SetString("username", Username);
                successMessage = "Login successful (Demo Mode)!";
                return RedirectToPage("/ViewEmail");
            }

            errorMessage = "Invalid login attempt.";
            return Page();
        }
    }
}