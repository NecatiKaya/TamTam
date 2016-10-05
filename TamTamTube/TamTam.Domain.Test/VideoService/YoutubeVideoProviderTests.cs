using Caterpillar.Core.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamTam.Domain.VideoService;

namespace TamTam.Domain.Test.VideoService
{
    [TestClass]
    public class YoutubeVideoProviderTests
    {
        [TestMethod]
        public void Is_Youtube_Active()
        {
            VideoSearchRequest searchParams = new VideoSearchRequest();
            searchParams.SearchQuery = "Undisputed";
            IVideoProvider youtube = new YoutubeVideoProvider();
            VideoSearchResponse result = youtube.SearchVideo(searchParams);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual((RequestStatus)result.Status, RequestStatus.Success);
        }
    }
}
