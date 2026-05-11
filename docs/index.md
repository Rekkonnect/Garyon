# Garyon Documentation

Garyon is a broad utility library for .NET with helpers for comparison, collections, reflection, process handling, and more.

<!-- This page contains generated navigation blocks derived from `docs/toc.yml`. -->

<!-- BEGIN GENERATED: home-index -->
## Getting Started

- [Installation](guides/installation.md)
- [Quick Start](guides/quick-start.md)
- [Quick Reference](QUICK_REFERENCE.md)
- [API Reference](api/index.md)

## Browse

- [User Guides](guides/index.md)
  - [Getting Started](guides/getting-started.md)
    - [Installation](guides/installation.md)
    - [Quick Start](guides/quick-start.md)
  - [Extensions & Utilities](guides/extensions-utilities.md)
    - [Task Handling & .NoContext](guides/task-handling.md)
    - [CancellationTokenFactory](guides/cancellation-token-factory.md)
    - [Typed Paths (FilePath & DirectoryPath)](guides/paths.md)
    - [Process Extensions](guides/process-extensions.md)
    - [Enumerable & Enumerator Extensions](guides/enumerable-extensions.md)
    - [Dictionary & Collection Helpers](guides/collection-helpers.md)
    - [Upcasting](guides/upcasting.md)
    - [DelegateHelpers](guides/delegate-helpers.md)
    - [NullGuards](guides/null-guards.md)
  - [Advanced Features](guides/advanced-features.md)
    - [AppDomainCache](guides/appdomain-cache.md)
    - [Timers and Delays](guides/timers-and-delays.md)
    - [SimpleProfiler](guides/simple-profiler.md)
    - [AsyncUsableLock](guides/async-usable-lock.md)
    - [Math Utilities](guides/math.md)
    - [Comparison Patterns](guides/comparison-patterns.md)
    - [Message Channels & Yielding](guides/messaging-yielding.md)
    - [Singleton & SharedInstance](guides/singleton-pattern.md)
    - [AdvancedLazy](guides/advanced-lazy.md)
    - [AsyncLazy](guides/async-lazy.md)
<!-- END GENERATED: home-index -->

## Notes

This project is still evolving. Public APIs are usable, but they can still shift between releases.

## Build The Docs Locally

From the repository root:

```powershell
.\docs\build-docs.ps1 -Serve
```

On non-Windows shells:

```bash
./docs/build-docs.sh --serve
```

> [!NOTE]
> The build scripts also regenerate the API namespace index and the conceptual landing pages (the blocks marked as "generated" in the markdown sources). If you run `docfx` directly, those generated sections will not be refreshed.

If you still want to run DocFX directly, run the generator first:

```powershell
python .\docs\generate-nav.py
docfx docs/docfx.json --serve
```
