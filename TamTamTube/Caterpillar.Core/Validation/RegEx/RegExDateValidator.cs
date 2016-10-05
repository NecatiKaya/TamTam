
namespace Caterpillar.Core.Validation.RegEx
{
    /// <summary>
    /// Helper type to validate a string whether it is a valid date or not.
    /// </summary>
    public class RegExDateValidator : RegExValidatorBase
    {
        /// <summary>
        /// RegEx email pattern to validate input that is thought to be valid date.
        /// </summary>
        private static readonly string _DatePattern = @"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$";

        public RegExDateValidator(string input)
            : base(input, _DatePattern)
        {

        }
    }
}
