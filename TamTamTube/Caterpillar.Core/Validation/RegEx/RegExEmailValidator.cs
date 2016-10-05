
namespace Caterpillar.Core.Validation.RegEx
{
    /// <summary>
    /// Helper type to validate a string whether it is a valid email or not.
    /// </summary>
    public class RegExEmailValidator : RegExValidatorBase
    {
        /// <summary>
        /// RegEx email pattern to validate input that is thought to be valid email address.
        /// </summary>
        private static readonly string _EmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public RegExEmailValidator(string input)
            : base(input, _EmailPattern)
        {

        }
    }
}
