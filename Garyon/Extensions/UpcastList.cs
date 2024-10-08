﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

internal sealed class UpcastList<TBase, TUp>(IList<TBase> backingList)
    : IList<TUp>, IReadOnlyList<TUp>
    where TUp : class, TBase
{
    private readonly IList<TBase> _backing = backingList;

    bool ICollection<TUp>.IsReadOnly => _backing.IsReadOnly;

    public int Count => _backing.Count;

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

    IEnumerator<TUp> IEnumerable<TUp>.GetEnumerator()
    {
        return _backing.Cast<TUp>()
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _backing.GetEnumerator();
    }

    int IList<TUp>.IndexOf(TUp item)
    {
        return _backing.IndexOf(item);
    }

    void IList<TUp>.Insert(int index, TUp item)
    {
        _backing.Insert(index, item);
    }

    bool ICollection<TUp>.Remove(TUp item)
    {
        return _backing.Remove(item);
    }

    void IList<TUp>.RemoveAt(int index)
    {
        _backing.RemoveAt(index);
    }

    public TUp this[int index] => _backing[index] as TUp;

    TUp IList<TUp>.this[int index]
    {
        get
        {
            return _backing[index] as TUp;
        }
        set
        {
            _backing[index] = value;
        }
    }
}
