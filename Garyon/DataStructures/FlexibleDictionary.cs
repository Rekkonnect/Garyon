using System.Collections;
using System.Collections.Generic;

namespace Garyon.DataStructures
{
    /// <summary>Represents a flexible dictionary, which is a dictionary that allows indexing or setting values on non-existing keys without throwing exceptions.</summary>
    /// <typeparam name="TKey">The type of the keys that are being added to the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values that are being paired with the keys in the dictionary.</typeparam>
    public class FlexibleDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> dictionary;

        #region Hidden Interface Properties
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;
        #endregion

        public int Count => dictionary.Count;

        public ICollection<TKey> Keys => dictionary.Keys;
        public ICollection<TValue> Values => dictionary.Values;

        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class with the default initial capacity (16).</summary>
        public FlexibleDictionary() : this(16) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="capacity">The capacity of the dictionary.</param>
        public FlexibleDictionary(int capacity)
        {
            dictionary = new Dictionary<TKey, TValue>(capacity);
        }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="collection">The collection to initialize the dictionary from. Each item in the provided collection is added as a key and is mapped to the default value of the <typeparamref name="TValue"/> type.</param>
        public FlexibleDictionary(IEnumerable<TKey> collection)
            : this()
        {
            foreach (var v in collection)
                Add(v);
        }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleDictionary{TKey, TValue}"/> class out of another <seealso cref="FlexibleDictionary{TKey, TValue}"/> instance.</summary>
        /// <param name="other">The other <seealso cref="FlexibleDictionary{TKey, TValue}"/> whose key-value pairs to copy.</param>
        public FlexibleDictionary(FlexibleDictionary<TKey, TValue> other)
            : this(other.Count)
        {
            foreach (var kvp in other.dictionary)
                dictionary.Add(kvp.Key, kvp.Value);
        }

        public virtual void Add(TKey key, TValue value = default) => dictionary.TryAdd(key, value);
        public virtual void Add(KeyValuePair<TKey, TValue> kvp) => dictionary.Add(kvp.Key, kvp.Value);
        public virtual bool Remove(TKey key) => dictionary.Remove(key);
        public void Clear() => dictionary.Clear();

        /// <summary>Clones this <seealso cref="FlexibleDictionary{TKey, TValue}"/> and adds all its keys to the resulting instance.</summary>
        /// <returns>The cloned instance containing the same key-value pairs.</returns>
        public virtual FlexibleDictionary<TKey, TValue> Clone() => new FlexibleDictionary<TKey, TValue>(this);

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => dictionary.GetEnumerator();

        #region Hidden Interface Methods
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => (dictionary as IDictionary<TKey, TValue>).Contains(item);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => (dictionary as IDictionary<TKey, TValue>).CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => (dictionary as IDictionary<TKey, TValue>).Remove(item);
        #endregion

        /// <summary>Gets or sets the value of the specified key. If the key does not exist, it will be added to the dictionary.</summary>
        /// <param name="key">The key whose value to get or set.</param>
        /// <returns>The value of the key. When getting a non-existent key, it will have <typeparamref name="TValue"/>'s <see langword="default"/> value.</returns>
        public virtual TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                    dictionary.Add(key, default);
                return dictionary[key];
            }
            set
            {
                if (!ContainsKey(key))
                    dictionary.Add(key, value);
                else
                    dictionary[key] = value;
            }
        }
    }
}
