using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Capella.Client
{
    public class ServiceClient
    {
        private readonly ServiceClientPipeline _pipeline = new ServiceClientPipeline();
        public Type TargetType { get; set; }
        public string BaseUrl { get; set; }
        private readonly HttpRequestBuilder _requestBuilder = new HttpRequestBuilder();
        private readonly ExceptionHandler _exceptionHandler = new ExceptionHandler();
        private readonly CapellaHttpClient _httpClient = new CapellaHttpClient();

        public ServiceClient()
        {
        }

        public ServiceClient(ServiceClientPipeline pipeline)
        {
            if (pipeline != null)
                _pipeline = pipeline;
        }

        public object Execute(CallContext ctx)
        {
            try
            {
                _pipeline.OnMethodInvoked(ctx);

                var request = _requestBuilder.Create(ctx);
                request.BaseUrl = BaseUrl;

                request = _pipeline.OnRequestCreated(request);

                string response;
                try
                {
                    response = _httpClient.ExecuteRequest(request);
                }
                catch (WebException e)
                {
                    var resp = e.Response;
                    var reader = new StreamReader(resp.GetResponseStream());
                    string content = reader.ReadToEnd();

                    var exception = _exceptionHandler.Handle(e, content);
                    _pipeline.OnException(exception);
                    throw exception;
                }

                if (ctx.Method.ReturnType == typeof (void))
                    return null;

                var result = JsonConvert.DeserializeObject(response, ctx.Method.ReturnType);
                _pipeline.OnMethodReturned(ctx, result);
                return result;
            }
            catch (Exception e)
            {
                _pipeline.OnException(e);
                throw;
            }
        }
    }
}



//public async Task<object> ExecuteAsync(CallContext ctx)
//{
//    var request = new Request()
//    {
//        ServiceLocation = ServiceLocation,
//        Url = ServiceLocation + TargetType.Name + "/" + ctx.MethodName,
//        Context = ctx,
//        ContentType = "application/json",
//        AcceptEncoding = "gzip",
//        Method = "POST",
//        Body = String.Join("|", ctx.Arguments.ToList().Select(JsonConvert.SerializeObject)),
//        Cookies = new Dictionary<string, string>()
//    };

//    if (OnRequest != null)
//        OnRequest(request);

//    string str;
//    using (var wc = new DecompressingWebClient())
//    {
//        wc.Headers[HttpRequestHeader.ContentType] = request.ContentType;
//        wc.Headers[HttpRequestHeader.AcceptEncoding] = request.AcceptEncoding;
//        wc.Headers[HttpRequestHeader.Cookie] = Request.ToCookieString(request.Cookies);
//        str = await wc.UploadStringTaskAsync(new Uri(request.Url), request.Body);
//    }

//    var result = JsonConvert.DeserializeObject(str, ctx.ReturnType);
//    return result;
//}