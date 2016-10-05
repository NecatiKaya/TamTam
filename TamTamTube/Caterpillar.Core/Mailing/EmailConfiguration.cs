using System;
using System.Configuration;

namespace Caterpillar.Core.Mailing
{
    public class EmailConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("SmtpAddress", IsRequired = true)]
        public string SmtpAddress
        {
            get
            {
                return Convert.ToString(this["SmtpAddress"]);
            }
            set
            {
                this["SmtpAddress"] = value;
            }
        }

        [ConfigurationProperty("Port", IsRequired = true)]
        public int Port
        {
            get
            {
                return Convert.ToInt32(this["Port"]);
            }
            set
            {
                this["Port"] = value;
            }
        }

        [ConfigurationProperty("From", IsRequired = true)]
        public string From
        {
            get
            {
                return Convert.ToString(this["From"]);
            }
            set
            {
                this["From"] = value;
            }
        }


        [ConfigurationProperty("EnableSsl", DefaultValue = true)]
        public bool EnableSsl
        {
            get
            {
                return Convert.ToBoolean(this["EnableSsl"]);
            }
            set
            {
                this["EnableSsl"] = value;
            }
        }

        [ConfigurationProperty("UseDefaultCredentials", DefaultValue = false)]
        public bool UseDefaultCredentials 
        {
            get
            {
                return Convert.ToBoolean(this["UseDefaultCredentials"]);
            }
            set
            {
                this["UseDefaultCredentials "] = value;
            }
        }

        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName
        {
            get
            {
                return Convert.ToString(this["UserName"]);
            }
            set
            {
                this["UserName"] = value;
            }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get
            {
                return Convert.ToString(this["Password"]);
            }
            set
            {
                this["Password"] = value;
            }
        }
    }
}
