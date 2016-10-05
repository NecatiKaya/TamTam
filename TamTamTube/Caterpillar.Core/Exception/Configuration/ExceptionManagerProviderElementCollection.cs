using System.Configuration;
using System.Linq;

namespace Caterpillar.Core.Exception.Configuration
{
    /// <summary>
    /// Collection type for ExceptionManagerProviderElement type.
    /// </summary>
    [ConfigurationCollection(typeof(ExceptionManagerProviderElement))]
    public class ExceptionManagerProviderElementCollection : ConfigurationElementCollection
    {
        #region ConfigurationElementCollection Override

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExceptionManagerProviderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ExceptionManagerProviderElement el = (ExceptionManagerProviderElement)element;
            return (el.ProviderType + el.RepositoryType);
        }

        public new ExceptionManagerProviderElement this[string index]
        {
            get 
            {
                return this.OfType<ExceptionManagerProviderElement>().Where(exProvider => string.Equals(exProvider.ProviderType + exProvider.RepositoryType, index)).First();
                //return (ExceptionManagerProviderElement)BaseGet(index); 
            }
        }

        #endregion
    }
}
