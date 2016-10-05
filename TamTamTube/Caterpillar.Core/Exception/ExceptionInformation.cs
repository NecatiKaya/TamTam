using Caterpillar.Core.Collections;
using System;

namespace Caterpillar.Core.Exception
{
    /// <summary>
    /// Contains extra information about an occured information
    /// </summary>
    [Serializable]
    public class ExceptionInformation
    {
        #region Properties

        /// <summary>
        /// Gets or sets system level exception.
        /// </summary>
        public System.Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets occuring date of exception.
        /// </summary>
        public DateTime OccuredDate { get; set; }

        /// <summary>
        /// Gets or sets id of user who got the exception.
        /// </summary>
        public object UserId { get; set; }

        /// <summary>
        /// Gets or sets culture settings information of user who got the exception
        /// </summary>
        public string UserCultureSettings { get; set; }

        /// <summary>
        /// Gets or sets extra information about exception.
        /// </summary>
        public StringToStringDictionary ExtraInformation { get; set; }

        /// <summary>
        /// Gets or sets host name of client who got the exception.
        /// </summary>
        public string ClientHostName { get; set; }

        /// <summary>
        /// Gets or sets ip address of client who got the exception.
        /// </summary>
        public string ClientIp { get; set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of ExcetptionInformation type.
        /// </summary>
        /// <param name="ex">Exception</param>
        public ExceptionInformation(System.Exception ex)
        {
            this.Exception = ex;
            this.OccuredDate = DateTime.Now;
            this.ExtraInformation = new StringToStringDictionary();
        }

        #endregion
    }
}
