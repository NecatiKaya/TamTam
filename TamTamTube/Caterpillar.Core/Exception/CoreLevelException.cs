using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Exception
{
    [Serializable]
    public class CoreLevelException : ExceptionBase
    {
        #region Constructors

        public CoreLevelException()
            : base()
        {

        }

        public CoreLevelException(string message)
            : base(message)
        {

        }

        public CoreLevelException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }

        public CoreLevelException(SerializationInfo info, StreamingContext sc)
            : base(info, sc)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Throws default core level exception. 
        /// </summary>
        /// <param name="message">Message which contains information about exception. Please provide as much as information about exception</param>
        public new static void Throw(string message)
        {
            throw new CoreLevelException(string.Format(LanguageStrings.CoreLevelExceptonMessageFormat, message ?? "#null#"));
        }

        #endregion
    }
}
