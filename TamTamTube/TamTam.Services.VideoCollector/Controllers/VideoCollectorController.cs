using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TamTam.Domain.VideoService;

namespace TamTam.Services.VideoCollector.Controllers
{
    [Route("api/Video")]
    public class VideoCollectorController : ApiController
    {
        [Route("search")]
        public void SearchVideo([FromUri] VideoSearchRequest searhRequest)
        {

        }
    }
}
