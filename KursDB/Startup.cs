using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KursDB.Startup))]
namespace KursDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
