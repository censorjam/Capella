using System;
using Newtonsoft.Json;

namespace Capella.Client
{
    public class ExceptionHandler
    {
        public Exception Handle(Exception originalException, string content)
        {
            Exception ex;
            try
            {
                try
                {
                    ex = (Exception)JsonConvert.DeserializeObject(content, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                }
                catch
                {
                    ex = JsonConvert.DeserializeObject<Exception>(content);
                }
            }
            catch
            {
                ex = originalException;
            }
            return ex;
        }
    }
}