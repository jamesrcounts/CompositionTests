using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompositionTests.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        private const string assemblyCatalogSignature = "AssemblyCatalog (Assembly=\"Microsoft.VisualStudio.QualityTools.UnitTestFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\")";

        [TestMethod]
        public void TestScrubPublicKeyToken()
        {
            Assert.AreEqual(
                "AssemblyCatalog (Assembly=\"Microsoft.VisualStudio.QualityTools.UnitTestFramework, Version=10.0.0.0, Culture=neutral\")",
                assemblyCatalogSignature.ScrubPublicKeyToken());
        }

        [TestMethod]
        public void ReturnNullForNullOnScrubPublicKeyToken()
        {
            string signature = null;
            Assert.IsNull(signature.ScrubPublicKeyToken());
        }

        [TestMethod]
        public void TestScrubVersionNumber()
        {
            Assert.AreEqual(
                "AssemblyCatalog (Assembly=\"Microsoft.VisualStudio.QualityTools.UnitTestFramework, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\")",
                assemblyCatalogSignature.ScrubVersionNumber());
        }

        [TestMethod]
        public void ReturnNullForNullOnScrubVersionNumber()
        {
            string signature = null;
            Assert.IsNull(signature.ScrubVersionNumber());
        }
    }
}