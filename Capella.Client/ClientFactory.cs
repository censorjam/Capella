using Castle.DynamicProxy;

namespace Capella.Client
{
    public class ClientFactory
    {
        public static ServiceClientPipeline Pipeline = null;

        private ClientFactory()
        {
            
        }

        public static T Create<T>(string baseUrl) where T : class
        {
            var generator = new ProxyGenerator();
            var client = new ServiceClient(Pipeline)
            {
                BaseUrl = baseUrl,
                TargetType = typeof(T),
            };
            var proxy = generator.CreateInterfaceProxyWithoutTarget<T>(new ServiceClientInterceptor(typeof(T), client));
            return proxy;
        }
    }
}