using Microsoft.Owin;
using Owin;
using _5051.Backend;

[assembly: OwinStartupAttribute(typeof(_5051.Startup))]
namespace _5051
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //AvatarItemBackend.Instance.Index();
            //FactoryInventoryBackend.Instance.Index();

            //GameBackend.Instance.Index();

            //KioskSettingsBackend.Instance.Reset();

            //SchoolCalendarBackend.Instance.Reset();
            //SchoolDismissalSettingsBackend.Instance.Reset();

            //StudentBackend.Instance.Reset();
        }
    }
}
