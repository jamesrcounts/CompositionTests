What's New in CompositionTests 2.0
===

New version policy
---

Following LLewellyn's lead with ApprovalTests, I am adopting a [JSON.NET-style](http://james.newtonking.com/archive/2012/04/04/json-net-strong-naming-and-assembly-version-numbers.aspx) version update policy.  Thanks to this policy, I no longer have to release a new version of CompositionTests every time ApprovalTests changes its own version.  Adopting this policy will enable me to sign CompositionTests in the future without creating a similar burden on anyone else.  For now, the package remains unsigned because its other dependency, the [MEFX Diagnostic Library](https://www.nuget.org/packages/MEFX.Core.Unofficial/) is unsigned.  I'll have to decide if I'm willing to do anything about that before I can even consider a signed version of CompositionTests.

To summarize, AssemblyVersion will stay at 2.0.0, the real version can be found by looking at AssemblyFileVersion, or by looking at the nuget package version, which will be 2.0.1 for this release.

Common Language Specification Compliance
---

The CompositionTests library now declares itself CLS compliant.  However, MEFX.Core does not make the same declaration, so certain methods that interact with the core are individually marked non-compliant.  Again, I'll have to decide that I'm willing to modify and maintain a fork of MEFX.Core before I can do anything about that, since I don't think Microsoft has plans to provide any more updates to this piece.  

Removed Obsolete Methods
---

Methods and types marked with the `ObsoleteAttribute` in the 1.0 time-frame have been removed in order to clean up the interface in 2.0.  You must now migrate to `Verify*` and `MefComposition` if you wish to use new versions of the library.
