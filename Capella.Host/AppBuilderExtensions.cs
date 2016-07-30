using Owin;

namespace Capella.Host
{
    public static class AppBuilderExtensions
    {
        public static void UseCapella(this IAppBuilder app)
        {
            app.Use(typeof(CapellaMiddleware));
        }
    }
}