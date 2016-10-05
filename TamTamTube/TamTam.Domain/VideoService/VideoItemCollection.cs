using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// A specialized generic collection for VideoItem type.
    /// </summary>
    public class VideoItemCollection : List<VideoItem>
    {
        #region | Constructors |

        /// <summary>
        /// Creates new instance of VideoItemCollection type.
        /// </summary>
        public VideoItemCollection()
        {

        }

        /// <summary>
        /// Creates new instance of VideoItemCollection type with already provided items. Items are added the collection immediately.
        /// </summary>
        public VideoItemCollection(IEnumerable<VideoItem> items)
        {
            this.AddRange(items);
        }

        #endregion | Constructors |
    }
}
