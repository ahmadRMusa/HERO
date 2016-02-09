using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HERO.Startup))]
namespace HERO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
