using Caterpillar.Core.Exception.Repository;

namespace Caterpillar.Core.Exception.ExceptionLoggingProviders
{
    /// <summary>
    /// Base type for logging exception to a storage
    /// </summary>
    public abstract class ExceptionLoggingProviderBase
    {
        #region Properties
        
        /// <summary>
        /// Gets or sets repository for exception logging.
        /// </summary>
        public IExceptionLogRepository ExceptionLogRepository { get; set; }

        #endregion

        #region Constructors

        public ExceptionLoggingProviderBase(IExceptionLogRepository repository)
        {
            ExceptionLogRepository = repository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs exception with an extra data to storage.
        /// </summary>
        /// <typeparam name="T">Type of Id for recorded exception</typeparam>
        /// <param name="informationAboutException">Exception to log.</param>
        public abstract T Log<T>(ExceptionInformation informationAboutException); 

        #endregion
    }
}
