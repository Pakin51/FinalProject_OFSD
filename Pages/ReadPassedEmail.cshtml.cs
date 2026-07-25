using Final_new.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_new.Pages
{
    public class ReadPassedEmailModel : PageModel
    {
        private readonly DemoDataService _demoData;

        public ReadPassedEmailModel(DemoDataService demoData)
        {
            _demoData = demoData;
        }

        public ReadEmailModel.EmailInfoDetails? EmailDetails { get; set; }

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
                EmailDetails = new ReadEmailModel.EmailInfoDetails
                {
                    EmailID = email.EmailID,
                    EmailSubject = email.EmailSubject,
                    EmailSender = email.EmailSender,
                    EmailDate = email.EmailDate,
                    EmailMessage = email.EmailMessage
                };
            }
        }
    }
}
