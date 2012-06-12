using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Reflection;
using ApprovalTests;
using Microsoft.ComponentModel.Composition.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarDealership.Tests
{
  [TestClass]
  public class IntegrationTest
  {
    [TestMethod]
    public void DiscoverParts()
    {
      DiscoverParts(Program.Catalog, Program.Host);
    }

    private static void DiscoverParts(ComposablePartCatalog catalog, ExportProvider host)
    {
      DiscoverParts(() => new CompositionInfo(catalog, host));
    }

    private static void DiscoverParts(Func<CompositionInfo> getCompositionInfo)
    {
      try
      {
        using (var stringWriter = new StringWriter())
        {
          CompositionInfoTextFormatter.Write(
              getCompositionInfo(),
              stringWriter);
          Approvals.Verify(stringWriter.ToString());
        }
      }
      catch (ReflectionTypeLoadException ex)
      {
        Array.ForEach(
            ex.LoaderExceptions,
            lex => Console.WriteLine(lex.ToString()));
        throw;
      }
    }
  }
}