
namespace Caterpillar.Core.Validation.RegEx
{
    /// <summary>
    /// Helper type to validate a string whether it has minimum length of 'n'
    /// </summary>
    public class RegExMinimumLengthValidator : RegExValidatorBase
    {
        /// <summary>
        /// RegEx email pattern to validate input that is thought to be valid date.
        /// </summary>
        private static readonly string _MinimumLengthPattern = @"{6,10}";

        public int MinimumLength { get; set; }

        public RegExMinimumLengthValidator(string input, int minimumLength)
            : base(input, _MinimumLengthPattern)
        {
            
        }
    }
}
