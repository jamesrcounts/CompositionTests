using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Reflection;
using ApprovalTests;
using Microsoft.ComponentModel.Composition.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompositionTests.Tests
{
    [TestClass]
    public class MefCompositionTest
    {
        [TestMethod]
        public void TestAggregateCatalog()
        {
            MefComposition.VerifyAggregateCatalog(GetAggregateCatalog());
        }

        [TestMethod]
        public void TestAssembly()
        {
            MefComposition.VerifyAssemblyCatalog(Assembly.GetExecutingAssembly());
        }

        [TestMethod]
        public void TestAssemblyCatalog()
        {
            MefComposition.VerifyAssemblyCatalog(GetAssemblyCatalog());
        }

        [TestMethod]
        public void TestAssemblyPath()
        {
            MefComposition.VerifyAssemblyCatalog(Assembly.GetExecutingAssembly().Location);
        }

        [TestMethod]
        public void TestComposablePartCatalog()
        {
            ComposablePartCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            MefComposition.VerifyCompositionInfo(
                catalog,
                s => s.ScrubVersionNumber().ScrubPublicKeyToken());
        }

        [TestMethod]
        public void TestGenericWithComposablePartCatalog()
        {
            ComposablePartCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            TestGeneric(catalog);
        }

        [TestMethod]
        public void TestComposablePartCatalogAndExportProvider()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var compositionContainer = new CompositionContainer(catalog);
            MefComposition.VerifyCompositionInfo(
                catalog,
                compositionContainer,
                s => s.ScrubPublicKeyToken(),
                s => s.ScrubVersionNumber());
        }

        [TestMethod]
        public void TestCompositionInfoFactory()
        {
            Func<CompositionInfo> getCompositionInfo = () =>
            {
                var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                return new CompositionInfo(catalog, new CompositionContainer(catalog));
            };
            MefComposition.VerifyCompositionInfo(getCompositionInfo, s => s.ScrubPublicKeyToken(), s => s.ScrubVersionNumber());
        }

        [TestMethod]
        public void TestDirectoryCatalog()
        {
            MefComposition.VerifyDirectoryCatalog(GetDirectoryCatalog());
        }

        [TestMethod]
        public void TestDirectoryPath()
        {
            MefComposition.VerifyDirectoryCatalog(GetDirectoryCatalogPath());
        }

        [TestMethod]
        public void TestDirectoryPathAndSearchPattern()
        {
            MefComposition.VerifyDirectoryCatalog(GetDirectoryCatalogPath(), "*.exe");
        }

        [TestMethod]
        public void TestGenericWithAggregateCatalog()
        {
            TestGeneric(GetAggregateCatalog());
        }

        [TestMethod]
        public void TestGenericWithAssemblyCatalog()
        {
            TestGeneric(GetAssemblyCatalog());
        }

        [TestMethod]
        public void TestGenericWithDirectoryCatalog()
        {
            TestGeneric(GetDirectoryCatalog());
        }

        [TestMethod]
        public void TestGenericWithTypeCatalog()
        {
            TestGeneric(GetTypeCatalog());
        }

        [TestMethod]
        public void TestReflectionTypeLoadException()
        {
            var consoleError = Console.Error;
            var errorTrap = new StringWriter();
            try
            {
                Console.SetError(errorTrap);
                MefComposition.VerifyCompositionInfo(() =>
                {
                    const string Message = "This is an inner exception.";
                    throw new ReflectionTypeLoadException(new[] { this.GetType() }, new[] { new InvalidOperationException(Message) });
                });
                Assert.Fail("Expected Exception");
            }
            catch (ReflectionTypeLoadException)
            {
                Approvals.Verify(errorTrap.ToString());
            }
            finally
            {
                Console.SetError(consoleError);
            }
        }

        [TestMethod]
        public void TestTypeArray()
        {
            MefComposition.VerifyTypeCatalog(typeof(FileRemover), typeof(TestingPart));
        }

        [TestMethod]
        public void TestTypeCatalog()
        {
            MefComposition.VerifyTypeCatalog(GetTypeCatalog());
        }

        private static AggregateCatalog GetAggregateCatalog()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(typeof(TestingPart)));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(GetDirectoryCatalogPath(), "*.exe"));
            return catalog;
        }

        private static AssemblyCatalog GetAssemblyCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }

        private static DirectoryCatalog GetDirectoryCatalog()
        {
            return new DirectoryCatalog(GetDirectoryCatalogPath());
        }

        private static string GetDirectoryCatalogPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static TypeCatalog GetTypeCatalog()
        {
            return new TypeCatalog(typeof(FileRemover));
        }

        private static void TestGeneric<T>(T aggregateCatalog) where T : ComposablePartCatalog
        {
            MefComposition.VerifyCatalog(aggregateCatalog);
        }

        [Export]
        [PartNotDiscoverable]
        public class SecondTestingPart
        {
            public char MyChar { get; set; }
        }

        [Export]
        public class TestingPart
        {
            [Import]
            public int MyProperty { get; set; }
        }

#pragma warning disable 0618

        #region Obsolete

        [TestMethod]
        public void TestDiscoverParts()
        {
            MefComposition.DiscoverParts(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                s => s.ScrubVersionNumber().ScrubPublicKeyToken());
        }

        [TestMethod]
        public void TestDiscoverPartsWithCatalogAndProvider()
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer compositionContainer = new CompositionContainer(catalog);
            MefComposition.DiscoverParts(
                catalog,
                compositionContainer,
                s => s.ScrubPublicKeyToken(),
                s => s.ScrubVersionNumber());
        }

        [TestMethod]
        public void TestDiscoverPartsWithFactory()
        {
            MefComposition.DiscoverParts(
                () =>
                {
                    var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                    return new CompositionInfo(catalog, new CompositionContainer(catalog));
                },
                s => s.ScrubPublicKeyToken(),
                s => s.ScrubVersionNumber());
        }

        #endregion Obsolete

#pragma warning restore 0618
    }
}