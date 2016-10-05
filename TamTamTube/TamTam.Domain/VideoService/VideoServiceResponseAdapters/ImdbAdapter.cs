using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService.VideoServiceResponseAdapters
{
    public class ImdbAdapter : IImdbVideoResponseAdapter
    {
        public const string IMDB_VIDEO_LINK = "http://www.imdb.com/title/{0}/";

        #region | IImdbVideoResponseAdapter Implementation |

        /// <summary>
        ///  Creates new instance of VideoItem type from Imdb.ImdbResponseItemProxy.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public VideoItemCollection ToVideoItemCollection(ImdbResponseItemProxy result)
        {
            if (result == null)
            {
                return null;
            }

            VideoItemCollection items = null;
            VideoItem item = null;
            if (result.title_popular != null)
            {
                items = new VideoItemCollection();
                foreach (ImdbResponseItemInnerProxy eachTitlePopular in result.title_popular)
                {
                    item = ToVideoItem(eachTitlePopular);
                    items.Add(item);
                }
            }
            if (result.title_exact != null)
            {
                if (items == null)
                {
                    items = new VideoItemCollection();
                }
                foreach (ImdbResponseItemInnerProxy eachTitlePopular in result.title_exact)
                {
                    item = ToVideoItem(eachTitlePopular);
                    items.Add(item);
                }
            }
            return items;
        }

        #endregion | IImdbVideoResponseAdapter Implementation |

        #region | Private Methods |

        private VideoItem ToVideoItem(ImdbResponseItemInnerProxy innerProxy)
        {
            if (innerProxy == null)
            {
                return null;
            }
            VideoItem video = new VideoItem()
            {
                Description = innerProxy.description,
                Title = innerProxy.title,
                VideoId = innerProxy.id,
                VideoSourceProvider = SourceProvider.Imdb,
                VideoUrl = CreateImdbVideoLink(innerProxy.id)
            };
            return video;
        }

        /// <summary>
        /// Creates fully qualified url from videoId
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        private string CreateImdbVideoLink(string videoId)
        {
            string url = string.Format(IMDB_VIDEO_LINK, videoId);
            return url;
        }

        #endregion | Private Methods |
    }
}
