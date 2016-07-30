using System;
using Castle.DynamicProxy;

namespace Capella.Client
{
    internal class ServiceClientInterceptor : IInterceptor
    {
        public ServiceClientInterceptor(Type type, ServiceClient client)
        {
            _type = type;
            _client = client;
        }

        private readonly Type _type;
        private readonly ServiceClient _client;

        public void Intercept(IInvocation invocation)
        {
            //if (invocation.Method.ReturnType == typeof (Task) || invocation.Method.ReturnType == typeof (Task<>))
            //{
            invocation.ReturnValue = _client.Execute(new CallContext(_type, invocation.Method, invocation.Arguments));
            //}
        }
    }
}
