using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BETOven.Startup))]
namespace BETOven
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //tests
            ConfigureAuth(app);
        }
    }
}
