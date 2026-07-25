using System.Collections.Concurrent;

namespace Final_new.Services
{
    public class DemoUser
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class DemoEmail
    {
        public string EmailID { get; set; } = string.Empty;
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string EmailDate { get; set; } = string.Empty;
        public string EmailIsRead { get; set; } = "N";
        public string EmailSender { get; set; } = string.Empty;
        public string EmailReceiver { get; set; } = string.Empty;
    }

    public class DemoDataService
    {
        private readonly List<DemoUser> _users = new();
        private readonly List<DemoEmail> _emails = new();
        private int _nextEmailId = 100;

        public DemoDataService()
        {
            InitializeDefaultData();
        }

        private void InitializeDefaultData()
        {
            // Seed Default Demo Users
            _users.Add(new DemoUser
            {
                Username = "adminOFSD",
                Password = "ofsd_1234",
                FirstName = "Admin",
                LastName = "System",
                Position = "Lead Developer",
                Phone = "081-234-5678"
            });

            _users.Add(new DemoUser
            {
                Username = "alex_dev",
                Password = "password123",
                FirstName = "Alex",
                LastName = "Taylor",
                Position = "Full Stack Engineer",
                Phone = "089-876-5432"
            });

            _users.Add(new DemoUser
            {
                Username = "sarah_m",
                Password = "password123",
                FirstName = "Sarah",
                LastName = "Miller",
                Position = "Project Manager",
                Phone = "082-111-2233"
            });

            // Seed Initial Emails
            _emails.Add(new DemoEmail
            {
                EmailID = "1",
                EmailSubject = "Welcome to the Webmail Interactive Demo",
                EmailMessage = "This web application is running in Interactive Demo Mode. All database features are simulated in-memory.",
                EmailDate = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"),
                EmailIsRead = "N",
                EmailSender = "alex_dev",
                EmailReceiver = "adminOFSD"
            });

            _emails.Add(new DemoEmail
            {
                EmailID = "2",
                EmailSubject = "Sprint Planning Sync",
                EmailMessage = "Hi Admin, please review the sprint backlog before our upcoming review meeting.",
                EmailDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                EmailIsRead = "Y",
                EmailSender = "sarah_m",
                EmailReceiver = "adminOFSD"
            });

            _emails.Add(new DemoEmail
            {
                EmailID = "3",
                EmailSubject = "Project Architecture Guidelines",
                EmailMessage = "Attached is the overview for our ASP.NET Core Razor Pages frontend layout.",
                EmailDate = DateTime.Now.ToString("yyyy-MM-dd"),
                EmailIsRead = "N",
                EmailSender = "adminOFSD",
                EmailReceiver = "alex_dev"
            });
        }

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            // In demo mode, allow valid login for default users or any non-empty password
            var user = _users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                return true;
            }
            // Allow any demo user input
            return true;
        }

        public DemoUser GetUser(string username)
        {
            var user = _users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            if (user != null) return user;

            return new DemoUser
            {
                Username = username,
                Password = "demo_password",
                FirstName = username,
                LastName = "(Demo User)",
                Position = "Webmail Demo User",
                Phone = "080-000-0000"
            };
        }

        public bool AddUser(DemoUser newUser)
        {
            if (_users.Any(u => string.Equals(u.Username, newUser.Username, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            _users.Add(newUser);
            return true;
        }

        public List<DemoEmail> GetInbox(string username)
        {
            return _emails
                .Where(e => string.Equals(e.EmailReceiver, username, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(e => e.EmailID)
                .ToList();
        }

        public List<DemoEmail> GetSent(string username)
        {
            return _emails
                .Where(e => string.Equals(e.EmailSender, username, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(e => e.EmailID)
                .ToList();
        }

        public DemoEmail? GetEmailById(string id)
        {
            return _emails.FirstOrDefault(e => e.EmailID == id);
        }

        public void AddEmail(DemoEmail email)
        {
            _nextEmailId++;
            email.EmailID = _nextEmailId.ToString();
            _emails.Add(email);
        }

        public void MarkAsRead(string id)
        {
            var email = GetEmailById(id);
            if (email != null)
            {
                email.EmailIsRead = "Y";
            }
        }

        public void DeleteEmail(string id)
        {
            var email = GetEmailById(id);
            if (email != null)
            {
                _emails.Remove(email);
            }
        }
    }
}
