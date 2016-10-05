using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Service
{
    [Serializable]
    [DataContract]
    /// <summary>
    /// Baic response for sign on operations
    /// </summary>
    
    public class SignOnResponse : ResponseBase<SingleSignOnToken>
    {

    }
}
