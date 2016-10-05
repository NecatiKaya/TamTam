using Caterpillar.Core.Exception.Repository;

namespace Caterpillar.Core.Exception.ExceptionLoggingProviders
{
    public class ExceptionLoggingMailProvider : ExceptionLoggingProviderBase
    {
        #region Constructor

        public ExceptionLoggingMailProvider(IExceptionLogRepository logRepository)
            : base(logRepository)
        {
                       
        }

        #endregion

        #region ExceptionLoggingProviderBase Implementation

        /// <summary>
        /// Logs exception with an extra data via email.
        /// </summary>
        /// <typeparam name="T">Type of Id of recorded exception</typeparam>
        /// <param name="informationAboutException">Exception to log.</param>
        public override T Log<T>(ExceptionInformation informationAboutException)
        {
            T id = default(T);
            id = this.ExceptionLogRepository.Log<T>(informationAboutException);
            return id;
        }

        #endregion
    }
}
