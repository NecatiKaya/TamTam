using Caterpillar.Web.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caterpillar.Core.Extensions;
using System.Configuration;
using TamTam.Domain.VideoService.VideoServiceResponseAdapters;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// Specific video provider for IMDB
    /// </summary>
    public class IMDBVideoProvider : IVideoProvider
    {
        /// <summary>
        /// Imdb Api Url
        /// </summary>
        public readonly static string IMDB_API_URL = ConfigurationManager.AppSettings["ImdbApiUrl"];

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

            string query = parameters.SearchQuery.Replace(" ", "+");
            VideoSearchResponse response = new VideoSearchResponse();
            this.RunSafely(() =>
            {
                RestOptions arguments = new RestOptions();
                arguments.Url = string.Format("{0}{1}", IMDB_API_URL, query);
                arguments.Method = RestMethod.Get;
                RestClient rest = new RestClient(arguments);
                ImdbResponseItemProxy restResult = rest.MakeReqeust<ImdbResponseItemProxy>();
                IImdbVideoResponseAdapter imdbDataADapter = new ImdbAdapter();
                VideoItemCollection viodeItems = imdbDataADapter.ToVideoItemCollection(restResult);
                response.Success(viodeItems);
            }, (ex) =>
            {
                response.Error("IMDBVideoProvider_SearchVideo", ex.Message, false);
            });
            return response;
        }

        #endregion | IVideoProvider Implementation |
    }
}
