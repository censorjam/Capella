using System;
using System.Reflection;

namespace Capella.Core
{
    public interface IRouter
    {
        Route Get(Type type, MethodInfo methodInfo);
    }
}