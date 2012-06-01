using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using Microsoft.ComponentModel.Composition.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompositionTests.Tests
{
    [TestClass]
    public class CompositionTest
    {
        private const string assemblyCatalogSignature = "AssemblyCatalog (Assembly=\"Microsoft.VisualStudio.QualityTools.UnitTestFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\")";

        [TestMethod]
        public void TestDiscoverParts()
        {
            Composition.DiscoverParts(
                () =>
                {
                    var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                    return new CompositionInfo(catalog, new CompositionContainer(catalog));
                },
                Composition.ScrubPublicKeyToken,
                Composition.ScrubVersionNumber);
        }

        [TestMethod]
        public void TestReflectionTypeLoadException()
        {
            var consoleError = Console.Error;
            var errorTrap = new StringWriter();
            try
            {
                Console.SetError(errorTrap);
                Composition.DiscoverParts(() =>
                {
                    string message = "This is an inner exception.";
                    throw new ReflectionTypeLoadException(new[] { this.GetType() }, new[] { new InvalidOperationException(message) });
                });
                Assert.Fail("Expected Exception");
            }
            catch (ReflectionTypeLoadException)
            {
                ApprovalTests.Approvals.Verify(errorTrap.ToString());
            }
            finally
            {
                Console.SetError(consoleError);
            }
        }

        [TestMethod]
        public void TestScrubPublicKeyToken()
        {
            Assert.AreEqual(
                "AssemblyCatalog (Assembly=\"Microsoft.VisualStudio.QualityTools.UnitTestFramework, Version=10.0.0.0, Culture=neutral\")",
                Composition.ScrubPublicKeyToken(assemblyCatalogSignature));
        }

        [TestMethod]
        public void TestScrubVersionNumber()
        {
            Assert.AreEqual(
                "AssemblyCatalog (Assembly=\"Microsoft.VisualStudio.QualityTools.UnitTestFramework, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\")",
                Composition.ScrubVersionNumber(assemblyCatalogSignature));
        }

        [Export]
        public class TestingPart
        {
            public int MyProperty { get; set; }
        }
    }
}