using System.Collections;
using System.Collections.Generic;

namespace Garyon.Extensions;

public static class NonGenericICollectionExtensions
{
    extension(ICollection source)
    {
        /// <summary>
        /// Determines whether the enumerable is empty or not via its
        /// <see cref="ICollection.Count"/> property.
        /// </summary>
        /// <remarks>
        /// Avoiding naming conflict with
        /// <see cref="IReadOnlyCollectionExtensions.get_IsEmpty{T}(IReadOnlyCollection{T})"/>.
        /// </remarks>
        public bool IsEmptyCollection => source.Count is 0;
    }
}
