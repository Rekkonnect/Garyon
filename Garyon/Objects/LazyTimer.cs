using System;

namespace Garyon.Objects;

/// <summary>
/// Represents a timer that lazily evaluates whether the signal
/// has been raised and the denoted timeout has elapsed, without
/// raising events in real time, and only operating on demand.
/// </summary>
public class LazyTimer
{
    /// <summary>
    /// Determines when the next signal is considered raised.
    /// </summary>
    /// <remarks>
    /// It is possible to overwrite the next signal timestamp when
    /// it has timed out, without having evaluated the timeout. In
    /// other words, there is no guarantee that this property's
    /// value is the same since the last reset, and it does not rely
    /// on a timeout to allow the value change.
    /// </remarks>
    public DateTime NextSignal { get; set; }

    /// <summary>
    /// Determines whether the timer has timed out, given the current
    /// value of the <seealso cref="NextSignal"/> property, compared
    /// against <seealso cref="DateTime.Now"/>.
    /// </summary>
    public bool SignalRaised => NextSignal <= DateTime.Now;

    // Privated to prefer using the factory methods.
    private LazyTimer() { }
    /// <summary>
    /// Initializes a new <seealso cref="LazyTimer"/> out of a given
    /// <seealso cref="DateTime"/> representing the next signal time.
    /// </summary>
    /// <param name="nextSignal">
    /// The value to set to the <seealso cref="NextSignal"/> property.
    /// </param>
    public LazyTimer(DateTime nextSignal)
    {
        NextSignal = nextSignal;
    }

    /// <summary>
    /// Resets the timer only if the signal has been raised, and adjusts
    /// the next signal timestamp based on the current time and the
    /// timespan offsetting the current time.
    /// </summary>
    /// <param name="nextSignalSpan">
    /// The <seealso cref="TimeSpan"/> offsetting the current time
    /// that will be set as the timestamp for the next signal if the
    /// timer is reset.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the signal had been raised and its next
    /// signal timestamp was adjusted, otherwise <see langword="false"/>.
    /// </returns>
    public bool ResetIfRaisedSignal(TimeSpan nextSignalSpan)
    {
        bool signalRaised = SignalRaised;

        if (signalRaised)
            Reset(nextSignalSpan);

        return signalRaised;
    }

    /// <summary>
    /// Resets the timer and adjusts the next signal timestamp based
    /// on the current time and the timespan offsetting the current time.
    /// </summary>
    /// <param name="nextSignalSpan">
    /// The <seealso cref="TimeSpan"/> offsetting the current time
    /// that will be set as the timestamp for the next signal.
    /// </param>
    public void Reset(TimeSpan nextSignalSpan)
    {
        var next = DateTime.Now + nextSignalSpan;
        NextSignal = next;
    }

    /// <summary>
    /// Resets the timer and sets the next signal timestamp to
    /// <seealso cref="DateTime.MaxValue"/>, effectively making the
    /// signal unraisable for until the next signal timestamp is reset
    /// to a more approachable date.
    /// </summary>
    public void ResetUnraisable()
    {
        NextSignal = DateTime.MaxValue;
    }

    #region Factory
    /// <summary>
    /// Initializes a new <seealso cref="LazyTimer"/> with a given
    /// <seealso cref="TimeSpan"/> representing the offset from the
    /// current time, initializing the <seealso cref="NextSignal"/>
    /// time using <seealso cref="Reset(TimeSpan)"/>.
    /// </summary>
    /// <param name="nextSignalSpan">
    /// The <seealso cref="TimeSpan"/> offsetting the current time
    /// that will be set as the timestamp for the next signal.
    /// </param>
    /// <returns>
    /// A new <seealso cref="LazyTimer"/> instance with the
    /// <seealso cref="NextSignal"/> initialized to the current time
    /// offset by the given time span.
    /// </returns>
    public static LazyTimer Initialize(TimeSpan nextSignalSpan)
    {
        var timer = new LazyTimer();
        timer.Reset(nextSignalSpan);
        return timer;
    }
    /// <summary>
    /// Initializes a new <seealso cref="LazyTimer"/> such that it is
    /// initially marked as though the signal has been raised.
    /// </summary>
    /// <returns>
    /// A new <seealso cref="LazyTimer"/> instance with the
    /// <seealso cref="NextSignal"/> initialized to the current time
    /// representing a timer whose signal has been raised.
    /// </returns>
    /// <remarks>
    /// The timer's next signal can be manually adjusted at any moment;
    /// this factory method does not prevent mutating the state of the
    /// timer.
    /// </remarks>
    public static LazyTimer InitializeRaised()
    {
        return new(DateTime.Now);
    }
    /// <summary>
    /// Initializes a new <seealso cref="LazyTimer"/> such that it is
    /// initially marked as though the signal has not yet been raised.
    /// </summary>
    /// <returns>
    /// A new <seealso cref="LazyTimer"/> instance with the
    /// <seealso cref="NextSignal"/> initialized to
    /// <seealso cref="DateTime.MaxValue"/>.
    /// </returns>
    /// <remarks>
    /// The timer's next signal can be manually adjusted at any moment;
    /// this factory method does not prevent mutating the state of the
    /// timer.
    /// </remarks>
    public static LazyTimer InitializeUnraised()
    {
        return new(DateTime.MaxValue);
    }
    #endregion
}