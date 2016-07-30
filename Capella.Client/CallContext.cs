using System;
using System.Reflection;

namespace Capella.Client
{
    public class CallContext
    {
        public Type Type { get; set; }
        public MethodInfo Method { get; private set; }
        public object[] Arguments { get; private set; }

        public CallContext(Type type, MethodInfo mi, object[] arguments)
        {
            Type = type;
            Method = mi;
            Arguments = arguments;
        }
    }
}