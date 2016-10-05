using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TamTamTube.UI.Startup))]
namespace TamTamTube.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
