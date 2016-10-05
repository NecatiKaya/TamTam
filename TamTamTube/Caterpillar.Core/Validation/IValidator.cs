using Caterpillar.Core.Collections;

namespace Caterpillar.Core.Validation
{
    /// <summary>
    /// Generic validator interface. Used for validating a type.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Gets or sets validation errors
        /// </summary>
        StringToStringDictionary Errors { get; }

        /// <summary>
        /// Validates instance.
        /// </summary>
        /// <returns>Returns true if instance is valid. Otherwise, false.</returns>
        bool IsValid();
    }
}
