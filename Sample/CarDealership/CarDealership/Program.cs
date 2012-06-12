using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace CarDealership
{
  public class Program
  {
    static Program()
    {
      Catalog = new AssemblyCatalog(Assembly.GetAssembly(typeof(Program)));
      Host = new CompositionContainer(Catalog);
    }

    public static ComposablePartCatalog Catalog { get; private set; }

    public static ExportProvider Host { get; private set; }

    private static void Main(string[] args)
    {
    }
  }
}