using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>
/// An <see cref="IEnumerable"/> wrapper that provides the ability to cache the
/// count of the underlying enumerable, and also provide the real-time lower
/// bound of the count while enumerating.
/// </summary>
public class CachedCountEnumerable : IEnumerable
{
    private int? _cachedCount;
    private int _minCount;
    private readonly IEnumerable _enumerable;

    public CachedCountEnumerable(IEnumerable enumerable)
    {
        _enumerable = enumerable;
        _cachedCount = enumerable.GetNonEnumeratedCount();
        _minCount = _cachedCount ?? 0;
    }

    /// <summary>
    /// The cached count of the enumerable.
    /// </summary>
    public int? CachedCount => _cachedCount;

    /// <summary>
    /// The lower bound of the count of the enumerable.
    /// </summary>
    public int MinCount => _minCount;

    /// <summary>
    /// Gets the count of the enumerable, forcing its complete enumeration if
    /// the count is not yet known.
    /// </summary>
    public int ForceCount()
    {
        return CachedCount ?? ForceCountCore();
    }

    private int ForceCountCore()
    {
        return this.Count();
    }

    private void IncreaseMinCount(int count)
    {
        _minCount.AssignMax(count);
    }

    private void CommitCount(int count)
    {
        _cachedCount ??= count;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new Enumerator(this);
    }

    private protected sealed class EnumeratorCounter
    {
        private int _count;

        public bool TrackMoveNext(bool hasNext, CachedCountEnumerable enumerable)
        {
            if (hasNext)
            {
                _count++;
            }

            enumerable.IncreaseMinCount(_count);

            if (!hasNext)
            {
                enumerable.CommitCount(_count);
            }

            return hasNext;
        }

        public void Reset()
        {
            _count = 0;
        }
    }

    private sealed class Enumerator(CachedCountEnumerable enumerable)
        : IEnumerator
    {
        private readonly EnumeratorCounter _enumeratorCounter = new();
        private readonly IEnumerator _underlying = enumerable._enumerable.GetEnumerator();

        public object? Current => _underlying.Current;

        public bool MoveNext()
        {
            var hasNext = _underlying.MoveNext();
            return _enumeratorCounter.TrackMoveNext(hasNext, enumerable);
        }

        public void Reset()
        {
            _enumeratorCounter.Reset();
            _underlying.Reset();
        }
    }
}

/// <summary>
/// An <see cref="IEnumerable{T}"/> wrapper that provides the ability to cache
/// the count of the underlying enumerable, and also provide the real-time lower
/// bound of the count while enumerating.
/// </summary>
public class CachedCountEnumerable<T>(IEnumerable<T> enumerable)
    : CachedCountEnumerable(enumerable), IEnumerable, IEnumerable<T>
#if ALLOWS_REF_STRUCTS
    where T : allows ref struct
#endif
{
    private readonly IEnumerable<T> _enumerable = enumerable;

    private IEnumerator<T> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private sealed class Enumerator(CachedCountEnumerable<T> enumerable)
        : IEnumerator<T>
    {
        private readonly EnumeratorCounter _enumeratorCounter = new();
        private readonly IEnumerator<T> _underlying = enumerable._enumerable.GetEnumerator();

        object? IEnumerator.Current => ((IEnumerator)_underlying).Current;
        public T Current => _underlying.Current;

        public bool MoveNext()
        {
            var hasNext = _underlying.MoveNext();
            return _enumeratorCounter.TrackMoveNext(hasNext, enumerable);
        }

        public void Reset()
        {
            _enumeratorCounter.Reset();
            _underlying.Reset();
        }

        void IDisposable.Dispose() { }
    }
}
