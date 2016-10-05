using Caterpillar.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Threading;

namespace Caterpillar.Core.ResourceLocalization
{
    public class DefaultResourceLocalizationService : IResourceLocalizationService
    {
        #region Global Variables

        /// <summary>
        /// Default memory cache.
        /// </summary>
        private static ObjectCache Cache = MemoryCache.Default;

        #endregion

        #region Public Methods

        /// <summary>
        /// Add resources to cache.
        /// </summary>
        /// <param name="resourcesToAddToCache">Resources to cache</param>
        public static void AddLocalizableResourcesToCache(LocalizedResource[] resourcesToAddToCache)
        {
            if (resourcesToAddToCache != null && resourcesToAddToCache.Length > 0)
            {
                LocalizedResource resource = null;
                string cacheKey = null;
                for (int i = 0; i < resourcesToAddToCache.Length; i++)
                {
                    resource = resourcesToAddToCache[i];
                    cacheKey = GenerateKey(resource.Key, resource.CultureName);
                    Cache[cacheKey] = resource;
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Generates a cacheKey for a given resource name and culture
        /// </summary>
        /// <param name="resourceName">Name of the resource</param>
        /// <param name="cultureName">culture value</param>
        /// <returns></returns>
        private static string GenerateKey(string resourceName, string cultureName)
        {
            return string.Format("{0}##{1}", resourceName, cultureName);
        }

        #endregion

        #region IResourceLocalizationService Implemantation

        /// <summary>
        /// Gets resource by given key and Thread.CurrentThread.CurrentUICulture.Name
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="throwExceptionIfResourceNotFound">Specify true to throw exception if specified resource is no found, othwerwise false. If this parameter is false nad resource is not found than a string containing key and language is returned s default.</param>
        /// <returns>Returns localized resource value.</returns>
        public string GetResourceValue(string key, bool throwExceptionIfResourceNotFound = false)
        {
            string cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            return GetResourceValue(key, cultureName, throwExceptionIfResourceNotFound);
        }

        /// <summary>
        /// Gets resource by key and language Id
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="cultureName">culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="throwExceptionIfResourceNotFound">Specify true to throw exception if specified resource is no found, othwerwise false. If this parameter is false nad resource is not found than a string containing key and language is returned s default.</param>
        /// <returns>Returns localized resource value.</returns>
        public string GetResourceValue(string key, string cultureName, bool throwExceptionIfResourceNotFound = false)
        {
            string cacheKey = GenerateKey(key, cultureName);
            LocalizedResource resource = Cache.Get(cacheKey) as LocalizedResource;
            if (resource != null)
            {
                return resource.Value;
            }

            if (throwExceptionIfResourceNotFound)
            {
                return string.Format("{0}", cacheKey, cultureName);
            }

            return string.Format("<Resource with the name of '{0}' not found in culture '{1}'>", key, cultureName);
        }

        /// <summary>
        /// Gets resources specified pattern.
        /// </summary>
        /// <param name="pattern">Resource name pattern</param>
        /// <param name="throwExceptionIfResourceNotFound">Specify true to throw exception if specified resource is no found, othwerwise false. If this parameter is false nad resource is not found than a string containing key and language is returned s default.</param>
        /// <returns>Returns localized resource value.</returns>
        public string[] GetResourceValueByPattern(string pattern, bool throwExceptionIfResourceNotFound = false)
        {
            string cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            return GetResourceValueByPattern(pattern, cultureName, throwExceptionIfResourceNotFound);
        }

        /// <summary>
        /// Gets resources specified pattern and language Id
        /// </summary>
        /// <param name="pattern">Resource name pattern</param>
        /// <param name="cultureName">culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="throwExceptionIfResourceNotFound">Specify true to throw exception if specified resource is no found, othwerwise false. If this parameter is false nad resource is not found than a string containing key and language is returned s default.</param>
        /// <returns>Returns localized resource value.</returns>
        public string[] GetResourceValueByPattern(string pattern, string cultureName, bool throwExceptionIfResourceNotFound = false)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            IList<string> resultSet = new List<string>();

            foreach (KeyValuePair<string, object> eachItem in Cache)
            {
                if (regex.IsMatch(eachItem.Key))
                {
                    LocalizedResource resource = (LocalizedResource)eachItem.Value;
                    if (string.Equals(resource.CultureName, cultureName, StringComparison.OrdinalIgnoreCase))
                    {
                        resultSet.Add(resource.Value);
                    }
                }
            }

            if (resultSet.Count > 0)
            {
                return resultSet.ToArray();
            }

            if (throwExceptionIfResourceNotFound)
            {
                return new string[] { string.Format("<Resource with the pattern of '{0}' not found in culture '{1}'>", pattern, cultureName) };
            }

            return new string[] { string.Format("{0}", pattern, cultureName) };
        }

        public void SetResource(LocalizedResource resource)
        {
            if (resource == null)
            {
                throw new CoreLevelException("resource parameter can not be null.", new ArgumentNullException("resource"));
            }

            if (string.IsNullOrWhiteSpace(resource.CultureName))
            {
                throw new CoreLevelException(string.Format("Resource.CultureName can not be null. Resource key : {0}", resource.Key), new ArgumentNullException("resource.CultureName"));
            }

            string cacheKey = null;
            cacheKey = GenerateKey(resource.Key, resource.CultureName);
            Cache[cacheKey] = resource;
        }
        
        public void SetResources(LocalizedResource[] resources)
        {
            if (resources != null)
            {
                foreach (LocalizedResource eachResource in resources)
                {
                    SetResource(eachResource);
                }
            }
        }

        #endregion
    }
}
