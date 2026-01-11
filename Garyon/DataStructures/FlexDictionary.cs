using System.Collections.Generic;

namespace Garyon.DataStructures;

/// <summary>A dictionary that allows indexing or setting values on non-existing keys without throwing exceptions.</summary>
/// <typeparam name="TKey">The type of the keys that are being added to the dictionary.</typeparam>
/// <typeparam name="TValue">The type of the values that are being paired with the keys in the dictionary.</typeparam>
public class FlexDictionary<TKey, TValue>
    : BaseFlexDictionary<TKey, TValue>
    where TKey : notnull
{
    private readonly TValue _defaultValue;
    
    /// <summary>Initializes a new instance of the <seealso cref="FlexDictionary{TKey, TValue}"/> class with the default initial capacity (16).</summary>
    public FlexDictionary(TValue defaultValue)
        : this(16, defaultValue) { }
    /// <summary>Initializes a new instance of the <seealso cref="FlexDictionary{TKey, TValue}"/> class.</summary>
    /// <param name="capacity">The capacity of the dictionary.</param>
    public FlexDictionary(int capacity, TValue defaultValue)
        : base(capacity)
    {
        _defaultValue = defaultValue;
    }
    /// <summary>Initializes a new instance of the <seealso cref="FlexDictionary{TKey, TValue}"/> class. Each item in the provided collection is added as a key and is mapped to the specified value of the <typeparamref name="TValue"/> type.</summary>
    /// <param name="collection">The collection to initialize the dictionary from.</param>
    /// <param name="initialValue">The initial value to map each key to.</param>
    public FlexDictionary(IEnumerable<TKey> collection, TValue initialValue)
        : base(collection, initialValue)
    {
        _defaultValue = initialValue;
    }

#if HAS_DICTIONARY_KVPS_CTOR
    /// <summary>Initializes a new instance of the <seealso cref="FlexDictionary{TKey, TValue}"/> class.</summary>
    /// <param name="kvps">The collection of <seealso cref="KeyValuePair{TKey, TValue}"/> objects to initialize the dictionary from.</param>
    public FlexDictionary(IEnumerable<KeyValuePair<TKey, TValue>> kvps, TValue defaultValue)
        : base(kvps)
    {
        _defaultValue = defaultValue;
    }
#endif

    /// <summary>Initializes a new instance of the <seealso cref="FlexDictionary{TKey, TValue}"/> class out of another <seealso cref="FlexDictionary{TKey, TValue}"/> instance.</summary>
    /// <param name="other">The other <seealso cref="FlexDictionary{TKey, TValue}"/> whose key-value pairs to copy.</param>
    public FlexDictionary(FlexDictionary<TKey, TValue> other)
        : base(other)
    {
        _defaultValue = other._defaultValue;
    }

    /// <summary>Clones this <seealso cref="FlexDictionary{TKey, TValue}"/> and adds all its keys to the resulting instance.</summary>
    /// <returns>The cloned instance containing the same key-value pairs.</returns>
    public override
#if SUPPORTS_COVARIANT_OVERRIDES
        FlexDictionary<TKey, TValue>
#else
        BaseFlexDictionary<TKey, TValue>
#endif
        Clone() => new FlexDictionary<TKey, TValue>(this);

    protected override TValue GetNewEntryInitializationValue() => _defaultValue;
}
