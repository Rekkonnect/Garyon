using Garyon.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.DataStructures
{
    /// <summary>Represents a flexible dictionary, which is a dictionary that allows indexing or setting values on non-existing keys without throwing exceptions.</summary>
    /// <typeparam name="TKey">The type of the keys that are being added to the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values that are being paired with the keys in the dictionary.</typeparam>
    public class FlexibleDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        /// <summary>The internal <seealso cref="Dictionary{TKey, TValue}"/> instance.</summary>
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

        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class with the default initial capacity (16).</summary>
        public FlexibleDictionary()
            : this(16) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="capacity">The capacity of the dictionary.</param>
        public FlexibleDictionary(int capacity)
        {
            Dictionary = new Dictionary<TKey, TValue>(capacity);
        }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class. Each item in the provided collection is added as a key and is mapped to the default value of the <typeparamref name="TValue"/> type.</summary>
        /// <param name="collection">The collection to initialize the dictionary from.</param>
        public FlexibleDictionary(IEnumerable<TKey> collection)
            : this(collection, default(TValue)) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class. Each item in the provided collection is added as a key and is mapped to the specified value of the <typeparamref name="TValue"/> type.</summary>
        /// <param name="collection">The collection to initialize the dictionary from.</param>
        /// <param name="initialValue">The initial value to map each key to.</param>
        public FlexibleDictionary(IEnumerable<TKey> collection, TValue initialValue)
            : this(collection.Count())
        {
            foreach (var v in collection)
                Add(v, initialValue);
        }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class. Each key in the provided collection is transformed with <paramref name="valueSelector"/> and added to the dictionary.</summary>
        /// <param name="keyCollection">The collection of keys to initialize the dictionary from.</param>
        /// <param name="valueSelector">The selector that transforms a <typeparamref name="TKey"/> into a <typeparamref name="TValue"/>.</param>
        public FlexibleDictionary(IEnumerable<TKey> keyCollection, ValueSelector<TKey, TValue> valueSelector)
            : this()
        {
            foreach (var key in keyCollection)
                Add(key, valueSelector(key));
        }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class. Each item in the provided collection is transformed with <paramref name="keySelector"/> and added to the dictionary.</summary>
        /// <param name="valueCollection">The collection of values to initialize the dictionary from.</param>
        /// <param name="keySelector">The selector that transforms a <typeparamref name="TValue"/> into a <typeparamref name="TKey"/>.</param>
        public FlexibleDictionary(IEnumerable<TValue> valueCollection, KeySelector<TKey, TValue> keySelector)
            : this()
        {
            foreach (var value in valueCollection)
                Add(keySelector(value), value);
        }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="kvps">The collection of <seealso cref="KeyValuePair{TKey, TValue}"/> objects to initialize the dictionary from.</param>
        public FlexibleDictionary(IEnumerable<KeyValuePair<TKey, TValue>> kvps)
        {
            Dictionary = new Dictionary<TKey, TValue>(kvps);
        }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class out of another <seealso cref="FlexibleDictionary{TKey, TValue}"/> instance.</summary>
        /// <param name="other">The other <seealso cref="FlexibleDictionary{TKey, TValue}"/> whose key-value pairs to copy.</param>
        public FlexibleDictionary(FlexibleDictionary<TKey, TValue> other)
        {
            Dictionary = new Dictionary<TKey, TValue>(other.Dictionary);
        }

        /// <inheritdoc/>
        public virtual void Add(TKey key, TValue value = default) => Dictionary.TryAdd(key, value);
        /// <inheritdoc/>
        public virtual void Add(KeyValuePair<TKey, TValue> kvp) => Dictionary.Add(kvp.Key, kvp.Value);

        /// <inheritdoc/>
        public virtual bool Remove(TKey key) => Dictionary.Remove(key);
        /// <summary>Removes a collection of keys from this <seealso cref="FlexibleDictionary{TKey, TValue}"/>. Keys that are not present will be simply ignored.</summary>
        /// <param name="keys">The keys to remove. Must not be <see langword="null"/>.</param>
        /// <returns>The number of keys that were removed.</returns>
        public int RemoveKeys(params TKey[] keys) => RemoveKeys((IEnumerable<TKey>)keys);
        /// <summary>Removes a collection of keys from this <seealso cref="FlexibleDictionary{TKey, TValue}"/>. Keys that are not present will be simply ignored.</summary>
        /// <param name="keys">The keys to remove. Must not be <see langword="null"/>.</param>
        /// <returns>The number of keys that were removed.</returns>
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

        /// <summary>Clones this <seealso cref="FlexibleDictionary{TKey, TValue}"/> and adds all its keys to the resulting instance.</summary>
        /// <returns>The cloned instance containing the same key-value pairs.</returns>
        public virtual FlexibleDictionary<TKey, TValue> Clone() => new(this);

        /// <inheritdoc/>
        public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);
        /// <inheritdoc/>
        public bool TryGetValue(TKey key, out TValue value) => Dictionary.TryGetValue(key, out value);

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Dictionary.GetEnumerator();

        #region Hidden Interface Methods
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => (Dictionary as IDictionary<TKey, TValue>).Contains(item);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => (Dictionary as IDictionary<TKey, TValue>).CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => (Dictionary as IDictionary<TKey, TValue>).Remove(item);
        #endregion

        /// <summary>Gets the value that will be stored by default upon creating a new entry through the <see langword="this"/> accessor. This does not affect the <see cref="Add(TKey, TValue)"/> function.</summary>
        /// <returns>The new value that will be stored in the new entry.</returns>
        protected virtual TValue GetNewEntryInitializationValue() => default;

        /// <summary>Gets or sets the value of the specified key. If the key does not exist, it will be added to the dictionary.</summary>
        /// <param name="key">The key whose value to get or set.</param>
        /// <returns>The value of the key. When getting a non-existent key, it will have <typeparamref name="TValue"/>'s <see langword="default"/> value.</returns>
        public virtual TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                    Dictionary.Add(key, GetNewEntryInitializationValue());
                return Dictionary[key];
            }
            set
            {
                if (!ContainsKey(key))
                    Dictionary.Add(key, value);
                else
                    Dictionary[key] = value;
            }
        }
    }
}
