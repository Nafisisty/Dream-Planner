using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DreamPlanner_Main.Startup))]
namespace DreamPlanner_Main
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
