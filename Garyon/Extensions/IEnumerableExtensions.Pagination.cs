using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

public static partial class IEnumerableExtensions
{
    extension<T>(IEnumerable<T> enumerable)
    {
        /// <summary>
        /// Skips and takes a select number of items from the enumerable source.
        /// </summary>
        /// <param name="skip">
        /// The number of items to skip.
        /// </param>
        /// <param name="take">
        /// The number of items to take.
        /// </param>
        public IEnumerable<T> SkipTake(int skip, int take)
        {
            return enumerable.Skip(skip).Take(take);
        }

        /// <summary>
        /// Paginates the data from the enumerable source.
        /// </summary>
        /// <param name="pageIndex">
        /// The number of pages to skip.
        /// </param>
        /// <param name="pageSize">
        /// The size of each page.
        /// </param>
        public IEnumerable<T> Paginate(int pageIndex, int pageSize)
        {
            int offset = pageIndex * pageSize;
            return enumerable.SkipTake(offset, pageSize);
        }
    }
}
