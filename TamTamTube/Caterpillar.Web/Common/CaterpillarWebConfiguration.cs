using System;

namespace Caterpillar.Web.Common
{
    /// <summary>
    /// TODO: nkaya, 23.09.2015 22:19, bu class configten okur hale getirilmelidir.
    /// </summary>
    public class CaterpillarWebConfiguration
    {        
        private CaterpillarWebConfiguration()
        {

        }

        private static readonly Lazy<CaterpillarWebConfiguration> _lazy = new Lazy<CaterpillarWebConfiguration>(() =>
        {
            return new CaterpillarWebConfiguration();
        }
        , true);

        public static CaterpillarWebConfiguration Current
        {
            get
            {
                return _lazy.Value;
            }
            private set 
            { 

            }
        }

        /// <summary>
        /// Gets or sets authentication cookie name.
        /// </summary>
        public string AuthenticationCookieName { get; set; }

    }
}
