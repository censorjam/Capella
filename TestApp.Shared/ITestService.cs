using System.Threading.Tasks;

namespace TestApp.Shared
{
    public class TestData
    {
        public string A { get; set; }
        public int B { get; set; }
    }

    public interface ITestService
    {
        string TestMethod1();
        TestData TestMethod2(TestData data);
    }

    public interface ITestServiceAsync
    {
        Task<string> TestMethod1();
        Task<TestData> TestMethod2(TestData data);
    }
}
