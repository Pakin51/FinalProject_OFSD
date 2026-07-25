using Final_new.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_new.Pages
{
    public class ReadEmailModel : PageModel
    {
        private readonly DemoDataService _demoData;

        public ReadEmailModel(DemoDataService demoData)
        {
            _demoData = demoData;
        }

        public EmailInfoDetails? EmailDetails { get; set; }

        public void OnGet(string emailId)
        {
            if (string.IsNullOrEmpty(emailId))
            {
                emailId = TempData["EmailID"]?.ToString() ?? string.Empty;
            }

            if (!string.IsNullOrEmpty(emailId))
            {
                FetchEmailDetails(emailId);
            }
        }

        private void FetchEmailDetails(string emailId)
        {
            var email = _demoData.GetEmailById(emailId);
            if (email != null)
            {
                _demoData.MarkAsRead(emailId);
                EmailDetails = new EmailInfoDetails
                {
                    EmailID = email.EmailID,
                    EmailSubject = email.EmailSubject,
                    EmailSender = email.EmailSender,
                    EmailDate = email.EmailDate,
                    EmailMessage = email.EmailMessage
                };
            }
        }

        public class EmailInfoDetails
        {
            public string EmailID { get; set; } = string.Empty;
            public string EmailSubject { get; set; } = string.Empty;
            public string EmailSender { get; set; } = string.Empty;
            public string EmailDate { get; set; } = string.Empty;
            public string EmailMessage { get; set; } = string.Empty;
        }
    }
}
