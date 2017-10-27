using Capella.Client;
using Capella.Host;
using Microsoft.Owin.Hosting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Host;
using TestApp.Shared;

namespace Capella.Tests
{
    [TestFixture]
    public class Class1
    {
        public interface ITestService
        {
            void ParameterLessMethodThatReturnsVoid();
            string ParameterLessMethodThatReturnsString();
            int? NullableInt(int? i);
            void DateTimeParameter(DateTime datetime);
            void GenericParam(List<string> l);
            List<string> ReturnsGeneric();
            Task<int> AsyncInt();
        }

        public string Url { get; set; }

        public Mock<ITestService> MockService { get; set; }
        public ITestService Client { get; set; }

        [SetUp]
        public void SetUp()
        {
            Url = "http://" + Environment.MachineName + ":1000";
            MockService = new Mock<ITestService>();
            Client = ClientFactory.Create<ITestService>(Url);
            WebApp.Start<Startup>(Url);
            CapellaMiddleware.Handler.Register<ITestService>(MockService.Object);
        }

        [Test]
        public void ParameterLessMethodThatReturnsVoid()
        {
            Client.ParameterLessMethodThatReturnsVoid();
            MockService.Verify(x => x.ParameterLessMethodThatReturnsVoid());
        }

        [Test]
        public void ParameterLessMethodThatReturnsString()
        {
            MockService.Setup(m => m.ParameterLessMethodThatReturnsString()).Returns("hello, world");
            var result = Client.ParameterLessMethodThatReturnsString();
            MockService.Verify(x => x.ParameterLessMethodThatReturnsString());
            Assert.AreEqual(result, "hello, world");
        }

        [Test]
        public void NullableIntReturnsInt()
        {
            MockService.Setup(m => m.NullableInt(It.IsAny<int>())).Returns(int.MaxValue);
            var result = Client.NullableInt(5);
            MockService.Verify(x => x.NullableInt(It.IsAny<int>()));
            Assert.AreEqual(result, int.MaxValue);
        }

        [Test]
        public void NullableIntAcceptsNull()
        {
            MockService.Setup(m => m.NullableInt(It.Is<int?>(null))).Returns<int?>(null);
            var result = Client.NullableInt(null);
            MockService.Verify(x => x.NullableInt(It.Is<int?>(null)));
        }

        [Test]
        public void X()
        {
            MockService.Setup(m => m.NullableInt(It.Is<int?>(null))).Returns<int?>(null);
            var result = Client.NullableInt(null);
            MockService.Verify(x => x.NullableInt(It.Is<int?>(null)));
        }

        [Test]
        public void X21()
        {
            MockService.Setup(m => m.NullableInt(It.Is<int?>(null))).Returns<int?>(null);
            var result = Client.NullableInt(null);
            MockService.Verify(x => x.NullableInt(It.Is<int?>(null)));
        }

        [Test]
        public void GenericParam()
        {
            MockService.Setup(m => m.GenericParam(It.IsAny<List<string>>()));
            Client.GenericParam(new List<string>() { "a", "b", "c" });
            MockService.Verify(x => x.GenericParam(It.Is<List<string>>((a) => a[0] == "a" && a[1] == "b" && a[2] == "c")));
        }

        [Test]
        public void ReturnGeneric()
        {
            MockService.Setup(m => m.ReturnsGeneric()).Returns(new List<string>() { "a", "b" });
            var result = Client.ReturnsGeneric();
            Assert.AreEqual("a", result[0]);
            Assert.AreEqual("b", result[1]);
        }

        [Test]
        public void DateTimeParameter()
        {
            MockService.Setup(m => m.DateTimeParameter(It.IsAny<DateTime>()));
            Client.DateTimeParameter(new DateTime(2001,02,03));
            MockService.Verify(x => x.DateTimeParameter(It.Is<DateTime>((a) => a == new DateTime(2001, 02, 03))));
        }
    }
}
