namespace Caterpillar.Core.Application
{
    /// <summary>
    /// DependencyResolver managing Inversion of Control (Dependency Injection)
    /// </summary>
    public interface ICaterpillarDependencyResolver
    {
        /// <summary>
        /// Gets service (usually application wide service such as cache service, session service etc...)
        /// </summary>
        /// <typeparam name="T">Type of the service to return</typeparam>
        /// <returns>Returns service as an instance of object</returns>
        T GetType<T>();
    }
}
