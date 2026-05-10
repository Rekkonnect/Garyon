using Garyon.Mechanisms;
using System;
using System.Threading;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Mechanisms;

public class MechanismsTests
{
    [Test]
    public async Task AsyncUsableLockTryLockAndReleaseTest()
    {
        var asyncLock = new AsyncUsableLock();

        using var held = asyncLock.TryLock(out bool initialSuccess);
        using var notHeld = asyncLock.TryLock(out bool secondarySuccess);

        await Assert.That(initialSuccess).IsTrue();
        await Assert.That(secondarySuccess).IsFalse();
        await Assert.That(asyncLock.IsLocked).IsTrue();

        asyncLock.Unlock();

        await Assert.That(asyncLock.IsLocked).IsFalse();
    }

    [Test]
    public async Task AsyncUsableLockLockAsyncAndUnlockNoOpTest()
    {
        var asyncLock = new AsyncUsableLock();

        using var held = await asyncLock.LockAsync();
        await Assert.That(asyncLock.IsLocked).IsTrue();

        asyncLock.Unlock();
        asyncLock.Unlock();

        await Assert.That(asyncLock.IsLocked).IsFalse();
    }

    [Test]
    public async Task LazyTimerResetHelpersTest()
    {
        var timer = LazyTimer.InitializeRaised();

        await Assert.That(timer.SignalRaised).IsTrue();
        await Assert.That(timer.ResetIfRaisedSignal(TimeSpan.FromMinutes(1))).IsTrue();
        await Assert.That(timer.SignalRaised).IsFalse();

        timer.ResetUnraisable();

        await Assert.That(timer.SignalRaised).IsFalse();
    }

    [Test]
    public async Task LazyTimerInitializeUnraisedAndResetTest()
    {
        var timer = LazyTimer.InitializeUnraised();

        await Assert.That(timer.SignalRaised).IsFalse();

        timer.Reset(TimeSpan.Zero);

        await Assert.That(timer.SignalRaised).IsTrue();
    }

    [Test]
    public async Task ActionTimerRequestRegistersExecutionTimeTest()
    {
        var timer = new ActionTimer(TimeSpan.Zero);

        await Assert.That(timer.Request(true)).IsTrue();
        await Assert.That(timer.LastExecutionTime > DateTime.MinValue).IsTrue();
    }

    [Test]
    public async Task ActionTimerRejectsImmediateRequestWithoutRegisteringTest()
    {
        var timer = new ActionTimer(TimeSpan.FromDays(1));

        await Assert.That(timer.Request(true)).IsTrue();
        var lastExecution = timer.LastExecutionTime;
        await Assert.That(timer.Request(true)).IsFalse();
        await Assert.That(timer.LastExecutionTime).IsEqualTo(lastExecution);
    }

    [Test]
    public async Task DelayerCancelAndImmediateWaitTest()
    {
        var delayer = new Delayer();

        delayer.SetFutureUnblock(TimeSpan.FromMinutes(1));
        await Assert.That(delayer.IsWaiting).IsFalse();

        delayer.CancelUnblock();
        await delayer.WaitUnblock(CancellationToken.None);

        await Assert.That(delayer.IsWaiting).IsFalse();
    }

    [Test]
    public async Task SimpleProfilerRunCapturesResultsTest()
    {
        var profiler = new SimpleProfiler();

        using (profiler.Run())
        {
            _ = new byte[1024];
        }

        await Assert.That(profiler.SnapshotResults).IsNotNull();
        await Assert.That(profiler.SnapshotResults!.Time >= TimeSpan.Zero).IsTrue();
    }

    [Test]
    public async Task SimpleProfilerInitializeAndEndTest()
    {
        var profiler = new SimpleProfiler();

        profiler.Initialize();
        profiler.End();

        await Assert.That(profiler.SnapshotResults).IsNotNull();
        await Assert.That(profiler.SnapshotResults!.Memory).IsGreaterThanOrEqualTo(long.MinValue);
    }
}
