using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// Converts response of video providers and VideoItemCollection type to each other.
    /// </summary>
    public static class VideoServiceProviderResponseMapper
    {
        /// <summary>
        /// Creates new instance with type of VideoItemCollection from SearchListResponse type.
        /// </summary>
        /// <param name="youtubeResponse"></param>
        /// <returns></returns>
        public static VideoItemCollection FromSearchListResponseToVideoItemCollection(SearchListResponse youtubeResponse)
        {
            //if (youtubeResponse == null)
            //{
            //    return null;
            //}

            //VideoItem temp = null;
            //VideoItemCollection items = new VideoItemCollection();
            //foreach (SearchResult eachItemFromYoutube in youtubeResponse.Items)
            //{
            //    temp = new VideoItem()
            //    {
            //        Description = eachItemFromYoutube.Snippet != null ? eachItemFromYoutube.Snippet.Description : null,
            //        Title = eachItemFromYoutube.Snippet.Title,
            //        VideoId = eachItemFromYoutube.Id != null ? eachItemFromYoutube.Id.VideoId : null,
            //         VideoSourceProvider =  SourceProvider.Youtube,
            //          VideoUrl = 
            //    };
            //}
            //return items;

            return null;
        }
    }
}
