using System.Collections;
using System.Collections.Generic;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <seealso cref="DictionaryEntry"/> struct.</summary>
    public static class DictionaryEntryExtensions
    {
        /// <summary>Converts a <seealso cref="DictionaryEntry"/> into a <seealso cref="KeyValuePair{TKey, TValue}"/>.</summary>
        /// <typeparam name="TKey">The type of the key in the <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
        /// <typeparam name="TValue">The type of the value in the <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
        /// <param name="entry">The entry to convert into a <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
        /// <returns>A <seealso cref="KeyValuePair{TKey, TValue}"/> constructed from the given <seealso cref="DictionaryEntry"/> whose key and value components are strongly typed.</returns>
        public static KeyValuePair<TKey, TValue?> ToKeyValuePair<TKey, TValue>(this DictionaryEntry entry)
        {
            return new((TKey)entry.Key, (TValue?)entry.Value);
        }
    }
}