using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FILEIDSMVC.Startup))]
namespace FILEIDSMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
