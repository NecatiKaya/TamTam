using Caterpillar.Core.Application.Configuration;
using Caterpillar.Core.Collections;
using Caterpillar.Core.Exception.Configuration;
using Caterpillar.Core.Exception.ExceptionLoggingProviders;
using Caterpillar.Core.Exception.Repository;
using Caterpillar.Core.Reflection;
using System.Collections.Generic;
using System.Configuration;

namespace Caterpillar.Core.Exception
{
    /// <summary>
    /// Contains methods to manage exception
    /// </summary>
    public class ExceptionManager
    {
        #region Global Variables

        /// <summary>
        /// Gets the ExceptionManager section in application configuration file.
        /// </summary>
        private static ApplicationConfigurationSection _ApplicationConfigurationSection = ConfigurationManager.GetSection("applicationSettings") as ApplicationConfigurationSection;

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets whether object is configured so that handling (logging, emailing etc...) of exception is possible
        /// </summary>
        public bool IsConfigured { get; set; }

        /// <summary>
        /// Gets or sets expetion logging providers.
        /// </summary>
        public ExceptionLoggingProviderBase[] ExceptionLoggingProviders { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Configures exception manager to manage exceptions.
        /// </summary>
        public void Configure()
        {
            if (_ApplicationConfigurationSection != null && _ApplicationConfigurationSection.ExceptionSection != null)
            {
                if (_ApplicationConfigurationSection.ExceptionSection.Providers != null && _ApplicationConfigurationSection.ExceptionSection.Providers.Count > 0)
                {
                    List<ExceptionLoggingProviderBase> exceptionLoggingProviders = new List<ExceptionLoggingProviderBase>();
                    IExceptionLogRepository tempRepository = null;
                    ExceptionLoggingProviderBase tempProviderBase = null;
                    try
                    {
                        foreach (ExceptionManagerProviderElement eachProviderElement in _ApplicationConfigurationSection.ExceptionSection.Providers)
                        {
                            tempRepository = (IExceptionLogRepository)ReflectionHelper.CreateInstance(eachProviderElement.RepositoryType.Trim(), null);
                            tempProviderBase = (ExceptionLoggingProviderBase)ReflectionHelper.CreateInstance(eachProviderElement.ProviderType.Trim(), tempRepository);
                            if (tempProviderBase != null)
                            {
                                exceptionLoggingProviders.Add(tempProviderBase);
                            }
                        }

                        this.ExceptionLoggingProviders = exceptionLoggingProviders.ToArray();
                        exceptionLoggingProviders.Clear();
                        exceptionLoggingProviders = null;
                        this.IsConfigured = true;
                    }
                    catch (System.Exception ex)
                    {
                        this.ExceptionLoggingProviders = null;
                        if (exceptionLoggingProviders != null)
                        {
                            exceptionLoggingProviders.Clear();
                        }                        
                        exceptionLoggingProviders = null;
                        this.IsConfigured = false;

                        CoreLevelException.Throw(string.Format(LanguageStrings.ExceptionManagingTypesCreationError, ".Net exception message : {0}, .Net Stack trace : {1}", ex.Message, ex.StackTrace));
                    }
                }
            }
        }

        /// <summary>
        /// Logs exception to storages provided by ExceptionLoggingProviders
        /// </summary>
        /// <param name="informationAboutException">Exception to log.</param>
        /// <returns>Returns all id list from all exception providers in object list collection.</returns>
        public ObjectListCollection Log(ExceptionInformation informationAboutException)
        {
            if (!this.IsConfigured)
            {
                CoreLevelException.Throw(LanguageStrings.ExceptionManagerIsNotConfiguredMessage);
            }

            ObjectListCollection objectIds = new ObjectListCollection();
            object tempId = null;
            foreach (ExceptionLoggingProviderBase eachExceptionLogger in ExceptionLoggingProviders)
            {
                tempId = eachExceptionLogger.Log<object>(informationAboutException);
                objectIds.Add(tempId);
            }

            return objectIds;
        }

        #endregion
    }
}
