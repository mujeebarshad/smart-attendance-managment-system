using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Attendance.Startup))]
namespace Attendance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
