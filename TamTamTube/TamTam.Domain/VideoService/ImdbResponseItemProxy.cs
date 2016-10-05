using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService
{
    /// <summary>
    /// For Imdb rest request, represents return json object
    /// </summary>
    public class ImdbResponseItemProxy
    {
        public ImdbResponseItemInnerProxy[] title_popular { get; set; }

        public ImdbResponseItemInnerProxy[] title_exact { get; set; }
    }

    /// <summary>
    /// For Imdb  rest request, represents return json object
    /// </summary>
    public class ImdbResponseItemInnerProxy
    {
        public string id { get; set; }

        public string title { get; set; }

        public string name { get; set; }

        public string episode_title { get; set; }

        public string description { get; set; }
    }
}
