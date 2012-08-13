What's New in CompositionTests 1.3
===

Generic VerifyCatalog Implementation
---

CompositionTests 1.2 introduced customized `Verify*Catalog` convenience methods that automatically provide scrubbers that make sense with each catalog type.  Although this worked fine, using the convenience methods meant that you needed to make an extra change to your test if you decided to change the catalog type... not very convenient!  

The new `VerifyCatalog<T>` method addresses this shortcoming.  The new method will take any catalog descended from `ComposablePartCatalog` and attempt to find an appropriate `Verify*Catalog` implementation.  When the method cannot find an appropriate implementation, the default is to use `VerifyCompositionInfo` with no scrubbers.

ApprovalTests 2.0
---

This version of CompositionTests is built against [ApprovalTests 2.0](http://blog.approvaltests.com/2012/08/whats-new-in-approvaltestsnet-v20.html "ApprovalTests Blog").  The CompositionTests 1.2.0 NuGet package incorrectly specified the ApprovalTests dependency as `x >= 1.9`.  Because the ApprovalTest assemblies are signed, dependent assemblies (like CompositionTests) will only load the ApprovalTests assembly which they are built against, so "greater than or equal to" should have been "exactly equal to".  The package for CompositionTests 1.3.0 correctly specifies the dependency as `x == 2.0`.

If CompostionTests stopped working for you after upgrading to ApprovalTests 2.0, this release should fix that for you.  


Reminder
---

Remember `DiscoverParts` and `Composition` are obsolete and will be removed in a future release.  Please migrate to `Verify*` and `MefComposition`.