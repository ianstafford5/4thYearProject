using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeaderBoardMVC.Startup))]
namespace LeaderBoardMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
