using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_497FinalProject.Startup))]
namespace _497FinalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
