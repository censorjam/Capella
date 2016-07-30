using System.Collections.Concurrent;
using System.Collections.Generic;
using Capella.Core;

namespace Capella.Client
{
    public class HttpRequestBuilder
    {
        private readonly IRouter _router = new DefaultRouter();
        private readonly ConcurrentDictionary<string, BodyFormatter> _serializers = new ConcurrentDictionary<string, BodyFormatter>();

        public HttpRequest Create(CallContext ctx)
        {
            var route = _router.Get(ctx.Type, ctx.Method);
            var bodySerializer = _serializers.GetOrAdd(route.ToString(), r => new BodyFormatter(ctx.Method.GetParameters()));
            var body = bodySerializer.Serialize(ctx.Arguments);

            var request = new HttpRequest()
            {
                Path = route.Path,
                Method = route.Method,
                Context = ctx,
                Accept = "application/json",
                ContentType = "application/json",
                AcceptEncoding = "gzip",
                Body = body,
                Cookies = new Dictionary<string, string>()
            };

            return request;
        }
    }
}