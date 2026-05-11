# SimpleProfiler

<xref:Garyon.Mechanisms.SimpleProfiler> is a small utility for capturing coarse performance signals:

- Wall-clock duration (based on `DateTime.Now`)
- Managed memory delta (based on `GC.GetTotalMemory(false)`)

It is intended for *quick, low-friction measurements* (debug logging, ad-hoc comparisons), not for high-precision benchmarking.

## Quick usage (`using`)

The easiest pattern is to run the profiler as a disposable "scope":

```csharp
using Garyon.Mechanisms;

var profiler = new SimpleProfiler();

using (profiler.Run())
{
    DoWork();
}

var results = profiler.SnapshotResults!;
Console.WriteLine($"Time: {results.Time}, Memory: {results.Memory} bytes");
```

## Manual usage

If you prefer explicit control:

```csharp
using Garyon.Mechanisms;

var profiler = new SimpleProfiler();
profiler.Initialize();

DoWork();

profiler.End();
var results = profiler.SnapshotResults!;
```

## Notes and caveats

- `GC.GetTotalMemory(false)` reports an estimate and can fluctuate due to GC timing and allocations performed by unrelated code.
- `DateTime.Now` is convenient and widely available, but it is not a high-precision timer. For micro-benchmarks, prefer dedicated benchmarking tools.

