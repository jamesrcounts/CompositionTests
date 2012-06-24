using ApprovalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompositionTests.Tests
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public void ApprovePublicApi()
        {
            var mefComposition = typeof(MefComposition).Assembly;
            var publicApi = ApiApprover.PublicApiGenerator.CreatePublicApiForAssembly(mefComposition);
            Approvals.Verify(publicApi);
        }
    }
}