using Caterpillar.Core.Application;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Exception
{
    public class MissingResourceException : System.Exception
    {
        #region Constructors

        public MissingResourceException()
            : base()
        {

        }

        public MissingResourceException(string message)
            : base(message)
        {

        }

        public MissingResourceException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }

        public MissingResourceException(SerializationInfo info, StreamingContext sc)
            : base(info, sc)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates MissingResourceException with message from resx files.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <returns>Returns new instance of ExceptionBase.</returns>
        public new static MissingResourceException CreateExceptionFromResource(string resourceKey, string cultureName)
        {
            string message = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(resourceKey, cultureName);
            MissingResourceException argumentNullOrEmpty = new MissingResourceException(message);
            return argumentNullOrEmpty;
        }

        /// <summary>
        /// Creates MissingResourceException with message from resx files and inner exception.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="ex">Inner exception.</param>
        /// <returns>Returns new instance of ExceptionBase.</returns>
        public new static MissingResourceException CreateExceptionFromResource(string resourceKey, string cultureName, System.Exception ex)
        {
            string message = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(resourceKey, cultureName);
            MissingResourceException argumentNullOrEmpty = new MissingResourceException(message, ex);
            return argumentNullOrEmpty;
        }

        /// <summary>
        /// Throws default argument null or exception message. 
        /// </summary>
        /// <param name="errorMessage"></param>
        public new static void Throw(string errorMessage)
        {
            throw new MissingResourceException(errorMessage);
        }

        #endregion
    }
}
