using System.ComponentModel.Composition;
using CompositionTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailApp.Tests
{
    /// <summary>
    /// This example shows one scenario where CompositionInfo needs both catalog and host
    /// to fully analyze composition.
    /// </summary>
    [TestClass]
    public class CompositionTest
    {
        static CompositionTest()
        {
            Program.AddInstance(new TestingSmtpHost());
        }

        [TestMethod]
        public void VerifyComposition()
        {
            MefComposition.VerifyCompositionInfo(Program.Catalog, Program.Host);
        }

        [Export(typeof(SmtpHost))]
        public class TestingSmtpHost : SmtpHost
        {
            public TestingSmtpHost()
                : base("localhost")
            {
            }
        }
    }
}