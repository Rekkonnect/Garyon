using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions.Upcasting;

internal sealed class UpcastDictionary<TKey, TBase, TUp>(IDictionary<TKey, TBase> backingDictionary)
    : IDictionary<TKey, TUp>, IReadOnlyDictionary<TKey, TUp>
    where TKey : notnull
    where TUp : class, TBase
{
    private readonly IDictionary<TKey, TBase> _backing = backingDictionary;

    public ICollection<TKey> Keys => _backing.Keys;

    public ICollection<TUp> Values => new UpcastCollection<TBase, TUp>(_backing.Values);

    IEnumerable<TKey> IReadOnlyDictionary<TKey, TUp>.Keys => _backing.Keys;

    IEnumerable<TUp> IReadOnlyDictionary<TKey, TUp>.Values => _backing.Values.Cast<TUp>();

    public int Count => _backing.Count;

    public bool IsReadOnly => _backing.IsReadOnly;

    public TUp this[TKey key]
    {
        get => (TUp)_backing[key]!;
        set => _backing[key] = value;
    }

    public void Add(TKey key, TUp value)
    {
        _backing.Add(key, value);
    }

    public bool ContainsKey(TKey key)
    {
        return _backing.ContainsKey(key);
    }

    public bool Remove(TKey key)
    {
        return _backing.Remove(key);
    }

    public bool TryGetValue(TKey key, out TUp value)
    {
        if (_backing.TryGetValue(key, out var baseValue))
        {
            value = (TUp)baseValue!;
            return true;
        }

        value = null!;
        return false;
    }

    public void Add(KeyValuePair<TKey, TUp> item)
    {
        _backing.Add(new KeyValuePair<TKey, TBase>(item.Key, item.Value));
    }

    public void Clear()
    {
        _backing.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TUp> item)
    {
        return _backing.TryGetValue(item.Key, out var value)
            && EqualityComparer<TBase>.Default.Equals(value, item.Value);
    }

    public void CopyTo(KeyValuePair<TKey, TUp>[] array, int arrayIndex)
    {
        foreach (var kvp in _backing)
        {
            array[arrayIndex++] = new KeyValuePair<TKey, TUp>(kvp.Key, (TUp)kvp.Value!);
        }
    }

    public bool Remove(KeyValuePair<TKey, TUp> item)
    {
        if (_backing is ICollection<KeyValuePair<TKey, TBase>> kvpCollection)
        {
            return kvpCollection.Remove(new KeyValuePair<TKey, TBase>(item.Key, item.Value));
        }

        if (!Contains(item))
        {
            return false;
        }

        return _backing.Remove(item.Key);
    }

    public IEnumerator<KeyValuePair<TKey, TUp>> GetEnumerator()
    {
        return _backing.Select(static kvp => new KeyValuePair<TKey, TUp>(kvp.Key, (TUp)kvp.Value!))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

