using System.Collections.Concurrent;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for the <see cref="ConcurrentBag{T}"/> type.
/// </summary>
public static class ConcurrentBagExtensions
{
    extension<T>(ConcurrentBag<T> bag)
    {
#if !HAS_CONCURRENT_BAG_CLEAR
        /// <summary>
        /// Clears out the bag by removing all its items sequentially.
        /// </summary>
        /// <remarks>
        /// This complements the lack of the built-in API. If the target
        /// framework adds support for this natively, this method is not
        /// defined.
        /// </remarks>
        public void Clear()
        {
            while (bag.TryTake(out _))
            {
            }
        }
#endif
    }
}
