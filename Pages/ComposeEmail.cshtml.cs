using Final_new.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_new.Pages
{
    public class ComposeEmailModel : PageModel
    {
        private readonly DemoDataService _demoData;
        private readonly ILogger<ComposeEmailModel> _logger;

        public EmailInfo emailInfo = new EmailInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public List<EmailInfo> Emails = new List<EmailInfo>();

        public ComposeEmailModel(ILogger<ComposeEmailModel> logger, DemoDataService demoData)
        {
            _logger = logger;
            _demoData = demoData;
        }

        public void OnGet()
        {
            string? sender = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(sender))
            {
                sender = "adminOFSD";
                HttpContext.Session.SetString("username", sender);
            }
            emailInfo.EmailSender = sender;
        }

        public IActionResult OnPost()
        {
            emailInfo.EmailSubject = Request.Form["subject"].ToString();
            emailInfo.EmailMessage = Request.Form["message"].ToString();
            emailInfo.EmailDate = DateTime.Now.ToString("yyyy-MM-dd");
            emailInfo.EmailIsRead = "N";
            emailInfo.EmailSender = HttpContext.Session.GetString("username") ?? "adminOFSD";
            emailInfo.EmailReciever = Request.Form["emailreciever"].ToString();

            if (string.IsNullOrWhiteSpace(emailInfo.EmailSubject) ||
                string.IsNullOrWhiteSpace(emailInfo.EmailReciever) ||
                string.IsNullOrWhiteSpace(emailInfo.EmailMessage))
            {
                errorMessage = "All fields are required.";
                return Page();
            }

            if (emailInfo.EmailMessage.Length > 100)
            {
                errorMessage = "Email message cannot exceed 100 characters.";
                return Page();
            }

            // Save to in-memory demo data store
            _demoData.AddEmail(new DemoEmail
            {
                EmailSubject = emailInfo.EmailSubject,
                EmailMessage = emailInfo.EmailMessage,
                EmailDate = emailInfo.EmailDate,
                EmailIsRead = emailInfo.EmailIsRead,
                EmailSender = emailInfo.EmailSender,
                EmailReceiver = emailInfo.EmailReciever
            });

            successMessage = "New Email was sent successfully (Demo Mode)!";
            return RedirectToPage("/SentEmail");
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
        public string EmailReciever = string.Empty;
    }
}