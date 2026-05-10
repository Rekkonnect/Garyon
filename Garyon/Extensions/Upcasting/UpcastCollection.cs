using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions.Upcasting;

internal sealed class UpcastCollection<TBase, TUp>(ICollection<TBase> backingCollection)
    : ICollection<TUp>, IReadOnlyCollection<TUp>
    where TUp : class, TBase
{
    private readonly ICollection<TBase> _backing = backingCollection;

    public int Count => _backing.Count;

    bool ICollection<TUp>.IsReadOnly => _backing.IsReadOnly;

    void ICollection<TUp>.Add(TUp item)
    {
        _backing.Add(item);
    }

    void ICollection<TUp>.Clear()
    {
        _backing.Clear();
    }

    bool ICollection<TUp>.Contains(TUp item)
    {
        return _backing.Contains(item);
    }

    void ICollection<TUp>.CopyTo(TUp[] array, int arrayIndex)
    {
        _backing.CopyTo(array, arrayIndex);
    }

    public IEnumerator<TUp> GetEnumerator()
    {
        return _backing.Cast<TUp>()
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    bool ICollection<TUp>.Remove(TUp item)
    {
        return _backing.Remove(item);
    }
}

