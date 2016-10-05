namespace Caterpillar.Core.ResourceLocalization
{
    /// <summary>
    /// Base Application Service type for lecolization resources.
    /// </summary>
    public interface IResourceLocalizationService
    {
        /// <summary>
        /// Gets resource by key
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="throwExceptionIfResourceNotFound">Specify true to throw exception if specified resource is no found, othwerwise false. If this parameter is false nad resource is not found than a string containing key and language is returned s default.</param>
        /// <returns>Returns localized resource value.</returns>
        string GetResourceValue(string key, bool throwExceptionIfResourceNotFound = false);

        /// <summary>
        /// Gets resource by key and language Id
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="cultureName">Culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="throwExceptionIfResourceNotFound">Specify true to throw exception if specified resource is no found, othwerwise false. If this parameter is false nad resource is not found than a string containing key and language is returned s default.</param>
        /// <returns>Returns localized resource value.</returns>
        string GetResourceValue(string key, string cultureName, bool throwExceptionIfResourceNotFound = false);

        /// <summary>
        /// Gets resources specified pattern.
        /// </summary>
        /// <param name="pattern">Resource name pattern</param>
        /// <param name="throwExceptionIfResourceNotFound">Specify true to throw exception if specified resource is no found, othwerwise false. If this parameter is false nad resource is not found than a string containing key and language is returned s default.</param>
        /// <returns>Returns localized resource value.</returns>
        string[] GetResourceValueByPattern(string pattern, bool throwExceptionIfResourceNotFound = false);

        /// <summary>
        /// Gets resources specified pattern and language Id
        /// </summary>
        /// <param name="pattern">Resource name pattern</param>
        /// <param name="cultureName">culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="throwExceptionIfResourceNotFound">Specify true to throw exception if specified resource is no found, othwerwise false. If this parameter is false nad resource is not found than a string containing key and language is returned s default.</param>
        /// <returns>Returns localized resource value.</returns>
        string[] GetResourceValueByPattern(string pattern, string cultureName, bool throwExceptionIfResourceNotFound = false);

        /// <summary>
        /// Sets resource to resource localization service.
        /// </summary>
        /// <param name="resource"></param>
        void SetResource(LocalizedResource resource);

        ///// <summary>
        ///// Gets all resources from (db) store
        ///// </summary>
        ///// <returns></returns>
        //List<LocalizedResource> GetAllResource();

        /// <summary>
        /// Adds caches provided resources
        /// </summary>
        /// <param name="resources"></param>
        void SetResources(LocalizedResource[] resources);
    }
}
