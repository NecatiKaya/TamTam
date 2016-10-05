using Caterpillar.Core.Extensions;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamTam.Domain.VideoService.VideoServiceResponseAdapters;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// Specific video provider for Youtube.
    /// </summary>
    public class YoutubeVideoProvider : IVideoProvider
    {
        /// <summary>
        /// YouTube api url
        /// </summary>
        public readonly static string YOUTUBE_API_URL = ConfigurationManager.AppSettings["YouTubeApiKey"];

        /// <summary>
        /// What is search in YouTube. Such as video, tv epiosede etc.
        /// </summary>
        private readonly static string YOUTUBE_SEARCH_TYPE = "video";

        /// <summary>
        /// Default value for YouTube api.
        /// </summary>
        private readonly static string YOUTUBE_SEARCH_SNIPPET = "snippet";

        #region | IVideoProvider Implementation |

        /// <summary>
        /// Search video for Imdb
        /// </summary>
        /// <param name="parameters">Search parameters</param>
        /// <returns>Returns an instance of VideSearchResponse which contains video search result or error information </returns>
        public VideoSearchResponse SearchVideo(VideoSearchRequest parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                return null;
            }

            VideoSearchResponse response = new VideoSearchResponse();
            this.RunSafely(() =>
            {
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = YOUTUBE_API_URL
                });

                SearchResource.ListRequest searchListRequest = youtubeService.Search.List(YOUTUBE_SEARCH_SNIPPET);
                searchListRequest.Q = parameters.SearchQuery;
                searchListRequest.MaxResults = parameters.MaxResult;
                searchListRequest.VideoType = SearchResource.ListRequest.VideoTypeEnum.Movie;
                searchListRequest.Type = YOUTUBE_SEARCH_TYPE;
                SearchListResponse searchListResponse = searchListRequest.Execute();

                VideoItem temp = null;
                VideoItemCollection items = new VideoItemCollection();
                YouTubeAdapter youTubeDataADapter = new YouTubeAdapter();                
                if (searchListResponse != null)
                {
                    foreach (SearchResult eachItemFromYouTubeService in searchListResponse.Items)
                    {
                        temp = youTubeDataADapter.ToVideoItem(eachItemFromYouTubeService);
                        items.Add(temp);
                    }
                }
                
                response.Success(items);
            }, (ex) =>
            {
                response.Error("YoutubeVideoProvider_SearchVideo", ex.Message, false);
            });
            return response;
        }

        #endregion | IVideoProvider Implementation |
    }
}
