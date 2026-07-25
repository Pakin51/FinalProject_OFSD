using Final_new.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_new.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly DemoDataService _demoData;

        public UserProfileModel(DemoDataService demoData)
        {
            _demoData = demoData;
        }

        public List<UserInfo> listUser = new List<UserInfo>();

        public void OnGet()
        {
            string? loggedInUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(loggedInUsername))
            {
                loggedInUsername = "adminOFSD";
                HttpContext.Session.SetString("username", loggedInUsername);
            }

            var user = _demoData.GetUser(loggedInUsername);
            listUser.Add(new UserInfo
            {
                UserID = "1",
                Username = user.Username,
                pwd = user.Password,
                fullname = $"{user.FirstName} {user.LastName}",
                position = user.Position,
                number = user.Phone
            });
        }

        public class UserInfo
        {
            public string UserID = string.Empty;
            public string Username = string.Empty;
            public string pwd = string.Empty;
            public string fullname = string.Empty;
            public string position = string.Empty;
            public string number = string.Empty;
        }
    }
}