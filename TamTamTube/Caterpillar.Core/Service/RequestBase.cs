using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Caterpillar.Core.Service
{
    /// <summary>
    /// A generic request base for making service (web service, wcf, web api etc.) calls
    /// </summary>
    [Serializable]
    [DataContract]
    public class RequestBase
    {
        #region | Constructors |

        public RequestBase()
        {
            this.TransactionId = Guid.NewGuid();
            this.RequestStartDate = DateTime.Now;
        }

        #endregion | Constructors |

        #region | Properties |

        /// <summary>
        /// Reserved for future usaged. By this parameter request and response objects can be tracked easily.
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Gets or sets request start date and time.
        /// </summary>
        public DateTime RequestStartDate { get; set; }

        /// <summary>
        /// Gets or sets verificationToken for the request. It can be work same as TransactionId property.
        /// </summary>
        public string VerificationToken { get; set; }

        #endregion | Properties |
    }
}
