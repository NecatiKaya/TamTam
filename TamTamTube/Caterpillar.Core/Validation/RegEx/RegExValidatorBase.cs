using System.Text.RegularExpressions;

namespace Caterpillar.Core.Validation.RegEx
{
    public abstract class RegExValidatorBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets input to validate.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Gets or sets regex pattern to validate input.
        /// </summary>
        public string Pattern { get; set; }

        #endregion

        #region Constructors

        public RegExValidatorBase(string input, string pattern)
        {
            Input = input;
            Pattern = pattern;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates input by pattern.
        /// </summary>
        /// <returns>Returns true if the input is validated according to pattern.</returns>
        public bool Validate() 
        {
            return Regex.IsMatch(Input, Pattern);
        }

        #endregion
    }
}
