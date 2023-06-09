using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UberApplication.Startup))]
namespace UberApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
