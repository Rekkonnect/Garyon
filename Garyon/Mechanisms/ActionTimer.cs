using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace Garyon.Mechanisms;

/// <summary>
/// Provides the ability to determine if a minimum timespan has elapsed since the
/// last successful registration of the time an action was executed.
/// </summary>
/// <param name="minOffset">
/// The minimum required elapsed time before successfully registering the next
/// execution time.
/// </param>
/// <remarks>
/// This does not hold any information about the action itself, nor is it required
/// to be only used for one single action.
/// </remarks>
public sealed class ActionTimer(TimeSpan minOffset)
{
    private DateTime _last;

    /// <summary>
    /// The last execution time that was registered.
    /// </summary>
    public DateTime LastExecutionTime => _last;

    /// <summary>
    /// The minimum timespan that must be elapsed before registering the
    /// next valid execution time.
    /// </summary>
    public TimeSpan MinOffset = minOffset;

    /// <summary>
    /// Requests the execution of an action based on the elapsed time.
    /// Optionally registers the current time as the last execution time.
    /// </summary>
    /// <param name="register">
    /// If <see langword="true"/>, the current time will be registered as
    /// the last execution time, only if this method also returns <see langword="true"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the elapsed time since the last execution
    /// is greater than the minimum required offset, otherwise <see langword="false"/>.
    /// </returns>
    public bool Request(bool register)
    {
        var now = DateTime.Now;
        var offset = now - _last;
        if (offset < MinOffset)
            return false;

        if (register)
        {
            _last = now;
        }
        return true;
    }
}
