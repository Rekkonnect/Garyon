using Garyon.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

using ObjectKvp = KeyValuePair<object, object?>;

public static partial class IDictionaryExtensions
{
    extension(IDictionary dictionary)
    {
        /// <summary>
        /// Converts the keys of the dictionary into a collection of
        /// strongly-typed elements.
        /// </summary>
        /// <typeparam name="T">
        /// The type to cast all the keys of the dictionary into.
        /// </typeparam>
        /// <returns>
        /// An <seealso cref="IEnumerable{T}"/> containing the keys of the
        /// provided dictionary.
        /// </returns>
        public IEnumerable<T> Keys<T>()
        {
            return dictionary.Keys.Cast<T>();
        }

        /// <summary>
        /// Converts the values of the dictionary into a collection of
        /// strongly-typed elements.
        /// </summary>
        /// <typeparam name="T">
        /// The type to cast all the values of the dictionary into.
        /// </typeparam>
        /// <returns>
        /// An <seealso cref="IEnumerable{T}"/> containing the values of the
        /// provided dictionary.
        /// </returns>
        public IEnumerable<T> Values<T>()
        {
            return dictionary.Values.Cast<T>();
        }

        /// <summary>
        /// Enumerates the dictionary's entries as a collection of
        /// <seealso cref="DictionaryEntry"/> elements.
        /// </summary>
        public IEnumerable<DictionaryEntry> EnumerateDictionaryEntries()
        {
            return new DictionaryEntryEnumerator(dictionary);
        }

        /// <summary>
        /// Enumerates the dictionary's entries as a collection of
        /// <seealso cref="KeyValuePair{TKey, TValue}"/> elements of object keys
        /// and object values.
        /// </summary>
        public IEnumerable<ObjectKvp> EnumerateEntriesAsKvp()
        {
            return new DictionaryEntryEnumerator(dictionary);
        }

        // TODO: Add EnumerateEntries<TKey, TValue> extension
    }

    private sealed class DictionaryEntryEnumerator(IDictionary dictionary)
        : IEnumerator<DictionaryEntry>,
        IEnumerable<DictionaryEntry>,
        IEnumerator<ObjectKvp>,
        IEnumerable<ObjectKvp>
    {
        private readonly IDictionaryEnumerator _enumerator = dictionary.GetEnumerator();

        public DictionaryEntry Current => _enumerator.Entry;

        DictionaryEntry IEnumerator<DictionaryEntry>.Current => Current;
        ObjectKvp IEnumerator<ObjectKvp>.Current => EntryToKvp(Current);
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }
        public void Reset()
        {
            _enumerator.Reset();
        }
        void IDisposable.Dispose() { }

        IEnumerator<DictionaryEntry> IEnumerable<DictionaryEntry>.GetEnumerator() => this;
        IEnumerator<ObjectKvp> IEnumerable<ObjectKvp>.GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;

        private static ObjectKvp EntryToKvp(DictionaryEntry entry)
        {
            return new(entry.Key, entry.Value);
        }
    }
}
