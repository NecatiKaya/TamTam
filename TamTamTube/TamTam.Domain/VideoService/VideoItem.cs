using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// Type for holding basic information about a video
    /// </summary>
    public class VideoItem
    {
        #region | Constructors |

        public VideoItem()
        {

        }

        #endregion | Constructors |

        #region | Properties |

        /// <summary>
        /// Gets or sets title of the video
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of vide
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets publish date of the video
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets or sets video provider.
        /// </summary>
        public SourceProvider VideoSourceProvider { get; set; }

        /// <summary>
        /// Gets or sets Id of video.
        /// </summary>
        public string VideoId { get; set; }

        /// <summary>
        /// Gets or sets url of video
        /// </summary>
        public string VideoUrl { get; set; }

        #endregion | Properties |
    }
}
