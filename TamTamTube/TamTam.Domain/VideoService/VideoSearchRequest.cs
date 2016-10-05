using Caterpillar.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// Contains basic request parameters for video querying. 
    /// </summary>
    public class VideoSearchRequest : RequestBase
    {
        #region | Constructors |

        /// <summary>
        /// Initialize new VideoSearchRequest object with TransactionId property already set to new Guid
        /// </summary>
        public VideoSearchRequest() : base()
        {
            MaxResult = 50;
        }

        #endregion | Constructors |

        #region | Properties |
        
        /// <summary>
        /// Gets or sets name of the video to searach for.
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Gets or sets max item count in response.
        /// </summary>
        public int MaxResult { get; set; }

        #endregion | Properties |
    }
}
