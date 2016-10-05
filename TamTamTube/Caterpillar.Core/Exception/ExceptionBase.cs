using Caterpillar.Core.Application;
using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Exception
{
    /// <summary>
    /// Base exception in Caterpillar Framework.
    /// </summary>
    [Serializable]
    public class ExceptionBase : System.Exception
    {
        #region Constructors
        
        public ExceptionBase()
            : base()
        {

        }

        public ExceptionBase(string message)
            : base(message)
        {

        }

        public ExceptionBase(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }

        public ExceptionBase(SerializationInfo info, StreamingContext sc)
            : base(info, sc)
        {

        } 

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates ExceptionBase with message from resx files.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <returns>Returns new instance of ExceptionBase.</returns>
        public static ExceptionBase CreateExceptionFromResource(string resourceKey, string cultureName)
        {
            string message = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(resourceKey, cultureName);
            ExceptionBase baseException = new ExceptionBase(message);
            return baseException;
        }

        /// <summary>
        /// Creates ExceptionBase with message from resx files and inner exception.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="ex">Inner exception.</param>
        /// <returns>Returns new instance of ExceptionBase.</returns>
        public static ExceptionBase CreateExceptionFromResource(string resourceKey, string cultureName, System.Exception ex)
        {
            string message = ApplicationFoundation.Current.ResourceLocalization.GetResourceValue(resourceKey, cultureName);
            ExceptionBase baseException = new ExceptionBase(message, ex);
            return baseException;
        }

        /// <summary>
        /// Creates exception, derived from ExceptionBase, with message from resx files.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <returns>Returns new instance of exception type of T.</returns>
        public static T CreateExceptionFromResource<T>(string resourceKey, string cultureName) where T: ExceptionBase
        {
            T exceptionToReturn = (T)CreateExceptionFromResource(resourceKey, cultureName);
            return exceptionToReturn;
        }

        /// <summary>
        /// Creates exception, derived from ExceptionBase, with message from resx files and inner exception.
        /// </summary>
        /// <param name="resourceKey">Resource key in resx file.</param>
        /// <param name="cultureName">Culture where the resource resides. Provide culture name in format of languagecode2-country/regioncode2.</param>
        /// <param name="ex">Inner exception.</param>
        /// <returns>Returns new instance of exception type of T.</returns>
        public static T CreateExceptionFromResource<T>(string resourceKey, string cultureName, System.Exception ex) where T : ExceptionBase
        {
            T exceptionToReturn = (T)CreateExceptionFromResource(resourceKey, cultureName, ex);
            return exceptionToReturn;
        }

        /// <summary>
        /// Throws exception with specified message.
        /// </summary>
        /// <param name="exMessage">Exception message.</param>
        public static void Throw(string exMessage)
        {
            throw new ExceptionBase(exMessage);
        }

        /// <summary>
        /// Throws exception with specified message.
        /// </summary>
        /// <param name="exMessage">Exception message.</param>
        public static void Throw<T>(string exMessage) where T : ExceptionBase, new()
        {
            T exception = new ExceptionBase(exMessage) as T;
            throw exception;
        }

        #endregion
    }
}
