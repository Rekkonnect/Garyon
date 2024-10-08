﻿namespace Garyon.Functions;

#nullable enable

// Copy-pasted until autogenerated
/// <summary>
/// Provides methods for quickly checking against
/// nullability for multiple objects simultaneously.
/// </summary>
public static class NullGuards
{
    public static bool AnyNull<T1, T2>(T1? value1, T2? value2)
    {
        return value1 is null
            || value2 is null;
    }
    public static bool AnyNull<T1, T2, T3>(T1? value1, T2? value2, T3? value3)
    {
        return value1 is null
            || value2 is null
            || value3 is null;
    }
    public static bool AnyNull<T1, T2, T3, T4>(T1? value1, T2? value2, T3? value3, T4? value4)
    {
        return value1 is null
            || value2 is null
            || value3 is null
            || value4 is null;
    }

    public static bool AnyNull<T1, T2>(T1? value1, T2? value2)
        where T1 : struct
        where T2 : struct
    {
        return value1 is null
            || value2 is null;
    }
    public static bool AnyNull<T1, T2, T3>(T1? value1, T2? value2, T3? value3)
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        return value1 is null
            || value2 is null
            || value3 is null;
    }
    public static bool AnyNull<T1, T2, T3, T4>(T1? value1, T2? value2, T3? value3, T4? value4)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        return value1 is null
            || value2 is null
            || value3 is null
            || value4 is null;
    }

    public static bool AnyNonNull<T1, T2>(T1? value1, T2? value2)
    {
        return value1 is not null
            || value2 is not null;
    }
    public static bool AnyNonNull<T1, T2, T3>(T1? value1, T2? value2, T3? value3)
    {
        return value1 is not null
            || value2 is not null
            || value3 is not null;
    }
    public static bool AnyNonNull<T1, T2, T3, T4>(T1? value1, T2? value2, T3? value3, T4? value4)
    {
        return value1 is not null
            || value2 is not null
            || value3 is not null
            || value4 is not null;
    }

    public static bool AnyNonNull<T1, T2>(T1? value1, T2? value2)
        where T1 : struct
        where T2 : struct
    {
        return value1 is not null
            || value2 is not null;
    }
    public static bool AnyNonNull<T1, T2, T3>(T1? value1, T2? value2, T3? value3)
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        return value1 is not null
            || value2 is not null
            || value3 is not null;
    }
    public static bool AnyNonNull<T1, T2, T3, T4>(T1? value1, T2? value2, T3? value3, T4? value4)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        return value1 is not null
            || value2 is not null
            || value3 is not null
            || value4 is not null;
    }

    public static bool NoneNull<T1, T2>(T1? value1, T2? value2)
    {
        return !AnyNull(value1, value2);
    }
    public static bool NoneNull<T1, T2, T3>(T1? value1, T2? value2, T3? value3)
    {
        return !AnyNull(value1, value2, value3);
    }
    public static bool NoneNull<T1, T2, T3, T4>(T1? value1, T2? value2, T3? value3, T4? value4)
    {
        return !AnyNull(value1, value2, value3, value4);
    }

    public static bool NoneNull<T1, T2>(T1? value1, T2? value2)
        where T1 : struct
        where T2 : struct
    {
        return !AnyNull(value1, value2);
    }
    public static bool NoneNull<T1, T2, T3>(T1? value1, T2? value2, T3? value3)
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        return !AnyNull(value1, value2, value3);
    }
    public static bool NoneNull<T1, T2, T3, T4>(T1? value1, T2? value2, T3? value3, T4? value4)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        return !AnyNull(value1, value2, value3, value4);
    }

    public static bool SingleNull<T1, T2>(T1? value1, T2? value2)
    {
        return value1 is null
             ^ value2 is null;
    }
    public static bool SingleNull<T1, T2>(T1? value1, T2? value2)
        where T1 : struct
        where T2 : struct
    {
        return value1 is null
             ^ value2 is null;
    }
}