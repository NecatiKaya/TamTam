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
    /// <summary>
    /// Summary description for ImdbVideProviderTests
    /// </summary>
    [TestClass]
    public class ImdbVideProviderTests
    {
        [TestMethod]
        public void Is_Imdb_Active()
        {
            VideoSearchRequest searchParams = new VideoSearchRequest();
            searchParams.SearchQuery = "Undisputed";
            IVideoProvider imdb = new IMDBVideoProvider();
            VideoSearchResponse result = imdb.SearchVideo(searchParams);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual((RequestStatus)result.Status, RequestStatus.Success);
        }
    }
}
