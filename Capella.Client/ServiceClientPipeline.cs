using System;

namespace Capella.Client
{
    public class ServiceClientPipeline
    {
        public virtual void OnMethodInvoked(CallContext context)
        {
        }

        public virtual void OnMethodReturned(CallContext context, object result)
        {
        }

        public virtual HttpRequest OnRequestCreated(HttpRequest request)
        {
            return request;
        }

        public virtual void OnException(Exception exception)
        {

        }
    }
}