using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

public static class StaticEnumerableExtensions
{
    extension(Enumerable)
    {
        public static IEnumerable<int> IndexRange(int count)
        {
            return Enumerable.Range(0, count);
        }
    }
}

