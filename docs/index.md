# Garyon Documentation

Garyon is a broad utility library for .NET with helpers for comparison, collections, reflection, process handling, and more.

## Getting Started

- [Installation Guide](guides/installation.md)
- [Quick Start](guides/quick-start.md)
- [Quick Reference](QUICK_REFERENCE.md)
- [API Reference](api/toc.yml)

## User Guides

### Extensions & Utilities

- [Task Handling & .NoContext](guides/task-handling.md)
- [CancellationTokenFactory](guides/cancellation-token-factory.md)
- [Process Extensions](guides/process-extensions.md)
- [Enumerable & Enumerator Extensions](guides/enumerable-extensions.md)
- [Dictionary & Collection Helpers](guides/collection-helpers.md)
- [Upcasting](guides/upcasting.md)

### Advanced Features

- [AppDomainCache](guides/appdomain-cache.md)
- [Math Utilities](guides/math.md)
- [Comparison Patterns](guides/comparison-patterns.md)
- [Message Channels & Yielding](guides/messaging-yielding.md)
- [Singleton & SharedInstance](guides/singleton-pattern.md)

## Notes

This project is still evolving. Public APIs are usable, but they can still shift between releases.

## Build The Docs Locally

From the repository root:

```powershell
.\docs\build-docs.ps1 -Serve
```

Or directly with DocFX:

```bash
docfx docs/docfx.json --serve
```
