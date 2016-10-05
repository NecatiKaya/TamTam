
namespace Caterpillar.Core.ResourceLocalization
{
    /// <summary>
    /// Localizable resource type that changes by languages.
    /// </summary>
    public class LocalizedResource
    {
        /// <summary>
        /// Gets or sets key of the resource which is used to find resource.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets language key of the resource.
        /// </summary>
        public short LanguageKey { get; set; }

        /// <summary>
        /// Gets the culture name in the format languagecode2-country/regioncode2.
        /// </summary>
        public string CultureName { get; set; }

        /// <summary>
        /// Gets or sets value of the resource.
        /// </summary>
        public string Value { get; set; }
    }
}
