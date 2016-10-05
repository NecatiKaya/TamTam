using Caterpillar.Core.Application;
using Caterpillar.Core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamTam.Domain.VideoService;

namespace TamTam.Services.VideoService
{
    public class VideSearchHelper
    {
        /// <summary>
        /// Searches cache by request term. Returns true if data is in cache, otherwise returns false.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Returns true if data is in cache, otherwi returns false.</returns>
        public bool GetFromCache(string query)
        {
            string queryInCache = ApplicationFoundation.Current.CacheService.Get<string>(query);
            bool isInCache = !string.IsNullOrWhiteSpace(queryInCache);
            return isInCache;
        }

        public VideoSearchResponse Search(VideoSearchRequest searhRequest)
        {
            List<string> itemsNotInCache = new List<string>();
            VideoSearchResponse response = null;

            ///Warning : NKaya, 2016-10-06 00:00, Lucene index search block must be activeted only if the business rules are certain.

            #region | Indexed Data is Searched via Lucene |

            ////if data is in cache return it. Because all request data is added to cache and thane pull back again. So if you an find data in cache, no need to make web request again which costs highly.
            //VideoItemCollection cachedData = SearchLuceneAndGetData(searhRequest.SearchQuery, out itemsNotInCache);
            //bool isDataInCache = cachedData != null && cachedData.Count > 0;
            //if (isDataInCache)
            //{
            //    response = new VideoSearchResponse();
            //    response.Success(cachedData);
            //    return response;
            //}

            #endregion | Indexed Data is Searched via Lucene |

            // Looking for data in the cache
            VideoItemCollection cachedData = cachedData = ApplicationFoundation.Current.CacheService.Get<VideoItemCollection>(searhRequest.SearchQuery);
            if (cachedData != null)
            {
                if (cachedData.Count > 0)
                {
                    response = new VideoSearchResponse();
                    response.Success(cachedData);
                    return response;
                }
            }

            //Couldn't find any data in cache, so search providers
            response = SearchProviders(searhRequest);
            return response;
        }

        /// <summary>
        /// Adds search result to cahce
        /// </summary>
        /// <param name="query"></param>
        /// <param name="data"></param>
        private void AddToCache(string query, VideoItemCollection data)
        {
            ApplicationFoundation.Current.CacheService.Set(query, data, int.MaxValue);
        }

        /// <summary>
        /// Searches queries that user entered to search video in Lucene index
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private VideoItemCollection SearchLuceneAndGetData(string query, out List<string> itemsNoCachedYet)
        {
            itemsNoCachedYet = new List<string>();
            VideoItemCollection data = null;
            VideoItemCollection temp = null;
            LuceneHelper lucene = new LuceneHelper();
            string[] cacheKeys = lucene.Search(query);

            if (cacheKeys != null && cacheKeys.Length > 0)
            {
                cacheKeys = cacheKeys.Distinct().ToArray();
                data = new VideoItemCollection();
                for (int i = 0; i < cacheKeys.Length; i++)
                {
                    temp = ApplicationFoundation.Current.CacheService.Get<VideoItemCollection>(cacheKeys[i]);
                    if (temp != null)
                    {
                        data.AddRange(temp);
                    }
                    else
                    {
                        itemsNoCachedYet.Add(cacheKeys[i]);
                    }
                }
                return data;               
            }

            return null;
        }

        /// <summary>
        /// Searches video providers, if any data is found, than it is cached and indexed on Lucene
        /// </summary>
        /// <param name="searhRequest"></param>
        /// <returns></returns>
        private VideoSearchResponse SearchProviders(VideoSearchRequest searhRequest)
        {
            // one provider request can be fail but the other can be success. So the webapi result should be success. No need to control if one is success or not below. If both provider req fails api should fail.
            VideoSearchResponse response = new VideoSearchResponse()
            {
                Status = RequestStatus.Fail
            };
            VideoItemCollection tempVideoList = null;
            VideoSearchResponse tempResponse = null;
            LuceneHelper lucene = new LuceneHelper();

            //Each provider attached to Demo app, make search request.
            foreach (IVideoProvider eachProvider in VideoServiceFoundation.Current.VideoProviders)
            {
                tempResponse = eachProvider.SearchVideo(searhRequest);
                if (tempResponse != null)
                {
                    //request is succesfully done, so get data from request.
                    if (tempResponse.Status == RequestStatus.Success)
                    {
                        response.Success(); // If one request is success and the other is fail, result should be success
                        if (tempResponse.Data != null)
                        {
                            tempVideoList = tempResponse.Data;
                            response.Data.AddRange(tempVideoList);
                        }
                    }
                }
            }

            // Adding aggreageted saerch result to index. If you add one provider search result i for loop to index, you override cache for one provider which means you only get one provider result from cache instead of two
            if (response.Data != null)
            {
                if (response.Status == RequestStatus.Success)
                {
                    //Add data to cache and search index
                    ApplicationFoundation.Current.CacheService.Set(searhRequest.SearchQuery, response.Data, int.MaxValue);
                    lucene.AddToIndex(searhRequest.SearchQuery, response.Data);
                }
            }
            tempVideoList = null;
            tempResponse = null;

            return response;
        }
    }
}