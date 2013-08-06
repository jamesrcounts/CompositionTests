What's New in CompositionTests 2.0
===

ApprovalTests 3.0
---
Updated the dependency on [ApprovalTests](http://approvaltests.com) to 3.0.01.  Thanks to the [new version updating policy](http://blog.approvaltests.com/2013/07/whats-new-in-approvaltestnet-v-30.html) for ApprovalTests, CompositionTests should remain forward compatible with future versions of ApprovalTests, unless there are breaking changes in the API. 

New version policy
---

Following LLewellyn's lead with ApprovalTests, I am adopting a [JSON.NET-style](http://james.newtonking.com/archive/2012/04/04/json-net-strong-naming-and-assembly-version-numbers.aspx) version update policy.  Adopting this policy will enable me to sign CompositionTests in the future without creating forward-compatibility problems for anyone else.  For now, the package remains unsigned because its other dependency, the [MEFX Diagnostic Library](https://www.nuget.org/packages/MEFX.Core.Unofficial/) is unsigned.  I'll have to decide if I'm willing to do anything about that before I can  consider a signed version of CompositionTests.

The impact is that the CompositionTests AssemblyVersion will stay at 2.0.0 from now on.  The real version can be found by looking at AssemblyFileVersion, or by looking at the nuget package version, which will be 2.0.1 for this release.

Common Language Specification Compliance
---

The CompositionTests library now declares itself CLS compliant.  However, MEFX.Core does not make the same declaration, so certain methods that interact with the core are individually marked non-compliant.  I don't think that MEFX.Core uses anything non-compliant, the library is simply missing the declaration of compliance. I don't think Microsoft has plans to provide any more updates to this piece, so I'll have to decide that I'm willing to modify and maintain a fork of MEFX.Core before I can do anything about that missing attribute.

Removed Obsolete Methods
---

Methods and types marked with the `ObsoleteAttribute` in the 1.0 time-frame have been removed in order to clean up the interface in 2.0.  You must now migrate to `Verify*` and `MefComposition` if you wish to use new versions of the library.
