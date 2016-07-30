using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Capella.Core;
using Capella.Host.Exceptions;

namespace Capella.Host
{
    public class RequestHandler
    {
        public IRouter Router = new DefaultRouter();
        public IDictionary<string, Dispatcher> Dispatchers = new ConcurrentDictionary<string, Dispatcher>();
        private readonly HashSet<Type> RegisteredTypes = new HashSet<Type>();
        private readonly object _sync = new object();

        public bool CanHandle(string method, string url)
        {
            return Dispatchers.ContainsKey(new Route() { Method = method, Path = url}.ToString());
        }

        public object Invoke(string method, string url, string body)
        {
            return Dispatchers[new Route() {Method = method, Path = url}.ToString()].Execute(body);
        }

        public void Register<T>(T implementation) where T : class
        {
            lock (_sync)
            {
                var type = typeof(T);

                if (RegisteredTypes.Contains(type))
                    throw new InvalidTypeException(string.Format("Type has already been registered ({0})", type.FullName));

                RegisteredTypes.Add(type);

                if (type.IsGenericType)
                    throw new InvalidTypeException(string.Format("Unable to host generic targets ({0})", type.FullName));

                foreach (var methodInfo in type.GetMethods())
                {
                    var route = Router.Get(type, methodInfo);

                    //if (Dispatchers.ContainsKey(route.ToString()))
                    //    throw new InvalidRouteException(string.Format("Ambiguous routes found on {0}. Use the RouteAttribute to define a unique route or the IgnoreAttribute to ignore one of methods", route.ToString()));

                    Dispatchers[route.ToString()] = new Dispatcher(implementation, methodInfo);
                }
            }
        }
    }
}