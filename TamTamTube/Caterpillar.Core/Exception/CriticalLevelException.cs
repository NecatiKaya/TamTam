using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Exception
{
    [Serializable]
    public class CriticalLevelException : ExceptionBase
    {
        public CriticalLevelException() : base()
        {

        }

        public CriticalLevelException(string message) 
            : base(message)
        {

        }

        public CriticalLevelException(string message, System.Exception innerException) 
            : base(message, innerException)
        {

        }

        public CriticalLevelException(SerializationInfo info, StreamingContext sc)
            : base(info , sc)
        {

        }
    }
}
