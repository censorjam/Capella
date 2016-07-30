using System;

namespace TestApp.Shared
{
    public class TestService : ITestService
    {
        public string TestMethod1()
        {
            return "hello world";
        }

        public TestData TestMethod2(TestData data)
        {
            return data;
        }
    }
}