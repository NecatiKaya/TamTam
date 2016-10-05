using Caterpillar.Core.Cache;
using Caterpillar.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Caterpillar.ApplicationServices.Cache
{
    /// <summary>
    /// Cache service that uses System.Runtime.Cache model.
    /// </summary>
    public class NetRuntimeCacheService : ICacheService
    {
        #region Global Variables

        /// <summary>
        /// Default memory cache.
        /// </summary>
        private static readonly ObjectCache _cache = MemoryCache.Default;

        #endregion

        #region ICacheService Implemantation

        /// <summary>
        /// Gets an object from cache by key
        /// </summary>
        /// <typeparam name="T">Type of the object to return</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="throwExceptionIfNotExists">If throwExceptionIfNotExists is true and cache item is not found in the cache throws exception</param>
        /// <returns>Returns object type of T</returns>
        public T Get<T>(string key, bool throwExceptionIfNotExists = false)
        {
            if (!_cache.Contains(key))
            {
                if (throwExceptionIfNotExists)
                {
                    throw new CoreLevelException("Cache item couldn't be found in cache.", new ArgumentNullException("Cache item couldn't be found in cache."));
                }

                return default(T);
            }

            return ((T)_cache[key]);
        }

        /// <summary>
        /// Gets an object from cache by key. If cache is null, cacheSetter function is used to set cache
        /// </summary>
        /// <typeparam name="T">Type of the object to return</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="cacheSetter">Used to set cache if item by the key is null</param>
        /// <param name="cacheTimeInSeconds">Duration which shows how mmuch time, in seconds, data will be stored in cache</param>
        /// <returns>Returns object type of T</returns>
        public T Get<T>(string key, Action cacheSetter, int cacheTimeInSeconds)
        {
            T cacheItem = (T)_cache[key];
            if (cacheItem == null)
            {
                if (cacheSetter != null)
                {
                    if (cacheTimeInSeconds > 0)
                    {
                        cacheSetter();
                    }
                }
            }

            return Get<T>(key);
        }

        /// <summary>
        /// Sets object in cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="data">Object to store in cache</param>
        /// <param name="cacheTimeInSeconds">Duration which shows how mmuch time, in seconds, data will be stored in cache</param>
        public void Set(string key, object data, int cacheTimeInSeconds)
        {
            if (_cache.Contains(key))
            {
                _cache[key] = data;
            }
            else
            {
                _cache.Add(key, data, DateTime.Now.AddSeconds(cacheTimeInSeconds));
            }
        }

        /// <summary>
        /// Gets all object which have spevified pattern as key.
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="pattern">Cache key pattern</param>
        /// <returns>Returns IEnumerable&lt;T&gt; objects.</returns>
        public IEnumerable<T> GetByPattern<T>(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return (from item in _cache where regex.IsMatch(item.Key) select (T) item.Value).ToList();
        }

        /// <summary>
        /// Checks wheter cahce contains key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Return true if key is in cache otherwise returns false</returns>
        public bool IsInCache(string key)
        {
            return _cache.Contains(key);
        }

        /// <summary>
        /// Gets all keys specified by pattern
        /// </summary>
        /// <param name="keyPattern"></param>
        /// <returns></returns>
        public IEnumerable<string> GetKeysByPattern(string keyPattern)
        {
            IEnumerable<string> keys = _cache.Where(item => item.Key.ToLower().Contains(keyPattern.ToLower())).Select(item => item.Key);
            return keys;
        }

        #endregion
    }
}
