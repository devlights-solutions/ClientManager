using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientManager.Web.Startup))]
namespace ClientManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
