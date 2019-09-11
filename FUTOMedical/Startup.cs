using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FUTOMedical.Startup))]
namespace FUTOMedical
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
