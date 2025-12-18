#if !HAS_HASH_CODE

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System;

// This implementation is just a dummy version of the actual
// only for the purposes of providing the symbol to earlier
// versions of the framework.

internal struct HashCode
{
    public static int Combine<T1, T2>(T1 value1, T2 value2)
    {
        return GetHashCode(value1) ^ GetHashCode(value2);
    }

    private static int GetHashCode<T>(T value)
    {
        return value?.GetHashCode() ?? 0;
    }
}

#endif
