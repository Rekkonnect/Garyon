﻿using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Garyon.DataStructures;

/// <summary>Represents a value counter dictionary, that stores a counter per entry as the value of the key.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
public class ValueCounterDictionary<TKey> : FlexibleDictionary<TKey, int>
{
    /// <summary>Initializes a new instance of the <seealso cref="ValueCounterDictionary{TKey}"/> class with the default initial capacity (16).</summary>
    public ValueCounterDictionary()
        : base() { }
    /// <summary>Initializes a new instance of the <seealso cref="ValueCounterDictionary{TKey}"/> class.</summary>
    /// <param name="capacity">The capacity of the dictionary.</param>
    public ValueCounterDictionary(int capacity)
        : base(capacity) { }
    /// <summary>Initializes a new instance of the <seealso cref="ValueCounterDictionary{TKey}"/> class. Each item in the provided collection is added as a key and is mapped to the value of 1.</summary>
    /// <param name="collection">The collection to initialize the dictionary from.</param>
    public ValueCounterDictionary(IEnumerable<TKey> collection)
        : base(collection, 1) { }
    /// <summary>Initializes a new instance of the <seealso cref="ValueCounterDictionary{TKey}"/> class. Each item in the provided collection is added as a key and is mapped to the specified value.</summary>
    /// <param name="collection">The collection to initialize the dictionary from.</param>
    /// <param name="initialValue">The initial value to map each key to.</param>
    public ValueCounterDictionary(IEnumerable<TKey> collection, int initialValue)
        : base(collection, initialValue) { }
#if HAS_DICTIONARY_KVPS_CTOR
    /// <summary>Initializes a new instance of the <seealso cref="ValueCounterDictionary{TKey}"/> class.</summary>
    /// <param name="kvps">The collection of value counters, represented as <seealso cref="KeyValuePair{TKey, TValue}"/> objects, to initialize the dictionary from.</param>
    public ValueCounterDictionary(IEnumerable<KeyValuePair<TKey, int>> kvps)
        : base(kvps) { }
#endif
    /// <summary>Initializes a new instance of the <seealso cref="ValueCounterDictionary{TKey}"/> class out of another <seealso cref="ValueCounterDictionary{TKey}"/> instance.</summary>
    /// <param name="other">The other <seealso cref="ValueCounterDictionary{TKey}"/> whose key-value pairs to copy.</param>
    public ValueCounterDictionary(ValueCounterDictionary<TKey> other)
        : base(other) { }

    /// <summary>Adds a value to a key's counter.</summary>
    /// <param name="key">The key whose counter to add a value to.</param>
    /// <param name="value">The value to add to the key's counter.</param>
    public override void Add(TKey key, int value = 1) => this[key] += value;
    /// <summary>Subtracts a value from a key's counter.</summary>
    /// <param name="key">The key whose counter to subtract a value from.</param>
    /// <param name="value">The value to subtract from the key's counter.</param>
    public void Subtract(TKey key, int value = 1) => this[key] -= value;
    /// <summary>Subtracts a value from the counter of one key and adds that value on the counter of another key.</summary>
    /// <param name="from">The key whose counter to subtract the value from.</param>
    /// <param name="to">The key whose counter to add the value to.</param>
    /// <param name="adjustment">The value to adjust the counters by.</param>
    public void AdjustCounters(TKey from, TKey to, int adjustment = 1)
    {
        Subtract(from, adjustment);
        Add(to, adjustment);
    }
}
