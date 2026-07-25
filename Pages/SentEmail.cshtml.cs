using Final_new.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_new.Pages
{
    public class SentEmailModel : PageModel
    {
        private readonly DemoDataService _demoData;

        public SentEmailModel(DemoDataService demoData)
        {
            _demoData = demoData;
        }

        public List<EmailInfo> listEmails = new List<EmailInfo>();

        public IActionResult OnPostReadEmail(string emailId)
        {
            TempData["EmailID"] = emailId;
            return RedirectToPage("/SentEmail");
        }

        public void OnGet()
        {
            string? loggedInUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(loggedInUsername))
            {
                loggedInUsername = "adminOFSD";
                HttpContext.Session.SetString("username", loggedInUsername);
            }

            var emails = _demoData.GetSent(loggedInUsername);
            foreach (var email in emails)
            {
                listEmails.Add(new EmailInfo
                {
                    EmailID = email.EmailID,
                    EmailSubject = email.EmailSubject,
                    EmailMessage = email.EmailMessage,
                    EmailDate = email.EmailDate,
                    EmailIsRead = email.EmailIsRead,
                    EmailSender = email.EmailSender,
                    EmailReceiver = email.EmailReceiver
                });
            }
        }

        public class EmailInfo
        {
            public string EmailID = string.Empty;
            public string EmailSubject = string.Empty;
            public string EmailMessage = string.Empty;
            public string EmailDate = string.Empty;
            public string EmailIsRead = string.Empty;
            public string EmailSender = string.Empty;
            public string EmailReceiver = string.Empty;
        }
    }
}