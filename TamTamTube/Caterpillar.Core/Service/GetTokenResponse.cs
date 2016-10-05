using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Service
{
    /// <summary>
    /// Baic response for GetToken operation.
    /// </summary>
    [Serializable]
    [DataContract]
    public class GetTokenResponse : ResponseBase<SingleSignOnToken>
    {

    }
}
