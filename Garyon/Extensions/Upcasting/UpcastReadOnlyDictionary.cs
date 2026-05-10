using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions.Upcasting;

internal sealed class UpcastReadOnlyDictionary<TKey, TBase, TUp>(IReadOnlyDictionary<TKey, TBase> backingDictionary)
    : IReadOnlyDictionary<TKey, TUp>
    where TKey : notnull
    where TUp : class, TBase
{
    private readonly IReadOnlyDictionary<TKey, TBase> _backing = backingDictionary;

    public IEnumerable<TKey> Keys => _backing.Keys;

    public IEnumerable<TUp> Values => _backing.Values.Cast<TUp>();

    public int Count => _backing.Count;

    public TUp this[TKey key] => (TUp)_backing[key]!;

    public bool ContainsKey(TKey key)
    {
        return _backing.ContainsKey(key);
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

