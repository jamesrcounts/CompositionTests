CompositionTests
====

Uses ApprovalTests and MEFX to provide a simple interface for executing MEF Composition tests.

What can it be used for?
---

When you know what your composition should look like, you can create automated integration tests that lock down the composition.

Examples
---

Say you have an attributed MEF part:

    [Export(typeof(Car))]
    public class Ford : Car

You could use the MEFX Command Line tool to examine the type and you would expect output like this:

	[Part] CarDealership.Ford from: TypeCatalog (Types='CarDealership.Ford').
	  [Export] CarDealership.Ford (ContractName="CarDealership.Car")
	
Let's assume that this is correct, and that someday down the line a junior programmer decides there should be an `ICar` interface and changes your export to this:

    [Export(typeof(ICar))]
    public class Ford : Car, ICar

Now MEF can't satisfy any `Car` imports you might have.  MEFX can catch this type of breakage (and much more) but you have to run it yourself.  Wouldn't it be nice to run an automated check that would catch this scenario? `CompositionTests` exists to fill that gap:

	using ApprovalTests.Reporters;
	using CompositionTests;
	
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class IntegrationTest
	{
	    [TestMethod]
	    public void VerifyComposition()
	    {
	        var catalog = new TypeCatalog(typeof(Ford));
	        MefComposition.VerifyCompositionInfo(catalog);
	    }
	}

This test will leverage the core library behind MEFX to produce the same output you would see on the command line, then use [ApprovalTests](http://www.approvaltests.com) to process the output and ensure that it doesn't change without your approval.  You can use CompositionTests with 4 of the 5 MEF catalog types (`DeploymentCatalog` is not supported at this time.)  And because we're automating MEFX, the test will not only show you the contents of the catalog, but will perform static analysis on the catalog contents, tell you which parts will be rejected, and idenitify possible rejection root causes.

Lets say our `Ford : Car` needs a motor:

    [Export(typeof(Car))]
    public class Ford : Car
    {
        [Import]
        public IMotor Motor { get; set; } 

If our catalog doesn't have motor, our test will let us know:

	[Part] CarDealership.Ford from: TypeCatalog (Types='CarDealership.Ford').
	  [Primary Rejection]
	  [Export] CarDealership.Ford (ContractName="CarDealership.Car")
	  [Import] CarDealership.Ford.Motor (ContractName="CarDealership.IMotor")
	    [Exception] System.ComponentModel.Composition.ImportCardinalityMismatchException: No exports were found that match the constraint: 
	    ContractName    CarDealership.IMotor
	    RequiredTypeIdentity    CarDealership.IMotor
	   at System.ComponentModel.Composition.Hosting.ExportProvider.GetExports(ImportDefinition definition, AtomicComposition atomicComposition)
	   at System.ComponentModel.Composition.Hosting.ExportProvider.GetExports(ImportDefinition definition)
	   at Microsoft.ComponentModel.Composition.Diagnostics.CompositionInfo.AnalyzeImportDefinition(ExportProvider host, IEnumerable`1 availableParts, ImportDefinition id)

Then we know that we need to add a `Motor` part to satisfy the `Car`

    [TestMethod]
    public void VerifyComposition()
    {
        var catalog = new TypeCatalog(typeof(Ford), typeof(V8Motor));
        MefComposition.VerifyCompositionInfo(catalog);
    }

And now our composition is happy, we can approve the new output to ensure that no one removes the `IMotor` to make our `Car` unusable.

	[Part] CarDealership.Ford from: TypeCatalog (Types='CarDealership.Ford, CarDealership.V8Motor').
	  [Export] CarDealership.Ford (ContractName="CarDealership.Car")
	  [Import] CarDealership.Ford.Motor (ContractName="CarDealership.IMotor")
	    [SatisfiedBy] CarDealership.V8Motor (ContractName="CarDealership.IMotor") from: CarDealership.V8Motor from: TypeCatalog (Types='CarDealership.Ford, CarDealership.V8Motor').
	
	[Part] CarDealership.V8Motor from: TypeCatalog (Types='CarDealership.Ford, CarDealership.V8Motor').
	  [Export] CarDealership.V8Motor (ContractName="CarDealership.IMotor")
	
But instead of building your catalog in your test, it would be a better idea to define it in your production code:
	
	public class Program
	{
	    static Program()
	    {
	        Catalog = new TypeCatalog(typeof(Ford), typeof(V8Motor));
	        Host = new CompositionContainer(Catalog);
	    }
	
	    public static TypeCatalog Catalog { get; private set; }

And share that catalog with the test:

    [TestMethod]
    public void VerifyComposition()
    {
        MefComposition.VerifyCompositionInfo(Program.Catalog);
    }

But sometimes MEFX puts extra information into the output that will break your ApprovalTest, even though nothing has actually changed that effects compostion (like version numbers and file paths).  So CompositionTests provides specific methods per catalog that can scrub common problem areas for you:

    [TestMethod]
    public void VerifyComposition()
    {
        MefComposition.VerifyTypeCatalog(Program.Catalog);
    }

Or you can define any custom `Func<string, string>` to manipulate the output before it reaches ApprovalTests:

    [TestMethod]
    public void VerifyComposition()
    {
        MefComposition.VerifyTypeCatalog(Program.Catalog, s => RemoveTypeNames(s));
    }

If you need to use an `ExportProvider` other than `CompositionContainer`, you can do that too, just check the overloads of `VerifyCompositionInfo` for one that works for you, and see the test project for more examples.

More Info
---

####Related Blog Posts:

[Stop Guessing About MEF Composition and Start Testing](http://ihadthisideaonce.wordpress.com/2012/01/31/stop-guessing-about-mef-composition-and-start-testing/)

[MEF Composition Tests, Redux](http://ihadthisideaonce.wordpress.com/2012/06/12/mef-composition-tests-redux/)

Available on NuGet
---

[Install-Package CompositionTests](https://nuget.org/packages/CompositionTests)

License
---

[MIT License](https://github.com/jamesrcounts/CompositionTests/blob/master/LICENSE.md)

Building the source
---

Windows

After cloning the repository, run msbuild, or open in Visual Studio and select "Build Solution" from the Build menu.

Public API
---
See [ApiTest.ApprovePublicApi.approved.txt](https://github.com/jamesrcounts/CompositionTests/blob/master/CompositionTests.Tests/ApiTest.ApprovePublicApi.approved.txt) in the [CompositionTests.Tests](https://github.com/jamesrcounts/CompositionTests/tree/master/CompositionTests.Tests) directory.

Questions?
---

twitter: [@jamesrcounts](https://twitter.com/#!/jamesrcounts) or #CompositionTests