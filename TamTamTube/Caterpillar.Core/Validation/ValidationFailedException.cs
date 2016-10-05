using Caterpillar.Core.Exception;
using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Validation
{
    /// <summary>
    /// Exception that is thrown when validating an object such ui objects, db objects etc...
    /// </summary>
    [Serializable]
    public class ValidationFailedException : UserLevelException
    {
        public ValidationFailedException() : base()
        {

        }

        public ValidationFailedException(string message) 
            : base(message)
        {

        }

        public ValidationFailedException(string message, System.Exception innerException) 
            : base(message, innerException)
        {

        }

        public ValidationFailedException(SerializationInfo info, StreamingContext sc)
            : base(info , sc)
        {

        }

        #region Public Methods

        /// <summary>
        /// Throws default validation exception message. 
        /// </summary>
        /// <param name="argumentName">Name of the argument throws the exception.</param>
        public new static void Throw(string argumentName)
        {
            throw new ValidationFailedException(string.Format(LanguageStrings.ValidationExceptionMessageFormat, argumentName));
        }

        #endregion
    }
}
