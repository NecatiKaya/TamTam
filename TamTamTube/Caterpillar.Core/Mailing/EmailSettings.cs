using System;

namespace Caterpillar.Core.Mailing
{
    public class EmailSettings
    {
        public string Name { get; set; }

        public string FromEmailAddress { get; set; }

        public string SmtpAddress { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
