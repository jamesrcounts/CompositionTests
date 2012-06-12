using CompositionTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarDealership.Tests
{
  [TestClass]
  public class IntegrationTest
  {
    [TestMethod]
    public void DiscoverParts()
    {
      Composition.DiscoverParts(Program.Catalog, StringExtensions.ScrubVersionNumber);
    }
  }
}