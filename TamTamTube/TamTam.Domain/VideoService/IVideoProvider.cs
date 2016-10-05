using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// Interface for movie database and video service providers
    /// </summary>
    public interface IVideoProvider
    {
        /// <summary>
        /// Search video for implemented provider
        /// </summary>
        /// <param name="parameters">Search parameters</param>
        /// <returns>Returns an instance of VideSearchResponse which contains video search result or error information </returns>
        VideoSearchResponse SearchVideo(VideoSearchRequest parameters);
    }
}
