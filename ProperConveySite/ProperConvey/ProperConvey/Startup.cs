using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProperConvey.Startup))]
namespace ProperConvey
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
