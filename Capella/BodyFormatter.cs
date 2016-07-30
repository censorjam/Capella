using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;

namespace Capella.Core
{
    public class BodyFormatter
    {
        private readonly Type _requestObjectType;
        private readonly ParameterInfo[] _parameters;
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        private readonly long _typeCount;

        public BodyFormatter(ParameterInfo[] parameters)
        {
            _parameters = parameters;
            TypeCreation tc = new TypeCreation();
            _requestObjectType = tc.Create("Type" + Interlocked.Increment(ref _typeCount), parameters);
        }

        public string Serialize(object[] arguments)
        {
            IDictionary<string, object> e = new ExpandoObject();
            for (var a = 0; a < _parameters.Length; a++)
            {
                e[_parameters[a].Name] = arguments[a];
            }

            var json = JsonConvert.SerializeObject(e, _settings);
            json = json.Replace("\"$type\":\"System.Dynamic.ExpandoObject, System.Core\",","");  //todo add unit test for this
            return json;
        }

        public object[] Deserialize(string data)
        {
            var ps = new object[_parameters.Length];
            if (ps.Length > 0)
            {
                var request = JsonConvert.DeserializeObject(data, _requestObjectType, _settings);
                for (int i = 0; i < _parameters.Length; i++)
                {
                    ps[i] = _requestObjectType.GetField(_parameters[i].Name).GetValue(request);
                }
            }
            return ps;
        }
    }
}
