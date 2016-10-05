using Caterpillar.Core.Exception.Repository;
using System.Threading;

namespace Caterpillar.Core.Exception.ExceptionLoggingProviders
{
    public class ExceptionLoggingAsyncMailProvider : ExceptionLoggingProviderBase
    {
        #region Constructor

        public ExceptionLoggingAsyncMailProvider(IExceptionLogRepository logRepository)
            : base(logRepository)
        {

        }

        #endregion

        #region ExceptionLoggingProviderBase Implementation

        /// <summary>
        /// Logs exception with an extra data via async email operation.
        /// </summary>
        /// <typeparam name="T">Type of Id of recorded exception</typeparam>
        /// <param name="informationAboutException">Exception to log.</param>
        public override T Log<T>(ExceptionInformation informationAboutException)
        {
            T id = default(T);
            ThreadPool.QueueUserWorkItem(o =>
            {
                id = this.ExceptionLogRepository.Log<T>(informationAboutException);
            });

            return id;
        }

        #endregion
    }
}
