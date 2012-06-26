using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MailApp
{
    public class Program
    {
        static Program()
        {
            Catalog = new TypeCatalog(typeof(BasicMailSender), typeof(DefaultPort));
            Host = new CompositionContainer(Catalog);
        }

        public static TypeCatalog Catalog { get; private set; }

        public static CompositionContainer Host { get; private set; }

        public static void AddInstance(object part)
        {
            var batch = new CompositionBatch();
            batch.AddPart(part);
            Host.Compose(batch);
        }

        public static void Main(string[] args)
        {
        }
    }
}