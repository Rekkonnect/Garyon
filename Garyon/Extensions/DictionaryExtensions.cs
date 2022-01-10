using System.Collections.Generic;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for dictionaries.</summary>
    public static class DictionaryExtensions
    {
        /// <summary>Clones the dictionary and returns a shallow copy of it.</summary>
        /// <param name="dictionary">The dictionary to clone.</param>
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return new(dictionary);
        }
    }
}
