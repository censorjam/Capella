using Capella.Host;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Shared;

namespace TestApp.Host
{
    //public interface ITest
    //{
    //    object DoThing(string thing);
    //}

    //public class Test : ITest
    //{
    //    public object DoThing(string thing)
    //    {
    //        return thing;
    //    }
    //}

    class Program
    {
        private static void Main(string[] args)
        {
            //var target = new Test();

            //var requestHandler = new RequestHandler();

            //requestHandler.Register<ITest>(target);

            ////var dispatcher = new Dispatcher(target, typeof(Test).GetMethod("DoThing"));
            ////var result = dispatcher.Execute("{'thing': 'teststring'}");

            //var result = requestHandler.Invoke("POST", "/ITest/DoThing", "{'thing': 'teststring'}");

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //for (var i = 0; i < 1000000; i++)
            //{
            //    var canHandle = requestHandler.CanHandle("POST", "/ITest/DoThing");
            //    if (canHandle)
            //        result = requestHandler.Invoke("POST", "/ITest/DoThing", "{'thing': 'teststring'}");
            //}

            //sw.Stop();

            //Console.WriteLine(sw.ElapsedMilliseconds);

            //Console.WriteLine(result);

            WebApp.Start<Startup>(String.Concat((object) ("http://" + Environment.MachineName + ":1000")));

            CapellaMiddleware.Handler.Register<ITestService>(new TestService());

            Console.ReadLine();
        }
    }
}
