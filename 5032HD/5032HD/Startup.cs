using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_5032HD.Startup))]
namespace _5032HD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
