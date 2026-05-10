using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions.Upcasting;

internal sealed class UpcastSet<TBase, TUp>(ISet<TBase> backingSet)
    : ISet<TUp>, IReadOnlyCollection<TUp>
    where TUp : class, TBase
{
    private readonly ISet<TBase> _backing = backingSet;

    public int Count => _backing.Count;

    bool ICollection<TUp>.IsReadOnly => _backing.IsReadOnly;

    public bool Add(TUp item)
    {
        return _backing.Add(item);
    }

    void ICollection<TUp>.Add(TUp item)
    {
        _ = Add(item);
    }

    public void Clear()
    {
        _backing.Clear();
    }

    public bool Contains(TUp item)
    {
        return _backing.Contains(item);
    }

    public void CopyTo(TUp[] array, int arrayIndex)
    {
        _backing.CopyTo(array, arrayIndex);
    }

    public void ExceptWith(IEnumerable<TUp> other)
    {
        _backing.ExceptWith(other);
    }

    public IEnumerator<TUp> GetEnumerator()
    {
        return _backing.Cast<TUp>()
            .GetEnumerator();
    }

    public void IntersectWith(IEnumerable<TUp> other)
    {
        _backing.IntersectWith(other);
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

    public bool Remove(TUp item)
    {
        return _backing.Remove(item);
    }

    public bool SetEquals(IEnumerable<TUp> other)
    {
        return _backing.SetEquals(other);
    }

    public void SymmetricExceptWith(IEnumerable<TUp> other)
    {
        _backing.SymmetricExceptWith(other);
    }

    public void UnionWith(IEnumerable<TUp> other)
    {
        _backing.UnionWith(other);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

