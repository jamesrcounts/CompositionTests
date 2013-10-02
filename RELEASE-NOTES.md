What's New in CompositionTests 2.0.2
===

ApprovalTests 3.0.2
---
Updated the dependency on [ApprovalTests](http://approvaltests.com) to 3.0.2.  This enabled a small refactoring because
`Approvals.Verify()` now includes an overload which accepts a scrubber delegate to perform arbitrary formatting, cleanup
or redaction.  CompositionTests previously implemented this on its own, but no longer needs to.  No changes to the API.
