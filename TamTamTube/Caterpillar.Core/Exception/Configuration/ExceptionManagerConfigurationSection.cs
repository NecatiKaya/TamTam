using System.Configuration;

namespace Caterpillar.Core.Exception.Configuration
{
    /// <summary>
    /// Configuration section for Exception Manager object defined in Caterpillar.Core.Exception namespace
    /// </summary>
    public class ExceptionManagerConfigurationSection : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the section in application configuration file.
        /// </summary>
        [ConfigurationProperty("providers", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ExceptionManagerProviderElementCollection), AddItemName = "provider", CollectionType=ConfigurationElementCollectionType.AddRemoveClearMap)]
        public ExceptionManagerProviderElementCollection Providers
        {
            get
            {
                return (ExceptionManagerProviderElementCollection)this["providers"]; 
            }
            set
            {
                this["providers"] = value; 
            }
        } 
    }
}
