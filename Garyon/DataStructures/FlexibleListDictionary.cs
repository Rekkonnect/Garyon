using System.Collections.Generic;

namespace Garyon.DataStructures
{
    /// <summary>Represents a flexible dictionary that maps a key to a list of objects.</summary>
    /// <typeparam name="TKey">The type of the key to index the dictionary contents by.</typeparam>
    /// <typeparam name="TObject">The type of objects to be stored in the list of objects each key is mapped to.</typeparam>
    public class FlexibleListDictionary<TKey, TObject> : FlexibleDictionary<TKey, List<TObject>>
    {
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleListDictionary{TKey, TObject}"/> class with the default initial capacity (16).</summary>
        public FlexibleListDictionary()
            : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleListDictionary{TKey, TObject}"/> class.</summary>
        /// <param name="capacity">The capacity of the dictionary.</param>
        public FlexibleListDictionary(int capacity)
            : base(capacity) { }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleListDictionary{TKey, TObject}"/> class.</summary>
        /// <param name="collection">The collection to initialize the dictionary from. Each item in the provided collection is added as a key and is mapped to the default value of the <typeparamref name="TObject"/> type.</param>
        public FlexibleListDictionary(IEnumerable<TKey> collection)
            : base()
        {
            foreach (var k in collection)
                Add(k, new List<TObject>());
        }
        /// <summary>Initializes a new instance of the <seealso cref="FlexibleListDictionary{TKey, TObject}"/> class out of another <seealso cref="FlexibleListDictionary{TKey, TObject}"/> instance.</summary>
        /// <param name="other">The other <seealso cref="FlexibleListDictionary{TKey, TObject}"/> whose key-value pairs to copy.</param>
        public FlexibleListDictionary(FlexibleListDictionary<TKey, TObject> other)
            : base()
        {
            foreach (var kvp in other)
                Add(kvp.Key, new List<TObject>(kvp.Value));
        }

        public override FlexibleListDictionary<TKey, TObject> Clone() => new FlexibleListDictionary<TKey, TObject>(this);

        /// <summary>Attempts to get a value within the mapped list from a provided key and with an index within the list.</summary>
        /// <param name="key">The key to look for in the dictionary.</param>
        /// <param name="index">The index of the desired value within the list the provided key maps to.</param>
        /// <param name="value">The value that was requested.</param>
        /// <returns><see langword="true"/> if the key existed in the dictionary and the index was within the list's bounds, otherwise <see langword="false"/>.</returns>
        public bool TryGetValue(TKey key, int index, out TObject value)
        {
            value = default;
            if (!TryGetValue(key, out var list))
                return false;
            if (index >= list.Count)
                return false;
            value = list[index];
            return true;
        }

        protected override List<TObject> GetNewEntryInitializationValue() => new List<TObject>();
    }
}
