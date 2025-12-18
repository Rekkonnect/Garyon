using Garyon.Objects.Enumerators;
using System.Collections.Generic;

namespace Garyon.Extensions;

#nullable enable

public static partial class IEnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>Wraps the <seealso cref="IEnumerable{T}"/> into an <seealso cref="IndexedEnumerable{T}"/> for enumeration with index.</summary>
        /// <returns>The <seealso cref="IndexedEnumerable{T}"/> that wraps the source enumerable for indexed enumeration.</returns>
        public IndexedEnumerable<T> WithIndex()
        {
            return new IndexedEnumerable<T>(source);
        }
    }
}
