using System;
using System.Reflection;

namespace Capella.Core
{
    public class DefaultRouter : IRouter
    {
        public Route Get(Type type, MethodInfo methodInfo)
        {
            return new Route() { Method = "POST", Path = "/" + type.Name + "/" + methodInfo.Name };
        }
    }
}