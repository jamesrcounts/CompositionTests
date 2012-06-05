using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using ApprovalTests;
using Microsoft.ComponentModel.Composition.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompositionTests.Tests
{
    public interface IFileInfoProxy
    {
    }

    public interface IRemoveFiles
    {
        void Push(IFileInfoProxy source);
    }

    [Export(typeof(IRemoveFiles))]
    public class FileRemover : IRemoveFiles
    {
        public void Push(IFileInfoProxy source)
        {
            return;
        }
    }

    [TestClass]
    public class OrderedCompositionInfoTextFormatterTest
    {
        [TestMethod]
        public void ApproveFormattedText()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            var info = new CompositionInfo(catalog, container);
            using (var output = new StringWriter())
            {
                OrderedCompositionInfoTextFormatter.Write(info, output);
                Approvals.Verify(output);
            }
        }
    }
}