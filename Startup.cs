using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Edstart.Startup))]
namespace Edstart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
