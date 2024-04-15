using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideogameStudio2.Startup))]
namespace VideogameStudio2
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
