using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaseProject.Web.Startup))]
namespace BaseProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
