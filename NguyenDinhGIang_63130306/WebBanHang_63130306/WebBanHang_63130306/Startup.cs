using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanHang_63130306.Startup))]
namespace WebBanHang_63130306
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
