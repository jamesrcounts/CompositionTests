using CompositionTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarDealership.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void VerifyComposition()
        {
            MefComposition.VerifyAssemblyCatalog(Program.Catalog);
        }
    }
}