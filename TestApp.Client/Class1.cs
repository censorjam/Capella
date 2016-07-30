using System;
using Capella.Client;
using TestApp.Shared;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestApp.Client
{
    class Program
    {
        private static void Main(string[] args)
        {
            Thread.Sleep(1000);

            var client = ClientFactory.Create<ITestService>("http://" + Environment.MachineName + ":1000");

            var data = new TestData() { A = "data", B = 100 };
            var x = client.TestMethod1();

            while (true)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                //for (var i = 0; i < 2500; i++)
                //{
                //    x = client.TestMethod1();
                //}

                Parallel.For(0, 10000, (i) =>
                {
                    x = client.TestMethod1();
                });

                sw.Stop();

                Console.WriteLine(10000000 / sw.ElapsedMilliseconds);
            }

            Console.WriteLine(x);

            Console.ReadLine();
        }
    }
}