using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Exception
{
    [Serializable]
    public class UserLevelException : ExceptionBase
    {
        public UserLevelException() : base()
        {

        }

        public UserLevelException(string message) 
            : base(message)
        {

        }

        public UserLevelException(string message, System.Exception innerException) 
            : base(message, innerException)
        {

        }

        public UserLevelException(SerializationInfo info, StreamingContext sc)
            : base(info , sc)
        {

        }
    }
}
