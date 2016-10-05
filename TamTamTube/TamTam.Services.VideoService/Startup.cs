using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using TamTam.Domain.VideoService;
using System.Web.Http;
using Caterpillar.Web.DelegationHandler;
using Caterpillar.Core.Logging;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Caterpillar.Core.Application;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

[assembly: OwinStartup(typeof(TamTam.Services.VideoService.Startup))]

namespace TamTam.Services.VideoService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            #region | Adding Video Providers |

            ///Can develop fluent api to set Videoproviders
            VideoServiceFoundation.Current.VideoProviders = new List<IVideoProvider>();
            VideoServiceFoundation.Current.VideoProviders.Add(new YoutubeVideoProvider());
            VideoServiceFoundation.Current.VideoProviders.Add(new IMDBVideoProvider());

            #endregion | Adding Video Providers |

            #region | Message Logging Config |

            ///Register web api message logging
            ILogger logger = new FileLogger();
            GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler(logger));

            #endregion | Message Logging Config |

            //#region | Initializing Lucene Index |

            //LuceneHelper helper = new LuceneHelper();
            //helper.AddToIndex(null);

            //#endregion | Initializing Lucene Index |

            InitializeDependencyConfig();
            ConfigureAuth(app);
        }

        public static void InitializeDependencyConfig()
        {
            IUnityContainer container = BuildUnityContainer();
            UnityDependencyResolver unityDependencyResolver = new UnityDependencyResolver(container);
            DependencyResolver.SetResolver(unityDependencyResolver);

            // Setting application wide services. This enables us to use the services in framework level.
            ApplicationFoundation.SetDependencyResolver(unityDependencyResolver);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            UnityConfigurationSection section = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            if (section != null)
            {
                section.Configure(container);
            }

            return container;
        }
    }
}
