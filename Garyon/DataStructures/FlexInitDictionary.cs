using Garyon.Extensions;
using Garyon.Objects;
using System.Collections.Generic;

namespace Garyon.DataStructures;

/// <summary>
/// Represents a flexible dictionary where the newly added keys will map to a
/// new initialized value of <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TKey">
/// The type of the key to index the dictionary contents by.
/// </typeparam>
/// <typeparam name="TValue">
/// The type of the initializable values that are being paired with the keys in
/// the dictionary.
/// </typeparam>
public class FlexInitDictionary<TKey, TValue>
    : BaseFlexDictionary<TKey, TValue>
    where TKey : notnull
    where TValue : new()
{
    /// <summary>
    /// Initializes a new instance of the
    /// <seealso cref="FlexInitDictionary{TKey, TValue}"/> class with the
    /// default initial capacity (16).
    /// </summary>
    public FlexInitDictionary()
        : base(16) { }
    /// <summary>
    /// Initializes a new instance of the
    /// <seealso cref="FlexInitDictionary{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="capacity">
    /// The capacity of the dictionary.
    /// </param>
    public FlexInitDictionary(int capacity)
        : base(capacity) { }
    /// <summary>
    /// Initializes a new instance of the
    /// <seealso cref="FlexInitDictionary{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="collection">
    /// The collection to initialize the dictionary from. Each item in the
    /// provided collection is added as a key and is mapped to the default value
    /// of the <typeparamref name="TValue"/> type.
    /// </param>
    public FlexInitDictionary(IEnumerable<TKey> collection)
        : base(collection.GetNonEnumeratedCountOrDefault())
    {
        foreach (var key in collection)
            AddWithInitializationValue(key);
    }

#if HAS_DICTIONARY_KVPS_CTOR
    /// <summary>Initializes a new instance of the <seealso cref="FlexInitDictionary{TKey, TValue}"/> class.</summary>
    /// <param name="kvps">The collection of <seealso cref="KeyValuePair{TKey, TValue}"/> objects to initialize the dictionary from.</param>
    public FlexInitDictionary(IEnumerable<KeyValuePair<TKey, TValue>> kvps)
        : base(kvps) { }
#endif
    /// <summary>
    /// Initializes a new instance of the
    /// <seealso cref="FlexInitDictionary{TKey, TValue}"/> class out of another
    /// <seealso cref="FlexInitDictionary{TKey, TValue}"/> instance.
    /// </summary>
    /// <param name="other">
    /// The other <seealso cref="FlexInitDictionary{TKey, TValue}"/> whose
    /// key-value pairs to copy.
    /// </param>
    public FlexInitDictionary(FlexInitDictionary<TKey, TValue> other)
        : base(other) { }

    /// <inheritdoc/>
    public override
#if SUPPORTS_COVARIANT_OVERRIDES
        FlexInitDictionary<TKey, TValue>
#else
        BaseFlexDictionary<TKey, TValue>
#endif
        Clone() => new FlexInitDictionary<TKey, TValue>(this);

    /// <inheritdoc/>
    protected override TValue GetNewEntryInitializationValue() => new();
}
