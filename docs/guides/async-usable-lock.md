# AsyncUsableLock

<xref:Garyon.Mechanisms.AsyncUsableLock> is an async-friendly lock that supports the `using`-disposal pattern.

It is useful when you need to protect a critical section in an `async` method (where `lock (...) { ... }` cannot be used with `await`).

## Basic usage (`await` + `using`)

```csharp
using Garyon.Mechanisms;

private readonly AsyncUsableLock _gate = new();

public async Task UpdateAsync()
{
    using var releaser = await _gate.LockAsync();

    // This section is protected; it is released when `releaser` is disposed.
    await Task.Delay(10);
}
```

## Non-blocking attempt (`TryLock`)

If you want to skip work when the lock is already held:

```csharp
using Garyon.Mechanisms;

var gate = new AsyncUsableLock();

using var releaser = gate.TryLock(out var success);
if (!success)
    return;

// Protected work...
```

## Cancellation

If you need a cancellable wait for the lock:

```csharp
using var releaser = await _gate.LockAsync(cancellationToken);
```

