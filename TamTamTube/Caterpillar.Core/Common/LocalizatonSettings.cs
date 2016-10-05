
namespace Caterpillar.Core.Common
{
    /// <summary>
    /// Contains general settings for localization.
    /// </summary>
    public class LocalizatonSettings
    {
        /// <summary>
        /// Gets or sets culture name that is used in Caterpillar Framework. Generally this property has same value with TwoLetterISOLanguageName property.
        /// </summary>
        public string CaterpillarCultureName { get; set; }

        /// <summary>
        /// Gets or sets language name in format of languagecode2-country/regioncode2
        /// </summary>
        public string CultureName { get; set; }

        /// <summary>
        /// Gets or sets two letter language name in ISO 639-1format
        /// </summary>
        public string TwoLetterISOLanguageName { get; set; }
    }
}
