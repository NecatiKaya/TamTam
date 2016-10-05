using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService.VideoServiceResponseAdapters
{
    /// <summary>
    /// Interface for changing Imdb video search response type to VideoItem type
    /// </summary>
    public interface IImdbVideoResponseAdapter
    {
        /// <summary>
        /// Creates new instance of VideoItemCollection type from Imdb.ImdbResponseItemProxy.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        VideoItemCollection ToVideoItemCollection(ImdbResponseItemProxy result);
    }
}
