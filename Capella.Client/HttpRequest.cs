using System.Collections.Generic;
using System.Text;

namespace Capella.Client
{
    public class HttpRequest
    {
        public CallContext Context { get; set; }
        public string BaseUrl { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public IDictionary<string, string> Cookies { get; set; }
        public string Method { get; set; }
        public string AcceptEncoding { get; set; }
        public string Body { get; set; }
        public string Accept { get; set; }

        public static string ToCookieString(IDictionary<string, string> cookies)
        {
            if (cookies == null)
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var kv in cookies)
            {
                sb.Append(kv.Key);
                sb.Append("=");
                sb.Append(kv.Value);
                sb.Append(";");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Method, BaseUrl, Path, ToCookieString(Cookies));
        }
    }
}