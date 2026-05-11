# Extensions & Utilities

These guides cover the extension APIs that are meant to be dropped directly into application code.

<!-- BEGIN GENERATED: child-links -->
- [Task Handling & .NoContext](task-handling.md)
- [CancellationTokenFactory](cancellation-token-factory.md)
- [Typed Paths (FilePath & DirectoryPath)](paths.md)
- [Process Extensions](process-extensions.md)
- [Enumerable & Enumerator Extensions](enumerable-extensions.md)
- [Dictionary & Collection Helpers](collection-helpers.md)
- [Upcasting](upcasting.md)
- [DelegateHelpers](delegate-helpers.md)
- [NullGuards](null-guards.md)
<!-- END GENERATED: child-links -->

## Alternatives & Ecosystem

Garyon focuses on pragmatic, low-friction helpers and extension APIs. If you're evaluating "utility libraries" in general, these popular packages may also be worth a look (see their NuGet pages for details, licensing, and supported frameworks):

- LINQ / sequence helpers: [MoreLINQ](https://www.nuget.org/packages/morelinq)
- Text / formatting helpers: [Humanizer](https://www.nuget.org/packages/Humanizer)
- Guard clauses: [Ardalis.GuardClauses](https://www.nuget.org/packages/Ardalis.GuardClauses)
- Functional helpers: [LanguageExt.Core](https://www.nuget.org/packages/LanguageExt.Core), [CSharpFunctionalExtensions](https://www.nuget.org/packages/CSharpFunctionalExtensions), [OneOf](https://www.nuget.org/packages/OneOf)
- Async helpers: [Nito.AsyncEx](https://www.nuget.org/packages/Nito.AsyncEx)
- High-performance primitives: [CommunityToolkit.HighPerformance](https://www.nuget.org/packages/CommunityToolkit.HighPerformance)

> **Disclaimer**: The list above is provided to reduce search time, not as an endorsement. Garyon remains a viable and solid alternative for many "everyday helpers" and extension-driven workflows (especially where you prefer a single, cohesive utility package over stitching together many small dependencies).
