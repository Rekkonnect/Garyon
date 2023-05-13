#if HAS_INTRINSICS

using Garyon.Exceptions;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers;

/// <summary>Provides helper functions for the <seealso cref="Vector256{T}"/> type.</summary>
public abstract unsafe class Vector256Helper : SIMDVectorHelper
{
    /// <summary>Creates a new <seealso cref="Vector256{T}"/> instance with all elements initialized to the specified value.</summary>
    /// <typeparam name="T">The type of the elements the <seealso cref="Vector256{T}"/> contains. Must have a size of 1, 2, 4 or 8.</typeparam>
    /// <param name="value">The value that all elements will be initialized to.</param>
    /// <returns>A new <seealso cref="Vector256{T}"/> with all elements initialized to <paramref name="value"/>.</returns>
    public static Vector256<T> Create<T>(T value)
        where T : unmanaged
    {
        if (typeof(T) == typeof(float))
            return Vector256.Create(*(float*)&value).As<float, T>();
        if (typeof(T) == typeof(double))
            return Vector256.Create(*(double*)&value).As<double, T>();

        switch (sizeof(T))
        {
            case sizeof(byte):
                return Vector256.Create(*(byte*)&value).As<byte, T>();
            case sizeof(short):
                return Vector256.Create(*(short*)&value).As<short, T>();
            case sizeof(int):
                return Vector256.Create(*(int*)&value).As<int, T>();
            case sizeof(long):
                return Vector256.Create(*(long*)&value).As<long, T>();
        }

        ThrowHelper.Throw<InvalidOperationException>();
        return default;
    }
    /// <summary>Creates a new <seealso cref="Vector256{T}"/> instance with the first element initialized to the specified value and the remaining elements initialized to zero.</summary>
    /// <typeparam name="T">The type of the elements the <seealso cref="Vector256{T}"/> contains. Must have a size of 1, 2, 4 or 8.</typeparam>
    /// <param name="value">The value that element 0 will be initialized to.</param>
    /// <returns>A new <seealso cref="Vector256{T}"/> instance with the first element initialized to <paramref name="value"/> and the remaining elements initialized to zero.</returns>
    public static Vector256<T> CreateScalar<T>(T value)
        where T : unmanaged
    {
        if (typeof(T) == typeof(float))
            return Vector256.CreateScalar(*(float*)&value).As<float, T>();
        if (typeof(T) == typeof(double))
            return Vector256.CreateScalar(*(double*)&value).As<double, T>();

        switch (sizeof(T))
        {
            case sizeof(byte):
                return Vector256.CreateScalar(*(byte*)&value).As<byte, T>();
            case sizeof(short):
                return Vector256.CreateScalar(*(short*)&value).As<short, T>();
            case sizeof(int):
                return Vector256.CreateScalar(*(int*)&value).As<int, T>();
            case sizeof(long):
                return Vector256.CreateScalar(*(long*)&value).As<long, T>();
        }

        ThrowHelper.Throw<InvalidOperationException>();
        return default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> AllBitsSet<T>()
        where T : unmanaged
    {
#if HAS_VECTOR_ALLBITS
        return Vector256<T>.AllBitsSet;
#else
        var zero = Vector256<T>.Zero;
        if (typeof(T) == typeof(float))
            return AllBitsSetSingle().As<float, T>();
        if (typeof(T) == typeof(double))
            return Avx.AndNot(zero.As<T, double>(), zero.As<T, double>()).As<double, T>();

        switch (sizeof(T))
        {
            case sizeof(byte):
                return Avx2.AndNot(zero.As<T, byte>(), zero.As<T, byte>()).As<byte, T>();
            case sizeof(short):
                return Avx2.AndNot(zero.As<T, short>(), zero.As<T, short>()).As<short, T>();
            case sizeof(int):
                return Avx2.AndNot(zero.As<T, int>(), zero.As<T, int>()).As<int, T>();
            case sizeof(long):
                return Avx2.AndNot(zero.As<T, long>(), zero.As<T, long>()).As<long, T>();
        }

        ThrowHelper.Throw<InvalidOperationException>();
        return default;
#endif
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<float> AllBitsSetSingle()
    {
#if HAS_VECTOR_ALLBITS
        return Vector256<float>.AllBitsSet;
#else
        var zero = Vector256<float>.Zero;
        return Avx.AndNot(zero, zero);
#endif
    }
}

#endif
