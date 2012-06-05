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
        [TestMethod]
        public void TestDiscoverParts()
        {
            Composition.DiscoverParts(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                s => s.ScrubVersionNumber().ScrubPublicKeyToken());
        }

        [TestMethod]
        public void TestDiscoverPartsWithCatalogAndProvider()
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer compositionContainer = new CompositionContainer(catalog);
            Composition.DiscoverParts(
                catalog,
                compositionContainer,
                s => s.ScrubPublicKeyToken(),
                s => s.ScrubVersionNumber());
        }

        [TestMethod]
        public void TestDiscoverPartsWithFactory()
        {
            Composition.DiscoverParts(
                () =>
                {
                    var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                    return new CompositionInfo(catalog, new CompositionContainer(catalog));
                },
                s => s.ScrubPublicKeyToken(),
                s => s.ScrubVersionNumber());
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

        [Export]
        public class TestingPart
        {
            public int MyProperty { get; set; }
        }
    }
}