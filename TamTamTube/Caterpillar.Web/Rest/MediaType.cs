using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caterpillar.Web.Rest
{
    /// <summary>
    /// Contains supported media types for REST
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// Media type for application/json 
        /// </summary>
        ApplicationJson = 1,
        /// <summary>
        /// Media type for application/octet-stream
        /// </summary>
        ApplicationOctetStream = 2
    }
}
