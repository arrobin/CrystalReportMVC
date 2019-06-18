using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrystalReportMVC.Startup))]
namespace CrystalReportMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
