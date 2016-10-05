using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Caterpillar.Web.Rest
{
    /// <summary>
    /// Helper class for rest operation
    /// </summary>
    public static class RestHelper
    {
        #region | Public Methods |

        /// <summary>
        /// Converts enum media type to string represatation
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string ConvertMediaTypeToString(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.ApplicationJson:
                    return "application/json";
                case MediaType.ApplicationOctetStream:
                    return "application/octet-stream";
            }

            throw new ArgumentException("Unspupported media type");
        }

        /// <summary>
        /// Converts enum media type to typed Header tye
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static MediaTypeWithQualityHeaderValue ConvertMediaTypeToHeaderValue(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.ApplicationJson:
                    return new MediaTypeWithQualityHeaderValue("application/json");
                case MediaType.ApplicationOctetStream:
                    return new MediaTypeWithQualityHeaderValue("application/octet-stream");
            }

            throw new ArgumentException("Unspupported media type");
        }

        public static string ConvertContentTypeToString(ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.ApplicationJson:
                    return "application/json";
                default:
                    //Add other content typesas case items.
                    break;
            }

            throw new ArgumentException("Unspupported media type");
        }

        #endregion  | Public Methods |
    }
}
