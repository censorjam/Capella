using Capella.Host;
using Owin;

namespace TestApp.Host
{
    public class Startup
    {
        public Startup()
        {
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseCapella();
        }
    }
}