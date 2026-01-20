using Garyon.Extensions;
using Garyon.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Garyon.DataStructures;

/// <summary>
/// A dictionary that allows indexing or setting values on non-existing keys
/// without throwing exceptions.
/// </summary>
/// <typeparam name="TKey">
/// The type of the keys that are being added to the dictionary.
/// </typeparam>
/// <typeparam name="TValue">
/// The type of the values that are being paired with the keys in the
/// dictionary.
/// </typeparam>
public abstract class BaseFlexDictionary<TKey, TValue>
    : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    where TKey : notnull
{
    /// <summary>
    /// The internal <seealso cref="Dictionary{TKey, TValue}"/> instance.
    /// </summary>
    protected readonly Dictionary<TKey, TValue> Dictionary;

    #region Hidden Interface Properties
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;
    #endregion

    /// <inheritdoc/>
    public int Count => Dictionary.Count;

    /// <inheritdoc/>
    public ICollection<TKey> Keys => Dictionary.Keys;
    /// <inheritdoc/>
    public ICollection<TValue> Values => Dictionary.Values;

    public BaseFlexDictionary(int capacity)
    {
        Dictionary = new Dictionary<TKey, TValue>(capacity);
    }

    public BaseFlexDictionary(IEnumerable<TKey> collection, TValue initialValue)
        : this(collection.GetNonEnumeratedCountOrDefault())
    {
        foreach (var v in collection)
            Add(v, initialValue);
    }

    public BaseFlexDictionary(IEnumerable<KeyValuePair<TKey, TValue>> kvps)
    {
#if HAS_DICTIONARY_KVPS_CTOR
        Dictionary = new(kvps);
#else
        Dictionary = new(kvps.GetNonEnumeratedCountOrDefault());
        foreach (var (key, value) in kvps)
            Add(key, value);
#endif
    }

    public BaseFlexDictionary(BaseFlexDictionary<TKey, TValue> other)
    {
        Dictionary = new Dictionary<TKey, TValue>(other.Dictionary);
    }

    /// <inheritdoc/>
    public virtual void Add(TKey key, TValue value) => Dictionary.TryAdd(key, value);
    /// <inheritdoc/>
    public virtual void Add(KeyValuePair<TKey, TValue> kvp) => Dictionary.Add(kvp.Key, kvp.Value);

    /// <inheritdoc/>
    public virtual bool Remove(TKey key) => Dictionary.Remove(key);
    /// <summary>
    /// Removes a collection of keys from this
    /// <seealso cref="FlexDictionary{TKey, TValue}"/>. Keys that are not
    /// present will be simply ignored.
    /// </summary>
    /// <param name="keys">
    /// The keys to remove. Must not be <see langword="null"/>.
    /// </param>
    /// <returns>
    /// The number of keys that were removed.
    /// </returns>
    public int RemoveKeys(params TKey[] keys) => RemoveKeys((IEnumerable<TKey>)keys);
    /// <summary>
    /// Removes a collection of keys from this
    /// <seealso cref="FlexDictionary{TKey, TValue}"/>. Keys that are not
    /// present will be simply ignored.
    /// </summary>
    /// <param name="keys">
    /// The keys to remove. Must not be <see langword="null"/>.
    /// </param>
    /// <returns>
    /// The number of keys that were removed.
    /// </returns>
    public int RemoveKeys(IEnumerable<TKey> keys)
    {
        int count = 0;
        foreach (var k in keys)
            if (Dictionary.Remove(k))
                count++;
        return count;
    }

    /// <inheritdoc/>
    public void Clear() => Dictionary.Clear();

    /// <summary>
    /// Clones this <seealso cref="FlexDictionary{TKey, TValue}"/> and adds all
    /// its keys to the resulting instance.
    /// </summary>
    /// <returns>
    /// The cloned instance containing the same key-value pairs.
    /// </returns>
    public abstract BaseFlexDictionary<TKey, TValue> Clone();

    /// <inheritdoc/>
    public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

    // Suppressed because of netstandard2.1 because it doesn't correctly annotate 'value'
    // despite the availability of the attribute.
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    /// <inheritdoc/>
    public bool TryGetValue(
        TKey key,
        [MaybeNullWhen(false)]
        out TValue value
        )
    {
        return Dictionary.TryGetValue(key, out value);
    }
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Dictionary.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Dictionary.GetEnumerator();

    #region Hidden Interface Methods
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => (Dictionary as IDictionary<TKey, TValue>).Contains(item);
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => (Dictionary as IDictionary<TKey, TValue>).CopyTo(array, arrayIndex);
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => (Dictionary as IDictionary<TKey, TValue>).Remove(item);
    #endregion

    /// <summary>
    /// Gets the value that will be stored by default upon creating a new entry
    /// through the <see langword="this"/> accessor. This does not affect the
    /// <see cref="Add(TKey, TValue)"/> function.
    /// </summary>
    /// <returns>
    /// The new value that will be stored in the new entry.
    /// </returns>
    protected abstract TValue GetNewEntryInitializationValue();

    protected void AddWithInitializationValue(TKey key)
    {
        Dictionary.Add(key, GetNewEntryInitializationValue());
    }

    /// <summary>
    /// Gets or sets the value of the specified key. If the key does not exist,
    /// it will be added to the dictionary.
    /// </summary>
    /// <param name="key">
    /// The key whose value to get or set.
    /// </param>
    /// <returns>
    /// The value of the key. When getting a non-existent key, it will have
    /// <typeparamref name="TValue"/>'s <see langword="default"/> value.
    /// </returns>
    public virtual TValue this[TKey key]
    {
        get
        {
            if (!ContainsKey(key))
            {
                AddWithInitializationValue(key);
            }

            return Dictionary[key];
        }
        set
        {
            Dictionary[key] = value;
        }
    }
}
