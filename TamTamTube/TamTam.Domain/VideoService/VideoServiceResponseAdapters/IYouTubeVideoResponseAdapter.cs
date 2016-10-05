using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService.VideoServiceResponseAdapters
{
    /// <summary>
    /// Interface for changing YouTube video search response type to VideoItem type
    /// </summary>
    public interface IYouTubeVideoResponseAdapter
    {
        /// <summary>
        /// Creates new instance of VideoItem type from Youtube.SearchResult.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        VideoItem ToVideoItem(SearchResult result);
    }
}
