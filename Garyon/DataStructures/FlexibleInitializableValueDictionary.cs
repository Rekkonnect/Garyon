using Garyon.Objects;
using System.Collections.Generic;

namespace Garyon.DataStructures
{
    /// <summary>Represents a flexible dictionary where the newly added keys will map to a new initialized value of <typeparamref name="TValue"/>.</summary>
    /// <typeparam name="TKey">The type of the key to index the dictionary contents by.</typeparam>
    /// <typeparam name="TValue">The type of the initializable values that are being paired with the keys in the dictionary.</typeparam>
    public class FlexibleInitializableValueDictionary<TKey, TValue> : FlexibleDictionary<TKey, TValue>
        where TValue : new()
    {
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> class with the default initial capacity (16).</summary>
        public FlexibleInitializableValueDictionary()
            : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="capacity">The capacity of the dictionary.</param>
        public FlexibleInitializableValueDictionary(int capacity)
            : base(capacity) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="collection">The collection to initialize the dictionary from. Each item in the provided collection is added as a key and is mapped to the default value of the <typeparamref name="TValue"/> type.</param>
        public FlexibleInitializableValueDictionary(IEnumerable<TKey> collection)
            : base(collection) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> class. Each key in the provided collection is transformed with <paramref name="valueSelector"/> and added to the dictionary.</summary>
        /// <param name="keyCollection">The collection of keys to initialize the dictionary from.</param>
        /// <param name="valueSelector">The selector that transforms a <typeparamref name="TKey"/> into a <typeparamref name="TValue"/>.</param>
        public FlexibleInitializableValueDictionary(IEnumerable<TKey> keyCollection, ValueSelector<TKey, TValue> valueSelector)
            : base(keyCollection, valueSelector) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> class. Each item in the provided collection is transformed with <paramref name="keySelector"/> and added to the dictionary.</summary>
        /// <param name="valueCollection">The collection of values to initialize the dictionary from.</param>
        /// <param name="keySelector">The selector that transforms a <typeparamref name="TValue"/> into a <typeparamref name="TKey"/>.</param>
        public FlexibleInitializableValueDictionary(IEnumerable<TValue> valueCollection, KeySelector<TKey, TValue> keySelector)
            : base(valueCollection, keySelector) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="kvps">The collection of <seealso cref="KeyValuePair{TKey, TValue}"/> objects to initialize the dictionary from.</param>
        public FlexibleInitializableValueDictionary(IEnumerable<KeyValuePair<TKey, TValue>> kvps)
            : base(kvps) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> class out of another <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> instance.</summary>
        /// <param name="other">The other <seealso cref="FlexibleInitializableValueDictionary{TKey, TValue}"/> whose key-value pairs to copy.</param>
        public FlexibleInitializableValueDictionary(FlexibleInitializableValueDictionary<TKey, TValue> other)
            : base(other) { }

        /// <inheritdoc/>
        public override FlexibleInitializableValueDictionary<TKey, TValue> Clone() => new FlexibleInitializableValueDictionary<TKey, TValue>(this);

        /// <inheritdoc/>
        protected override TValue GetNewEntryInitializationValue() => new();
    }
}
