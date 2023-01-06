using System.Collections.Generic;

namespace Garyon.Internal;

internal static class HashSet
{
    public static HashSet<T> New<T>(int count)
    {
#if HAS_HASHSET_CAPACITY_CTOR
        return new(count);
#else
        return new();
#endif
    }
    public static HashSet<T> New<T>(int count, IEqualityComparer<T> comparer)
    {
#if HAS_HASHSET_CAPACITY_CTOR
        return new(count, comparer);
#else
        return new(comparer);
#endif
    }
}
