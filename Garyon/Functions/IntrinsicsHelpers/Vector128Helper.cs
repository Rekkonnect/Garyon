#if HAS_INTRINSICS

using Garyon.Exceptions;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers;

/// <summary>Provides helper functions for the <seealso cref="Vector128{T}"/> type.</summary>
public abstract unsafe class Vector128Helper : SIMDVectorHelper
{
    /// <summary>Creates a new <seealso cref="Vector128{T}"/> instance with all elements initialized to the specified value.</summary>
    /// <typeparam name="T">The type of the elements the <seealso cref="Vector128{T}"/> contains. Must have a size of 1, 2, 4 or 8.</typeparam>
    /// <param name="value">The value that all elements will be initialized to.</param>
    /// <returns>A new <seealso cref="Vector128{T}"/> with all elements initialized to <paramref name="value"/>.</returns>
    public static Vector128<T> Create<T>(T value)
        where T : unmanaged
    {
        if (sizeof(T) is sizeof(byte))
            return Vector128.Create(*(byte*)&value).As<byte, T>();
        if (sizeof(T) is sizeof(short))
            return Vector128.Create(*(short*)&value).As<short, T>();
        if (sizeof(T) is sizeof(int))
            return Vector128.Create(*(int*)&value).As<int, T>();
        if (sizeof(T) is sizeof(long))
            return Vector128.Create(*(long*)&value).As<long, T>();

        ThrowHelper.Throw<InvalidOperationException>();
        return default;
    }
    /// <summary>Creates a new <seealso cref="Vector128{T}"/> instance with the first element initialized to the specified value and the remaining elements initialized to zero.</summary>
    /// <typeparam name="T">The type of the elements the <seealso cref="Vector128{T}"/> contains. Must have a size of 1, 2, 4 or 8.</typeparam>
    /// <param name="value">The value that element 0 will be initialized to.</param>
    /// <returns>A new <seealso cref="Vector128{T}"/> instance with the first element initialized to <paramref name="value"/> and the remaining elements initialized to zero.</returns>
    public static Vector128<T> CreateScalar<T>(T value)
        where T : unmanaged
    {
        if (sizeof(T) is sizeof(byte))
            return Vector128.CreateScalar(*(byte*)&value).As<byte, T>();
        if (sizeof(T) is sizeof(short))
            return Vector128.CreateScalar(*(short*)&value).As<short, T>();
        if (sizeof(T) is sizeof(int))
            return Vector128.CreateScalar(*(int*)&value).As<int, T>();
        if (sizeof(T) is sizeof(long))
            return Vector128.CreateScalar(*(long*)&value).As<long, T>();

        ThrowHelper.Throw<InvalidOperationException>();
        return default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> AllBitsSet<T>()
        where T : unmanaged
    {
        return AllBitsSetSingle().As<float, T>();
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> AllBitsSetSingle()
    {
#if HAS_VECTOR_ALLBITS
        return Vector128<float>.AllBitsSet;
#else
        var zero = Vector128<float>.Zero;
        return Sse.AndNot(zero, zero);
#endif
    }
}

#endif
