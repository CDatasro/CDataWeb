using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CDataWeb.Startup))]
namespace CDataWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
