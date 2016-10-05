using Caterpillar.Core.Application;
using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Exception
{
    /// <summary>
    /// Exception that is fired when an element is null or contains no sequences.
    /// </summary>
    [Serializable]
    public class ObjectNullException : ExceptionBase
    {
        #region Constructors

        public ObjectNullException()
            : base()
        {

        }

        public ObjectNullException(string message)
            : base(message)
        {

        }

        public ObjectNullException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }

        public ObjectNullException(SerializationInfo info, StreamingContext sc)
            : base(info, sc)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates ObjectNullException with message from resx files.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <returns>Returns new instance of ExceptionBase.</returns>
        public new static ObjectNullException CreateExceptionFromResource(string resourceKey, string cultureName)
        {
            string message = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(resourceKey, cultureName);
            ObjectNullException objectNull = new ObjectNullException(message);
            return objectNull;
        }

        /// <summary>
        /// Creates ObjectNullException with message from resx files and inner exception.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="ex">Inner exception.</param>
        /// <returns>Returns new instance of ExceptionBase.</returns>
        public new static ObjectNullException CreateExceptionFromResource(string resourceKey, string cultureName, System.Exception ex)
        {
            string message = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(resourceKey, cultureName);
            ObjectNullException objectNull = new ObjectNullException(message, ex);
            return objectNull;
        }

        /// <summary>
        /// Throws default object null or exception message. 
        /// </summary>
        /// <param name="objectName">Name of the object throws the exception.</param>
        public new static void Throw(string objectName)
        {
            throw new ObjectNullException(string.Format(LanguageStrings.ObjectNullExceptionMessageFormat, objectName));
        }

        #endregion
    }
}
