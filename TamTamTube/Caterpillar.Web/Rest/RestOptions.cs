using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Caterpillar.Web.Rest
{
    /// <summary>
    /// Contains request options for REST operations
    /// </summary>
    public class RestOptions
    {
        #region | Constructors |

        /// <summary>
        /// Initialize new instance of RestOptions type with default values are set.
        /// </summary>
        public RestOptions()
        {
            MediaType = MediaType.ApplicationJson;
            ContentType = ContentType.ApplicationJson;
            Method = RestMethod.Get;
        }

        #endregion | Constructors |

        #region | Properties |

        /// <summary>
        /// Gets or sets media type header for REST operatios
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// Gets or sets url to make a REST request.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets type of content for REST request.
        /// </summary>
        public ContentType ContentType { get; set; }

        /// <summary>
        /// Gets or sets http verb to use. Suchs as POST, GET etc...
        /// </summary>
        public RestMethod Method { get; set; }

        #endregion | Properties |
    }
}
