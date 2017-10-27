using Capella.CodeGen;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capella.Tests
{
    [TestFixture]
    public class CodeGenTests
    {
        [Test]
        public void Test1()
        {
            var codegen = new ClientGenerator();
            var code = codegen.GenerateClient<ITest>();
            var f = @"C:\Users\marc.jones\Source\Repos\Capella\Capella.Tests\CodeGen\TestClient.cs";
            var file = File.ReadAllText(f);

            Assert.AreEqual(file, code);
        }

        [Test]
        public void Test2()
        {
            var codegen = new ClientGenerator();
            var code = codegen.GenerateMethod(typeof(ITest).GetMethod("ReturnsAString"), true);
            Assert.AreEqual(File.ReadAllText(@"C:\Users\marc.jones\Source\Repos\Capella\Capella.Tests\CodeGen\MethodDefinitionTests\Test2.txt"), code);
        }

        [Test]
        public void Test3()
        {
            var codegen = new ClientGenerator();
            var usings = codegen.GenerateUsings(typeof(ITest));



        }

        [Test]
        public void Test4()
        {
            var codegen = new ClientGenerator();
            var code = codegen.GenerateClient<ITest>();
        }
    }
}
