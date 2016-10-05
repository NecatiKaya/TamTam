using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Caterpillar.Core.Exception;
using Newtonsoft.Json;

namespace Caterpillar.Web.Rest
{
    /// <summary>
    /// Basic object for making rest calls
    /// </summary>
    public class RestClient
    {
        #region | Constructor |

        /// <summary>
        /// Initialize new instance of RestClient
        /// </summary>
        public RestClient()
        {

        }

        /// <summary>
        /// Initialize new instance of RestClient with RestOptions
        /// </summary>
        public RestClient(RestOptions arguments)
        {
            Arguments = arguments;
        }

        #endregion | Constructor |

        #region | Properties |

        /// <summary>
        /// Gets or sets options for res operations
        /// </summary>
        public RestOptions Arguments { get; set; }

        #endregion | Properties |

        #region | Public Methods |

        /// <summary>
        /// Makes a Rest request
        /// </summary>
        /// <returns></returns>
        public T MakeReqeust<T>()
        {
            if (Arguments == null)
            {
                throw new ArgumentNullException("Arguments");
            }

            if (string.IsNullOrWhiteSpace(Arguments.Url))
            {
                throw new ArgumentNullOrEmptyException("Arguments.Url");
            }

            T result = default(T);
            MediaTypeWithQualityHeaderValue meadiTypeHeader = RestHelper.ConvertMediaTypeToHeaderValue(Arguments.MediaType);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Arguments.Url);
                client.DefaultRequestHeaders.Accept.Add(meadiTypeHeader);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", RestHelper.ConvertContentTypeToString(Arguments.ContentType));
                HttpResponseMessage response = null;
                switch (Arguments.Method)
                {
                    case RestMethod.Get:
                        response = client.GetAsync(Arguments.Url).Result;
                        break;
                    case RestMethod.Post:
                    case RestMethod.Delete:
                    case RestMethod.Head:
                    case RestMethod.Options:
                    case RestMethod.Put:
                    case RestMethod.Trace:
                    // For future usage.
                    default:
                        break;
                }

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<T>(resultString);
                    //result = response.Content.ReadAsAsync<T>(new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() }).Result;
                }
            }
            return result;
        }

        /// <summary>
        /// Makes a Rest request
        /// </summary>
        /// <returns></returns>
        public T MakeReqeust<T>(RestOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            if (string.IsNullOrWhiteSpace(options.Url))
            {
                throw new ArgumentNullOrEmptyException("options.Url");
            }
            T result = default(T);
            MediaTypeWithQualityHeaderValue meadiTypeHeader = RestHelper.ConvertMediaTypeToHeaderValue(MediaType.ApplicationJson);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(options.Url);
                client.DefaultRequestHeaders.Accept.Add(meadiTypeHeader);
                HttpResponseMessage response = null;
                switch (options.Method)
                {
                    case RestMethod.Get:
                        response = client.GetAsync(options.Url).Result;
                        break;
                    case RestMethod.Post:
                    case RestMethod.Delete:
                    case RestMethod.Head:
                    case RestMethod.Options:
                    case RestMethod.Put:
                    case RestMethod.Trace:
                    // For future usage.
                    default:
                        break;
                }

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsAsync<T>().Result;
                }
            }
            return result;
        }

        #endregion | Public Methods |
    }
}
