using Caterpillar.Core.Exception.Configuration;
using System.Configuration;

namespace Caterpillar.Core.Application.Configuration
{
    /// <summary>
    /// Configuration section for application
    /// </summary>
    public class ApplicationConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the ErrorPageUrl configuration value.
        /// </summary>
        [ConfigurationProperty("errorPageUrl")]
        public string ErrorPageUrl
        {
            get
            {
                return (string)this["errorPageUrl"];
            }
            set
            {
                this["errorPageUrl"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Exception configuration in Application Settings section.
        /// </summary>
        [ConfigurationProperty("exception")]
        public ExceptionManagerConfigurationSection ExceptionSection 
        {
            get
            {
                return (ExceptionManagerConfigurationSection)this["exception"];
            }
            set
            {
                this["exception"] = value;
            }
        }
    }
}
