using System;

namespace Caterpillar.Core.Exception.Repository
{
    public class ExceptionLogXmlRepository : IExceptionLogRepository
    {
        #region IExceptionLogRepository

        /// <summary>
        /// Logs exception to an xml file
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
