using Final_new.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_new.Pages
{
    public class UserAccountModel : PageModel
    {
        private readonly DemoDataService _demoData;
        private readonly ILogger<UserAccountModel> _logger;

        public UserInfo userInfo = new UserInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public List<UserInfo> listUser = new List<UserInfo>();

        public UserAccountModel(ILogger<UserAccountModel> logger, DemoDataService demoData)
        {
            _logger = logger;
            _demoData = demoData;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            userInfo.username = Request.Form["username"].ToString();
            userInfo.pwd = Request.Form["pwd"].ToString();
            userInfo.fname = Request.Form["fname"].ToString();
            userInfo.lname = Request.Form["lname"].ToString();
            userInfo.jposition = Request.Form["jposition"].ToString();
            userInfo.numb = Request.Form["numb"].ToString();
            string confirmPwd = Request.Form["confirmPwd"].ToString();

            if (string.IsNullOrWhiteSpace(userInfo.username) || string.IsNullOrWhiteSpace(userInfo.pwd) ||
                string.IsNullOrWhiteSpace(userInfo.fname) || string.IsNullOrWhiteSpace(userInfo.lname) ||
                string.IsNullOrWhiteSpace(userInfo.jposition) || string.IsNullOrWhiteSpace(userInfo.numb))
            {
                errorMessage = "All fields are required.";
                return Page();
            }

            if (userInfo.pwd != confirmPwd)
            {
                errorMessage = "Passwords do not match.";
                return Page();
            }

            bool created = _demoData.AddUser(new DemoUser
            {
                Username = userInfo.username,
                Password = userInfo.pwd,
                FirstName = userInfo.fname,
                LastName = userInfo.lname,
                Position = userInfo.jposition,
                Phone = userInfo.numb
            });

            if (!created)
            {
                errorMessage = "Username already exists.";
                return Page();
            }

            successMessage = "Account created successfully (Demo Mode)!";
            return RedirectToPage("/LoginPage");
        }

        public class UserInfo
        {
            public string id = string.Empty;
            public string username = string.Empty;
            public string pwd = string.Empty;
            public string fname = string.Empty;
            public string lname = string.Empty;
            public string jposition = string.Empty;
            public string numb = string.Empty;
        }
    }
}