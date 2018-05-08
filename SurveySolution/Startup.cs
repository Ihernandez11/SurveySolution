using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SurveySolution.Startup))]
namespace SurveySolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
