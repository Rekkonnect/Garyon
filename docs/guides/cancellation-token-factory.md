# CancellationTokenFactory

`CancellationTokenFactory` is a small helper that keeps a “current” `CancellationTokenSource` and automatically re-instantiates it after the previous source has been cancelled.

This is useful for repeated operations (search, refresh, polling, etc.) where you want a simple “cancel current run / next run gets a fresh token” workflow.

## Basic Usage

```csharp
using Garyon.Objects;

var factory = new CancellationTokenFactory();

// Start an operation with the current token
await DoWorkAsync(factory.CurrentToken);

// Cancel the current token source (if any)
factory.Cancel();

// The next time you read CurrentToken/CurrentSource after cancellation,
// the factory creates a new CTS lazily.
await DoWorkAsync(factory.CurrentToken);
```

## Notes

- `Cancel()` cancels the current source; it does not create a new source immediately.
- `Cancel()` cancels and disposes the current source (via `CancelDispose()`).
- Capture `CurrentToken` once per operation and pass it through (avoid re-reading it mid-operation).
- `CancellationTokenFactory` does not synchronize access; add your own locking if used concurrently.
- `Dispose()` disposes the current source without cancelling it.

## API Reference

- [CancellationTokenFactory](../api/Garyon.Objects.CancellationTokenFactory.yml)
- [CancellationTokenSourceExtensions](../api/Garyon.Extensions.CancellationTokenSourceExtensions.yml)
