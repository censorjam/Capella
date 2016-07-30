using System.Reflection;

namespace Capella.Core
{
    public interface IBodyFormatter
    {
        string Serialize(ParameterInfo[] parameters, object[] arguments);
        object[] Deserialize(Route route, ParameterInfo[] parameters, string data);
    }
}