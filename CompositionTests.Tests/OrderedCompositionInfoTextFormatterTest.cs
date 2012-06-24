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
        private static CompositionInfo GetInfo()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            return new CompositionInfo(catalog, new CompositionContainer(catalog));
        }

        [TestMethod]
        public void ApproveFormattedText()
        {
            using (var output = new StringWriter())
            {
                OrderedCompositionInfoTextFormatter.Write(GetInfo(), output);
                Approvals.Verify(output);
            }
        }

        [TestMethod]
        public void TestFormat()
        {
            Approvals.Verify(OrderedCompositionInfoTextFormatter.Format(GetInfo()));
        }
    }
}