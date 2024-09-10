using System;
using System.Threading.Tasks;
using System.Threading;

namespace Garyon.Mechanisms;

/// <summary>
/// Represents a delayer that keeps track of the next time moment that
/// it can be unblocked, allowing awaiting the next unblock.
/// </summary>
public sealed class Delayer
{
    private DateTime _nextUnblock = DateTime.MinValue;
    private Task? _delayTask;

    /// <summary>
    /// Denotes whether the delayer has registered a task for awaiting
    /// the next unblock.
    /// </summary>
    public bool IsWaiting => _delayTask is not null;

    /// <summary>
    /// Sets the next unblock time to <see cref="DateTime.Now"/> plus
    /// the specified <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="span">
    /// The timespan representing the offset from the current time.
    /// </param>
    /// <remarks>
    /// If the next unblock is already set to a later time, this method
    /// does not overwrite the next unblock time.
    /// </remarks>
    public void SetFutureUnblock(TimeSpan span)
    {
        var next = DateTime.Now + span;
        ExtendNextUnblock(next);
    }

    /// <summary>
    /// Sets the next unblock to the specified <see cref="DateTime"/>.
    /// </summary>
    /// <param name="next">
    /// The time to set the next unblock time to.
    /// </param>
    /// <remarks>
    /// If the next unblock is already set to a later time, this method
    /// does not overwrite the next unblock time.
    /// </remarks>
    public void ExtendNextUnblock(DateTime next)
    {
        if (next <= _nextUnblock)
        {
            return;
        }

        _nextUnblock = next;
    }

    /// <summary>
    /// Cancels the next unblock time, effectively unblocking the delayer
    /// and allowing any unblock time to be set.
    /// </summary>
    /// <remarks>
    /// If an active await task is present, it will be ignored but not
    /// cancelled. The task returned from <see cref="WaitUnblock(CancellationToken)"/>
    /// must have its cancellation token signal the cancellation manually
    /// in order to avoid waiting for the original unblock time.
    /// </remarks>
    public void CancelUnblock()
    {
        _nextUnblock = DateTime.MinValue;
        _delayTask = null;
    }

    /// <summary>
    /// Waits until the unblock time is reached. This allows consecutive unblock
    /// time delays until the instance is considered unblocked, via methods like
    /// <see cref="ExtendNextUnblock(DateTime)"/>.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> allowing to cancel the awaiting task.
    /// </param>
    /// <returns>
    /// A task representing the awaiting of the next unblock time.
    /// </returns>
    /// <remarks>
    /// The <paramref name="cancellationToken"/> must be manually cancelled
    /// if <see cref="CancelUnblock"/> is called while awaiting the task.
    /// </remarks>
    public async Task WaitUnblock(CancellationToken cancellationToken)
    {
        // locking this is not crucial; it's probably not too bad spawning a
        // second task to track the time until the next unblock, compared to
        // the cost of entering the lock
        if (_delayTask is null)
        {
            _delayTask = MainWaitUnblock(cancellationToken);
        }

        await _delayTask;
        _delayTask = null;
    }

    private async Task MainWaitUnblock(CancellationToken cancellationToken)
    {
        while (true)
        {
            var remainder = _nextUnblock - DateTime.Now;
            if (remainder <= TimeSpan.Zero)
            {
                return;
            }

            await Task.Delay(remainder, cancellationToken);
            // we don't want to throw exceptions here
            if (cancellationToken.IsCancellationRequested)
                return;
        }
    }
}
