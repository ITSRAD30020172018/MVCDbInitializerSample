using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCDbInitializerSample.Startup))]
namespace MVCDbInitializerSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
