using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Recycling.Startup))]
namespace Recycling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
