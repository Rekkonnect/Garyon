#if !HAS_HASH_CODE

namespace System;

// This implementation is just a dummy version of the actual
// only for the purposes of providing the symbol to earlier
// versions of the framework.

internal struct HashCode
{
    public static int Combine<T1, T2>(T1 value1, T2 value2)
    {
        return value1.GetHashCode() ^ value2.GetHashCode();
    }
}

#endif