using System;
using System.Collections.Generic;

namespace Caterpillar.Core.Cache
{
    /// <summary>
    /// Base application wide session service. Implement this application service interface for session services used in projects.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets an object from cache by key. If cache is null, cacheSetter function is used to set cache
        /// </summary>
        /// <typeparam name="T">Type of the object to return</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="throwExceptionIfNotExists">If throwExceptionIfNotExists is true and cache item is not found in the cache throws exception</param>
        /// <returns>Returns object type of T</returns>
        T Get<T>(string key, bool throwExceptionIfNotExists = false);

        /// <summary>
        /// Gets an object from cache by key. If cache is null, cacheSetter function is used to set cache
        /// </summary>
        /// <typeparam name="T">Type of the object to return</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="cacheSetter">Used to set cache if item by the key is null</param>
        /// <param name="cacheTimeInSeconds">Duration which shows how mmuch time, in seconds, data will be stored in cache</param>
        /// <returns>Returns object type of T</returns>
        T Get<T>(string key, Action cacheSetter, int cacheTimeInSeconds);

        /// <summary>
        /// Sets object in cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="data">Object to store in cache</param>
        /// <param name="cacheTimeInSeconds">Duration which shows how mmuch time, in seconds, data will be stored in cache</param>
        void Set(string key, object data, int cacheTimeInSeconds);

        /// <summary>
        /// Gets all object which have spevified pattern as key.
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="pattern">Cache key pattern</param>
        /// <returns>Returns IEnumerable&lt;T&gt; objects.</returns>
        IEnumerable<T> GetByPattern<T>(string pattern);

        /// <summary>
        /// Checks wheter cahce contains key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Return true if key is in cache otherwise returns false</returns>
        bool IsInCache(string key);

        /// <summary>
        /// Gets all keys specified by pattern
        /// </summary>
        /// <param name="keyPattern"></param>
        /// <returns></returns>
        IEnumerable<string> GetKeysByPattern(string keyPattern);
    }
}
