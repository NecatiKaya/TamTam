using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TamTam.Domain.VideoService;
using TamTam.Domain.VideoService.Validation;
using TamTam.Domain.Extensions;
using Caterpillar.Core.Extensions;
using Caterpillar.Core.Client;
using Caterpillar.Core.Application;
using System.Web.Http.Cors;

namespace TamTam.Services.VideoService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VideoController : ApiController
    {   
        [Route("api/Video/search")]
        [HttpPost]
        public VideoSearchResponse SearchVideo([FromUri] VideoSearchRequest searhRequest)
        {
            // one provider request can be fail but the other can be success. So the webapi result should be success. No need to control if one is success or not below. If both provider req fails api should fail.
            VideoSearchResponse response = new VideoSearchResponse()
            {
                Status = RequestStatus.Fail
            }; 
            response.Data = new VideoItemCollection();
            this.RunSafely(() =>
            {
                //Validation vide search request
                VideSearchRequestValidator requestValidator = new VideSearchRequestValidator();
                ValidationResult result = requestValidator.Validate(searhRequest);
                if (!result.IsValid)
                {
                    result.CopyToDictionary(response.UserFriendlyErrorMesages);
                    // request is not validated. So no need to continue
                    return;
                }

                #region | Moved To VideoHelper.Search() |

                //VideoItemCollection tempVideoList = null;
                //VideSearchResponse tempResponse = null;
                //LuceneHelper lucene = new LuceneHelper();

                ////Each provider attached to Demo app, make search request.
                //foreach (IVideoProvider eachProvider in VideoServiceFoundation.Current.VideoProviders)
                //{
                //    tempResponse = eachProvider.SearchVideo(searhRequest);
                //    if (tempResponse != null)
                //    {
                //        //request is succesfully done, so get data from request.
                //        if (tempResponse.Status == RequestStatus.Success)
                //        {
                //            response.Success(); // If one request is success and the other is fail, result should be success
                //            if (tempResponse.Data != null)
                //            {
                //                tempVideoList = tempResponse.Data;
                //                response.Data.AddRange(tempVideoList);

                //                //Add data to cache and search index
                //                ApplicationFoundation.Current.CacheService.Set(searhRequest.SearchQuery, tempVideoList, int.MaxValue);
                //                lucene.AddToIndex(searhRequest.SearchQuery);
                //            }
                //        }
                //    }
                //}
                //tempVideoList = null;
                //tempResponse = null;       

                #endregion | Moved To VideoHelper.Search() |

                VideSearchHelper searchHelper = new VideSearchHelper();
                response = searchHelper.Search(searhRequest);
            }, (ex) =>
            {
                response.Error("VideoSearchApi_Error", ex.Message);
            });
            return response;
        }

        [Route("api/Video/Suggestions")]
        [HttpPost]
        public object Suggestions([FromUri] string query, [FromUri] string token)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new { Title = string.Empty, Value = string.Empty };
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return new { Title = string.Empty, Value = string.Empty };
            }

            var keys = ApplicationFoundation.Current.CacheService.GetKeysByPattern(query).Select(key => new { Title = key, Value = key }).Distinct().Take(50);
            return keys;
        }
    }
}
