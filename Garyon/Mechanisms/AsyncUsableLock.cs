using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace Garyon.Mechanisms;

/// <summary>
/// Represents an asynchronous lock that can be used in an
/// async/await context without the <see langword="lock"/> statement.
/// </summary>
public sealed class AsyncUsableLock
{
    private volatile bool _isLocked = false;

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    /// <summary>
    /// Determines whether the lock is currently locked.
    /// </summary>
    public bool IsLocked => _isLocked;

    /// <summary>
    /// Attempts to lock the current lock, and immediately returns if
    /// already held.
    /// </summary>
    /// <param name="success">
    /// Returns <see langword="true"/> if this lock was successfully locked,
    /// otherwise <see langword="false"/> if the lock was already held.
    /// </param>
    /// <returns>
    /// A <see cref="LockReleaser"/> instance unlocking this instance
    /// once the releaser is disposed, or <see langword="default"/> if the
    /// lock was already held. The <see langword="default"/> value can be
    /// simply ignored and left to be disposed for a no-op.
    /// </returns>
    public LockReleaser TryLock(out bool success)
    {
        success = _semaphore.Wait(0);

        if (!success)
            return default;
        return PerformLock();
    }

    /// <summary>
    /// Locks the current instance and returns a <see cref="LockReleaser"/>
    /// allowing the usage of the disposal pattern via <see langword="using"/>.
    /// If the lock is currently locked, this method will block until the lock
    /// is released from the current lock holder.
    /// </summary>
    /// <returns>
    /// A <see cref="LockReleaser"/> instance unlocking this instance
    /// once the releaser is disposed.
    /// </returns>
    public LockReleaser Lock()
    {
        _semaphore.Wait();
        return PerformLock();
    }

    /// <summary>
    /// Locks the current instance and returns a <see cref="LockReleaser"/>
    /// allowing the usage of the disposal pattern via <see langword="using"/>.
    /// If the lock is currently locked, this method will block until the lock
    /// is released from the current lock holder.
    /// </summary>
    /// <returns>
    /// A <see cref="LockReleaser"/> instance unlocking this instance
    /// once the releaser is disposed.
    /// </returns>
    public async Task<LockReleaser> LockAsync()
    {
        await _semaphore.WaitAsync();
        return PerformLock();
    }

    /// <summary>
    /// Locks the current instance and returns a <see cref="LockReleaser"/>
    /// allowing the usage of the disposal pattern via <see langword="using"/>.
    /// If the lock is currently locked, this method will block until the lock
    /// is released from the current lock holder.
    /// </summary>
    /// <param name="token">
    /// The <see cref="CancellationToken"/> that can cancel the wait from the lock to be released.</param>
    /// <returns>
    /// A <see cref="LockReleaser"/> instance unlocking this instance
    /// once the releaser is disposed.
    /// </returns>
    public async Task<LockReleaser> LockAsync(CancellationToken token)
    {
        await _semaphore.WaitAsync(token);
        return PerformLock();
    }

    private LockReleaser PerformLock()
    {
        Debug.Assert(!_isLocked, "expected the lock to be unset");
        _isLocked = true;
        return new(this);
    }

    /// <summary>
    /// Represents an instance that automatically releases the currently held
    /// lock from an <see cref="AsyncUsableLock"/> instance when disposed.
    /// </summary>
    /// <param name="AsyncLock">
    /// The <see cref="AsyncUsableLock"/> instance to unlock upon disposal.
    /// Allows <see langword="null"/> to enable <see langword="default"/> values
    /// to be used as no-ops.
    /// </param>
    public readonly record struct LockReleaser(AsyncUsableLock? AsyncLock)
        : IDisposable
    {
        void IDisposable.Dispose()
        {
            if (AsyncLock is null)
                return;

            AsyncLock._semaphore.Release();
            AsyncLock._isLocked = false;
        }
    }
}
