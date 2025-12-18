using System;

namespace Garyon.Mechanisms;

/// <summary>
/// A simple profiler that tracks time and memory without
/// sophisticated mechanisms and edge cases.
/// </summary>
/// <remarks>
/// Can be useful for basic info that can be logged for
/// debug purposes.
/// </remarks>
public sealed class SimpleProfiler
{
    private Snapshot _start;
    private Snapshot _end;

    public Results? SnapshotResults { get; private set; }

    public void Initialize()
    {
        _start = TakeSnapshot();
    }

    public void End()
    {
        _end = TakeSnapshot();
        SetResult();
    }

    /// <summary>
    /// Begins the profiling process and returns an instance
    /// tracking the process that implements
    /// <see cref="IDisposable"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ProfilingProcess"/> involving this simple
    /// profiler, which can be used in a <see langword="using"/>
    /// block.
    /// </returns>
    public ProfilingProcess Run()
    {
        return new(this);
    }

    private void SetResult()
    {
        SnapshotResults = new()
        {
            Time = _end.Time - _start.Time,
            Memory = _end.MemoryBytes - _start.MemoryBytes,
        };
    }

    private static Snapshot TakeSnapshot()
    {
        return new()
        {
            Time = DateTime.Now,
            MemoryBytes = GC.GetTotalMemory(false),
        };
    }

    public readonly struct ProfilingProcess : IDisposable
    {
        private readonly SimpleProfiler _profiling;

        public ProfilingProcess(SimpleProfiler profiling)
        {
            _profiling = profiling;
            profiling.Initialize();
        }

        public void Dispose()
        {
            _profiling.End();
        }
    }

    private readonly record struct Snapshot(
        DateTime Time, long MemoryBytes);

    public sealed class Results
    {
        public required TimeSpan Time { get; init; }
        public required long Memory { get; init; }
    }
}
