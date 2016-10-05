
namespace Caterpillar.Core.Validation.RegEx
{
    /// <summary>
    /// Helper type to validate a string whether it only contains letter(s) or not.
    /// </summary>
    public class RegExOnlyLetterValidator : RegExValidatorBase
    {
         /// <summary>
        /// RegEx digit pattern to validate input that is thought to be only contains letter(s).
        /// </summary>
        private static readonly string _OnlyLetterPattern = @"^[a-zA-Z]+$";

        public RegExOnlyLetterValidator(string input)
            : base(input, _OnlyLetterPattern)
        {

        }
    }
}
