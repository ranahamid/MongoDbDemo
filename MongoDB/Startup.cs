using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MongoDB.Startup))]
namespace MongoDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
