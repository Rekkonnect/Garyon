#if HAS_READONLY_SET
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions.Upcasting;

internal sealed class UpcastReadOnlySet<TBase, TUp>(IReadOnlySet<TBase> backingSet)
    : IReadOnlySet<TUp>
    where TUp : class, TBase
{
    private readonly IReadOnlySet<TBase> _backing = backingSet;

    public int Count => _backing.Count;

    public bool Contains(TUp item)
    {
        return _backing.Contains(item);
    }

    public IEnumerator<TUp> GetEnumerator()
    {
        return _backing.Cast<TUp>()
            .GetEnumerator();
    }

    public bool IsProperSubsetOf(IEnumerable<TUp> other)
    {
        return _backing.IsProperSubsetOf(other);
    }

    public bool IsProperSupersetOf(IEnumerable<TUp> other)
    {
        return _backing.IsProperSupersetOf(other);
    }

    public bool IsSubsetOf(IEnumerable<TUp> other)
    {
        return _backing.IsSubsetOf(other);
    }

    public bool IsSupersetOf(IEnumerable<TUp> other)
    {
        return _backing.IsSupersetOf(other);
    }

    public bool Overlaps(IEnumerable<TUp> other)
    {
        return _backing.Overlaps(other);
    }

    public bool SetEquals(IEnumerable<TUp> other)
    {
        return _backing.SetEquals(other);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
#endif

