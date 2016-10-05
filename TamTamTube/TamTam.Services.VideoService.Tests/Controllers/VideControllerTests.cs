using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TamTam.Services.VideoService.Controllers;
using TamTam.Domain.VideoService;
using Caterpillar.Core.Client;
using Caterpillar.Core.Application;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Caterpillar.Core.Logging;
using Caterpillar.Web.DelegationHandler;
using System.Web.Http;

namespace TamTam.Services.VideoService.Tests
{
    /// <summary>
    /// Summary description for VideControllerTests
    /// </summary>
    [TestClass]
    public class VideControllerTests
    {
        public static void InitializeDependencyConfig()
        {
            IUnityContainer container = BuildUnityContainer();
            UnityDependencyResolver unityDependencyResolver = new UnityDependencyResolver(container);
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

        [TestInitialize]
        public void SetDependencies()
        {
            InitializeDependencyConfig();

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
        }

        [TestMethod]
        public void Search_Video_Service_Must_Have_Result()
        {
            VideoController controller = new VideoController();
            VideoSearchRequest request = new VideoSearchRequest();
            request.MaxResult = 50;
            request.SearchQuery = "google";
            request.TransactionId = Guid.NewGuid();
            request.VerificationToken = Guid.NewGuid().ToString();// MVC antiforgery token can be provided.
            VideoSearchResponse response = controller.SearchVideo(request);

            // object must be null
            Assert.IsNotNull(response);

            // must have data
            Assert.IsNotNull(response.Data);

            // must be successfull
            Assert.AreEqual<RequestStatus>(response.Status, RequestStatus.Success);

            //if there is no item fail test
            Assert.AreEqual<int>(response.Data.Count, 0);

        }
    }
}
