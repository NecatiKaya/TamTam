using Caterpillar.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// Holds results of a movie database or a video service search
    /// </summary>
    public class VideoSearchResponse : ResponseBase<VideoItemCollection>
    {
        #region | Constructors |

        public VideoSearchResponse() : base(new VideoItemCollection())
        {

        }

        #endregion | Constructors |
    }
}
