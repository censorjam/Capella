using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capella.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test1()
        {
            var x = 5;
            Assert.AreEqual(5, x);
        }
    }
}
