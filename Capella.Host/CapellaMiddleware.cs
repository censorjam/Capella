using System;
using System.IO;
using System.Threading.Tasks;
using Capella.Core;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace Capella.Host
{
    public class CapellaMiddleware : OwinMiddleware
    {
        public static RequestHandler Handler = new RequestHandler();

        public CapellaMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            try
            {
                context.Response.Headers.Add("Content-Type", new[] { "application/json; charset=utf-8" });
                context.Response.Headers.Add("Cache-control", new[] { "private" });
                context.Response.StatusCode = 200;

                if (Handler.CanHandle(context.Request.Method, context.Request.Path.ToUriComponent()))
                {
                    string body;
                    using (var writer = new StreamReader(context.Request.Body))
                    {
                        body = writer.ReadToEnd();
                    }

                    var result = Handler.Invoke(context.Request.Method, context.Request.Path.ToUriComponent(), body);

                    using (var writer = new StreamWriter(context.Response.Body))
                    {
                        writer.Write(result);
                    }
                }

                await Next.Invoke(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                using (var writer = new StreamWriter(context.Response.Body))
                {
                    writer.Write(JsonConvert.SerializeObject(e.InnerException, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }));
                }
            }
            await Next.Invoke(context);
        }
    }
}
