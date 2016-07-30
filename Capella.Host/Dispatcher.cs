using System.Reflection;
using Capella.Core;
using Newtonsoft.Json;

namespace Capella.Host
{
    public class Dispatcher
    {
        private readonly MethodInfo _methodInfo;
        private readonly object _target;
        private readonly BodyFormatter _serializer;

        public Dispatcher(object target, MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
            _target = target;
            _serializer = new BodyFormatter(_methodInfo.GetParameters());
        }

        public string Execute(string body)
        {
            var ps = _serializer.Deserialize(body);
            var result = _methodInfo.Invoke(_target, ps);
            var resultJson = JsonConvert.SerializeObject(result);
            return resultJson;
        }
    }
}