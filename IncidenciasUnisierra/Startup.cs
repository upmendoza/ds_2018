using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IncidenciasUnisierra.Startup))]
namespace IncidenciasUnisierra
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
