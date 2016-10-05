using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;

namespace TamTam.Domain.VideoService.VideoServiceResponseAdapters
{
    /// <summary>
    /// Concrete implementatio for IYouTubeAdapter 
    /// </summary>
    public class YouTubeAdapter : IYouTubeVideoResponseAdapter
    {
        public const string YOUTUBE_VIDEO_LINK = "https://www.youtube.com/watch?v={0}";

        #region | IYouTubeAdapter Implementation |

        /// <summary>
        ///  Creates new instance of VideoItem type from Youtube.SearchResult.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public VideoItem ToVideoItem(SearchResult result)
        {
            if (result == null)
            {
                return null;
            }

            VideoItem item = new VideoItem()
            {
                Description = result.Snippet != null ? result.Snippet.Description : null,
                Title = result.Snippet.Title,
                VideoId = result.Id.VideoId,
                VideoSourceProvider = SourceProvider.Youtube,
                VideoUrl = CreateYoutubeVideoLink(result.Id != null ? result.Id.VideoId : null)
            };
            return item;
        }

        #endregion | IYouTubeAdapter Implementation |

        #region | Private Methods |

        /// <summary>
        /// Creates fully qualified url from videoId
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        private string CreateYoutubeVideoLink(string videoId)
        {
            string url = string.Format(YOUTUBE_VIDEO_LINK, videoId);
            return url;
        }

        #endregion | Private Methods |
    }
}
