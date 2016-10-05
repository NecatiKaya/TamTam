using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Service
{
    [Serializable]
    /// <summary>
    /// Contains information about token whether user is valided or not.
    /// </summary>
    [DataContract]
    public class SingleSignOnToken
    {
        #region Constructors

        /// <summary>
        /// Initialize new instance of SingleSignOnToken.
        /// </summary>
        public SingleSignOnToken()
        {
            IsValid = false;
            Token = null;
        }

        #endregion

        #region Properties

        [DataMember]
        /// <summary>
        /// Gets or sets valided user token. If this value is null or empty, the user is not validated .
        /// </summary>
        public string Token { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets whether user validated or not.
        /// </summary>
        public bool IsValid { get; set; }

        #endregion
    }
}
