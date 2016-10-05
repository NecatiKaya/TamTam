using Caterpillar.Core.Application.Configuration;
using Caterpillar.Core.Cache;
using Caterpillar.Core.Exception;
using Caterpillar.Core.Mailing;
using Caterpillar.Core.ResourceLocalization;
using Caterpillar.Core.Session;
using System;
using System.Configuration;

namespace Caterpillar.Core.Application
{
    /// <summary>
    /// Contains application wide services.
    /// </summary>
    public class ApplicationFoundation
    {
        #region Private Variables

        private static readonly Lazy<ApplicationFoundation> _appFoundation = new Lazy<ApplicationFoundation>(() => new ApplicationFoundation(), true);
        private static ICaterpillarDependencyResolver _dependencyResolver;
        private static ICacheService _iCacheService;
        private static ISessionService _iSessionService;
        private static IMailingService _iMailingService;
        private static IResourceLocalizationService _iResourceLocalizationService;

        private static ApplicationSettings _Settings;
        private static ExceptionManager _ExceptionManager;
 
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current ApplicationFoundation object.
        /// </summary>
        public static ApplicationFoundation Current
        {
            get
            {
                return _appFoundation.Value;
            }
        }

        /// <summary>
        /// Gets or sets the settings used for an application.
        /// </summary>
        public static ApplicationSettings Settings 
        {
            get
            {
                return _Settings;
            }
            private set
            {
                _Settings = value;
            }
        }

        /// <summary>
        /// Gets or sets Exception Manager object to handle and manage system exceptions.
        /// </summary>
        public ExceptionManager ExceptionManager 
        {
            get
            {
                return _ExceptionManager;
            }
            set
            {
                _ExceptionManager = value;
            }
        }

        /// <summary>
        /// Gets cache service for the application.
        /// </summary>
        public ICacheService CacheService
        {
            get
            {
                if (_iCacheService == null)
                {
                    if (_dependencyResolver == null)
                    {
                        throw new CoreLevelException("Property _DependencyResolver is null. Please call static method of ApplicationFoundation.SetDependencyResolver when the application starts such as in global.asax file", new ArgumentNullException("_DependencyResolver"));
                    }

                    _iCacheService = _dependencyResolver.GetType<ICacheService>();
                }

                return _iCacheService;
            }
        }

        /// <summary>
        /// Gets session service for the application.
        /// </summary>
        public ISessionService SessionService
        {
            get
            {
                if (_iSessionService == null)
                {
                    if (_dependencyResolver == null)
                    {
                        throw new CoreLevelException("Property _DependencyResolver is null. Please call static method of ApplicationFoundation.SetDependencyResolver when the application starts such as in global.asax file", new ArgumentNullException("_DependencyResolver"));
                    }

                    _iSessionService = _dependencyResolver.GetType<ISessionService>();
                }

                return _iSessionService;
            }
        }

        /// <summary>
        /// Gets mailing service for the application.
        /// </summary>
        public IMailingService MailingService
        {
            get
            {
                if (_iMailingService == null)
                {
                    if (_dependencyResolver == null)
                    {
                        throw new CoreLevelException("Property _DependencyResolver is null. Please call static method of ApplicationFoundation.SetDependencyResolver when the application starts such as in global.asax file", new ArgumentNullException("_DependencyResolver"));
                    }

                    _iMailingService = _dependencyResolver.GetType<IMailingService>();
                }

                return _iMailingService;
            }
        }

        /// <summary>
        /// Gets resource localization service.
        /// </summary>
        public IResourceLocalizationService ResourceLocalization
        {
            get
            {
                if (_iResourceLocalizationService == null)
                {
                    if (_dependencyResolver == null)
                    {
                        throw new CoreLevelException("Property _DependencyResolver is null. Please call static method of ApplicationFoundation.SetDependencyResolver when the application starts such as in global.asax file", new ArgumentNullException("_DependencyResolver"));
                    }

                    _iResourceLocalizationService = _dependencyResolver.GetType<IResourceLocalizationService>();
                }

                return _iResourceLocalizationService;
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Sets dependency resolver to resolve services defined in ApplicationFoundation object instance.
        /// </summary>
        /// <param name="dependencyResolver"></param>
        public static void SetDependencyResolver(ICaterpillarDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        /// <summary>
        /// Sets settings property of ApplicationFoundation type from parameter <paramref name="settings"/>
        /// </summary>
        /// <param name="settings">Settings for application</param>
        public static void SetApplicationSettings(ApplicationSettings settings)
        {
            ApplicationFoundation.Settings = settings;
            ConfigureExceptionHandling();
        }

        /// <summary>
        /// Sets settings property of ApplicationFoundation type from application configuration file.
        /// </summary>
        /// <param name="settings">Settings for application</param>
        public static void SetApplicationSettings()
        {
            ApplicationSettings settings = new ApplicationSettings();
            ApplicationConfigurationSection appConfig = ConfigurationManager.GetSection("applicationConfiguration") as ApplicationConfigurationSection;
            if (appConfig != null)
            {
                settings.ErrorPageUrl = appConfig.ErrorPageUrl;
            }

            ApplicationFoundation.Settings = settings;
            ConfigureExceptionHandling();            
        }

        #endregion

        #region Private Methods

        private static void ConfigureExceptionHandling()
        {
            #region Exception Manager Configuration

            ExceptionManager exManager = new ExceptionManager();
            exManager.Configure();
            ApplicationFoundation.Current.ExceptionManager = exManager;

            #endregion
        }

        #endregion
    }
}
