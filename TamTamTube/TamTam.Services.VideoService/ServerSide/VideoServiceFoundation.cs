using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamTam.Domain.VideoService;

namespace TamTam.Services.VideoService
{
    public class VideoServiceFoundation
    {
        #region Private Variables

        /// <summary>
        /// private singletone instance helper.
        /// </summary>
        private static readonly Lazy<VideoServiceFoundation> _appFoundation = new Lazy<VideoServiceFoundation>(() => new VideoServiceFoundation(), true);

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets video providers
        /// </summary>
        public List<IVideoProvider> VideoProviders { get; set; }

        private static VideoServiceFoundation _Current;
        /// <summary>
        /// Singletone current instance
        /// </summary>
        public static VideoServiceFoundation Current
        {
            get
            {
                return _appFoundation.Value;
            }
        }

        #endregion Properties
    }
}