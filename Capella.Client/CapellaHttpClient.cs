using System.Net;

namespace Capella.Client
{
    public class CapellaHttpClient
    {
        public string ExecuteRequest(HttpRequest request)
        {
            string str;
            using (var wc = new DecompressingWebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = request.ContentType;
                wc.Headers[HttpRequestHeader.AcceptEncoding] = request.AcceptEncoding;
                wc.Headers[HttpRequestHeader.Cookie] = HttpRequest.ToCookieString(request.Cookies);
                wc.Headers[HttpRequestHeader.Accept] = request.Accept;
                str = wc.UploadString(request.BaseUrl + request.Path, request.Body);
            }
            return str;
        }
    }
}