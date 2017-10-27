using System.Threading.Tasks;

namespace test
{
    public class ClientBase
    {
        public object Execute(string method, object[] parameters)
        {
            return null;
        }

        public Task<object> ExecuteAsync(string method, object[] parameters)
        {
            return Task.FromResult<object>(null);
        }
    }
}