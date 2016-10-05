using System;

namespace Caterpillar.Core.Exception.Repository
{
    /// <summary>
    /// Mailing repository for logging exception.
    /// </summary>
    public class ExceptionLogNetMailRepository : IExceptionLogRepository
    {
        public ExceptionLogNetMailRepository()
        {

        }

        #region IExceptionLogRepository

        /// <summary>
        /// Sends email contains information about error and extra data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="informationAboutException"></param>
        /// <returns></returns>
        public T Log<T>(ExceptionInformation informationAboutException)
        {
            return default(T);
            //TODO: Add .NET mailing operations here.
            throw new NotImplementedException();
        }

        #endregion
    }
}
