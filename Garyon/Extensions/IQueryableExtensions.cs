using Garyon.Functions;
using Garyon.Objects;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>Contains extensions for the <seealso cref="IQueryable{T}"/> interface.</summary>
public static partial class IQueryableExtensions
{
    // TODO: Provide those as extensions over IEnumerable too
    extension<T>(IQueryable<T> queryable)
    {
        /// <summary>
        /// Skips and takes a select number of items from the queryable source.
        /// </summary>
        /// <param name="skip">The number of items to skip.</param>
        /// <param name="take">The number of items to take.</param>
        public IQueryable<T> SkipTake(int skip, int take)
        {
            return queryable.Skip(skip).Take(take);
        }

        /// <summary>
        /// Paginates the data from the queryable source.
        /// </summary>
        /// <param name="pageIndex">The number of pages to skip.</param>
        /// <param name="pageSize">The size of each page.</param>
        public IQueryable<T> Paginate(int pageIndex, int pageSize)
        {
            int offset = pageIndex * pageSize;
            return queryable.SkipTake(offset, pageSize);
        }
    }
}
