using System.Collections.Generic;

namespace Garyon.Extensions;

public static class IReadOnlyCollectionExtensions
{
    extension<T>(IReadOnlyCollection<T> source)
    {
        /// <summary>
        /// Determines whether the enumerable is empty or not via its
        /// <see cref="IReadOnlyCollection{T}.Count"/> property.
        /// </summary>
        public bool IsEmpty => source.Count is 0;
    }
}
