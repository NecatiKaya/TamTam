using Caterpillar.Core.Application;
using Caterpillar.Core.Session;

namespace Caterpillar.Core.Extensions
{
    /// <summary>
    /// Contains extension methods for string type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if specified string has value other then null, empty or whitespace.
        /// </summary>
        /// <param name="value">String to check</param>
        /// <returns>Returns true if specified string has value other then null, empty or whitespace. Othwerwise, false.</returns>
        public static bool IsNotNullOrWhitespace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Returns true if specified string has value null, empty or whitespace.
        /// </summary>
        /// <param name="value">>Returns true if specified string has value null, empty or whitespace. Othwerwise, false.</param>
        /// <returns></returns>
        public static bool IsNullOrWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Localize given key by specified culture name
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <returns>Returns localized resource value.</returns>
        public static string Localize(this string key)
        {
            BasicSessionObject sessionObj = ApplicationFoundation.Current.SessionService.GetBasicSessionObject();
            string cultureName = sessionObj.Culture;
            string localizedValue = key.Localize(cultureName);
            return localizedValue;
        }

        /// <summary>
        /// Localize given key by specified culture name
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="cultureName">Culture name in format of languagecode2-country/regioncode2.</param>
        /// <returns>Returns localized resource value.</returns>
        public static string Localize(this string key, string cultureName)
        {
            string localizedValue = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(key, cultureName);
            return localizedValue;
        }
    }
}
