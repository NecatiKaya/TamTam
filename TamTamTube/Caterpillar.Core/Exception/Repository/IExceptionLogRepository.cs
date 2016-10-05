namespace Caterpillar.Core.Exception.Repository
{
    /// <summary>
    /// Base repository for exception logging.
    /// </summary>
    public interface IExceptionLogRepository
    {
        /// <summary>
        /// Inserts an exception to storage(databse, file) and returns inserted exception id.
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="informationAboutException">Exception to save</param>
        /// <returns></returns>
        T Log<T>(ExceptionInformation informationAboutException);
    }
}
