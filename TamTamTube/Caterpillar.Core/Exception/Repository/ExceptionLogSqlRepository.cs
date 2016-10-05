using System;

namespace Caterpillar.Core.Exception.Repository
{
    /// <summary>
    /// Provides Ms Sql database operations for exception logging.
    /// </summary>
    public class ExceptionLogSqlRepository : IExceptionLogRepository
    {
        #region IExceptionLogRepository

        /// <summary>
        /// Logs exception to database
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
