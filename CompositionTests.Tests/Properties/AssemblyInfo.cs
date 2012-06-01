using System.Reflection;
using System.Runtime.InteropServices;
using ApprovalTests.Reporters;

[assembly: UseReporter(typeof(DiffReporter))]
[assembly: AssemblyTitle("CompositionTests.Tests")]
[assembly: AssemblyDescription("Tests for CompositionTests")]
[assembly: AssemblyProduct("CompositionTests.Tests")]
[assembly: ComVisible(false)]
[assembly: Guid("b45a75c1-fab4-4cbe-a78c-9ff6b70de153")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]