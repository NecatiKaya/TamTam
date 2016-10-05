using System.Text.RegularExpressions;

namespace Caterpillar.Core.Helpers
{
    /// <summary>
    /// Regular Expression helpers.
    /// </summary>
    public class RegExHelper
    {
        #region Static Properties

        /// <summary>
        /// Gets only digit pattern for regex. Used for validating an input whether only containing digit(s) or not.
        /// </summary>
        public static readonly string OnlyDigitsPattern = @"^[0-9]+$";

        /// <summary>
        /// Gets only letter pattern for regex. Used for validating an input whether only containing letter(s) or not.
        /// </summary>
        public static readonly string OnlyLettersPattern = @"^[a-zA-Z]+$";

        /// <summary>
        /// Gets date pattern for regex. Used for validating an input whether is valid date or not. Date format is yyyy-mm-dd.
        /// </summary>
        public static readonly string DatePattern = @"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$";

        /// <summary>
        /// Gets valid email pattern for regex. Used for validating an input whether it is valid email address or not.
        /// </summary>
        public static readonly string EmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public static readonly string LengthPattern = "{6,10}";

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates input by pattern.
        /// </summary>
        /// <returns>Returns true if input is valid according to pattern.</returns>
        public static bool IsValid(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }
        
        #endregion
    }
}
