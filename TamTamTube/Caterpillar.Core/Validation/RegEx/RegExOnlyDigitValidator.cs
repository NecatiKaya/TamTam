
namespace Caterpillar.Core.Validation.RegEx
{
    /// <summary>
    /// Helper type to validate a string whether it only contains digit(s) or not.
    /// </summary>
    public class RegExOnlyDigitValidator : RegExValidatorBase
    {
        /// <summary>
        /// RegEx digit pattern to validate input that is thought to be only contains digits.
        /// </summary>
        private static readonly string _OnlyDigitPattern = @"^[0-9]+$";

        public RegExOnlyDigitValidator(string input)
            : base(input, _OnlyDigitPattern)
        {

        }
    }
}
