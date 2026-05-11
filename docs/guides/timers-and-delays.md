# Timers and Delays

Garyon includes a few lightweight *time-based primitives* under the `Garyon.Mechanisms` namespace. They are designed to be small building blocks for polling loops, throttling, debouncing, and "wait until" workflows.

> [!NOTE]
> These types generally do **not** schedule callbacks or raise events. They are meant to be queried (and updated) on demand.

## ActionTimer (throttle gating)

<xref:Garyon.Mechanisms.ActionTimer> is a minimal "am I allowed to run now?" gate:

- It tracks the last *registered* execution time.
- `Request(register: true)` returns `true` when at least `MinOffset` has elapsed since the last registration, and updates the registration time.

```csharp
using Garyon.Mechanisms;

var timer = new ActionTimer(TimeSpan.FromSeconds(1));

while (true)
{
    if (timer.Request(register: true))
    {
        // Runs at most once per second.
        Console.WriteLine("Tick");
    }
}
```

> [!TIP]
> The very first `Request(true)` will typically succeed because the initial `LastExecutionTime` has not been set yet.

## LazyTimer (lazy timeouts)

<xref:Garyon.Mechanisms.LazyTimer> represents a timestamp (`NextSignal`) and exposes a lazily evaluated boolean:

- `SignalRaised` becomes `true` when `NextSignal <= DateTime.Now`.
- You control when the timer is "checked" by reading `SignalRaised`.
- You control when the next timeout happens by setting `NextSignal` (or calling `Reset(...)`).

A common pattern is "run when raised, then reschedule":

```csharp
using Garyon.Mechanisms;

var timer = LazyTimer.InitializeRaised();

if (timer.ResetIfRaisedSignal(TimeSpan.FromMinutes(5)))
{
    // Do periodic work every ~5 minutes, on demand.
}
```

## Delayer (awaiting an extendable unblock time)

<xref:Garyon.Mechanisms.Delayer> tracks a "next unblock" time and lets you await it via `WaitUnblock(...)`.

What makes it different from a plain `Task.Delay(...)` is that you can *extend* the unblock time while a wait is already in progress:

```csharp
using Garyon.Mechanisms;

var delayer = new Delayer();

// Somewhere: keep pushing the unblock further into the future.
delayer.SetFutureUnblock(TimeSpan.FromSeconds(2));

// Somewhere else: await until the (possibly-extended) unblock time is reached.
await delayer.WaitUnblock(CancellationToken.None);
```

> [!NOTE]
> `CancelUnblock()` resets the internal unblock time, but does **not** cancel an already returned `WaitUnblock(...)` task; use a `CancellationToken` if you need to stop awaiting early.

