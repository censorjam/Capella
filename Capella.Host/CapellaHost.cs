namespace Capella.Host
{
    public class CapellaHost
    {
        private RequestHandler _requestHandler = new RequestHandler();

        public CapellaHost()
        {
        }

        public void Register<T>(T implementation) where T : class
        {
            _requestHandler.Register(implementation);
        }
    }
}