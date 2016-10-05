using System.Configuration;

namespace Caterpillar.Core.Exception.Configuration
{
    /// <summary>
    /// Configuration element for ExceptionManager provider.
    /// </summary>
    public class ExceptionManagerProviderElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the provider for exception logging.
        /// </summary>
        [ConfigurationProperty("providerType", IsRequired = true)]
        public string ProviderType
        {
            get
            {
                return (string)this["providerType"];
            }
            set
            {
                this["providerType"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the repository used by provider for exception logging.
        /// </summary>
        [ConfigurationProperty("repositoryType", IsRequired = true)]
        public string RepositoryType
        {
            get
            {
                return (string)this["repositoryType"];
            }
            set
            {
                this["repositoryType"] = value;
            }
        }
    }
}
