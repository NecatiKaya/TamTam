using Caterpillar.Core.Application;
using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Exception
{
    /// <summary>
    /// Exception that is fired when an argument contains invalid character.
    /// </summary>
    [Serializable]
    public class ArgumentContainsInvalidCharacterException : ExceptionBase
    {
        #region Constructors

        public ArgumentContainsInvalidCharacterException()
            : base()
        {

        }

        public ArgumentContainsInvalidCharacterException(string message)
            : base(message)
        {

        }

        public ArgumentContainsInvalidCharacterException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }

        public ArgumentContainsInvalidCharacterException(SerializationInfo info, StreamingContext sc)
            : base(info, sc)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates ArgumentNullOrEmptyException with message from resx files.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <returns>Returns new instance of ExceptionBase.</returns>
        public new static ArgumentNullOrEmptyException CreateExceptionFromResource(string resourceKey, string cultureName)
        {
            string message = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(resourceKey, cultureName);
            ArgumentNullOrEmptyException argumentNullOrEmpty = new ArgumentNullOrEmptyException(message);
            return argumentNullOrEmpty;
        }

        /// <summary>
        /// Creates ArgumentNullOrEmptyException with message from resx files and inner exception.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="ex">Inner exception.</param>
        /// <returns>Returns new instance of ExceptionBase.</returns>
        public new static ArgumentNullOrEmptyException CreateExceptionFromResource(string resourceKey, string cultureName, System.Exception ex)
        {
            string message = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(resourceKey, cultureName);
            ArgumentNullOrEmptyException argumentNullOrEmpty = new ArgumentNullOrEmptyException(message, ex);
            return argumentNullOrEmpty;
        }

        /// <summary>
        /// Throws default argument null or exception message. 
        /// </summary>
        /// <param name="argumentName">Name of the argument throws the exception.</param>
        public new static void Throw(string argumentName)
        {
            throw new ArgumentNullOrEmptyException(string.Format(LanguageStrings.ArgumentNullExceptionMessageFormat, argumentName));
        }

        #endregion
    }
}
