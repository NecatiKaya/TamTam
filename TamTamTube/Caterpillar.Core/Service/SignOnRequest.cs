using System;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Service
{
    /// <summary>
    /// Basic request object for signing on both for client and partner/company.
    /// </summary>
    [DataContract]
    [Serializable]
    public class SignOnRequest
    {
        #region Properties

        [DataMember(IsRequired = true)]
        /// <summary>
        /// Gets or sets user name for signing on.
        /// </summary>
        public string UserName { get; set; }

        [DataMember(IsRequired = true)]
        /// <summary>
        /// Gets or sets password for signing on.
        /// </summary>
        public string Password { get; set; }

        [DataMember(IsRequired = true)]
        /// <summary>
        /// Gets or sets application Id which client is trying to sign on.
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets partner Id to validate partner. If the client is an end user, ths parameter must be null. If the user is a partner/company this parameter mustn't be null.
        /// </summary>
        public string PartnerId { get; set; }

        #endregion
    }
}
