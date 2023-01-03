using Garyon.Functions.PointerHelpers;
using System.Runtime.CompilerServices;
#if HAS_INTRINSICS
using System.Runtime.Intrinsics;
#endif

namespace Garyon.Functions.IntrinsicsHelpers;

/// <summary>Provides helper functions for SIMD intrinsics.</summary>
public abstract unsafe class SIMDIntrinsicsHelper
{
    #region Last Element Storing
    /// <summary>Stores the last elements of a sequence into the target sequence of the same element type. This function is made with regards to storing elements through <seealso cref="Vector128"/>s, thus storing up to 15 bytes that remain to be processed from the original sequence.</summary>
    /// <typeparam name="T">The type of the elements in the sequences. Its size in bytes has to be a power of 2, up to 8.</typeparam>
    /// <param name="origin">The origin sequence, passed as a pointer.</param>
    /// <param name="target">The target sequence, passed as a pointer.</param>
    /// <param name="length">The length of the sequence.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreLastElementsVector128<T>(T* origin, T* target, uint length)
        where T : unmanaged
    {
        StoreLastElementsVector128(origin, target, 0, length);
    }
    /// <summary>Stores the last elements of a sequence into the target sequence of the same element type. This function is made with regards to storing elements through <seealso cref="Vector128"/>s, thus storing up to 15 bytes that remain to be processed from the original sequence.</summary>
    /// <typeparam name="T">The type of the elements in the sequences. Its size in bytes has to be a power of 2, up to 8.</typeparam>
    /// <param name="origin">The origin sequence, passed as a pointer.</param>
    /// <param name="target">The target sequence, passed as a pointer.</param>
    /// <param name="index">The first index of the sequence that will be processed.</param>
    /// <param name="length">The length of the sequence.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreLastElementsVector128<T>(T* origin, T* target, uint index, uint length)
        where T : unmanaged
    {
        PointerArithmetic.Increment(ref origin, ref target, index);

        uint count = length - index;

        if (sizeof(T) <= sizeof(byte))
            StoreRemainingElements(ref origin, ref target, count, 8);
        if (sizeof(T) <= sizeof(short))
            StoreRemainingElements(ref origin, ref target, count, 4);
        if (sizeof(T) <= sizeof(int))
            StoreRemainingElements(ref origin, ref target, count, 2);
        if (sizeof(T) <= sizeof(long))
            StoreRemainingElements(ref origin, ref target, count, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void StoreRemainingElements<T>(ref T* origin, ref T* target, uint count, uint remainder)
        where T : unmanaged
    {
        if ((count & remainder) > 0)
        {
            if (sizeof(T) is sizeof(byte))
                StoreRemainingByte((byte*)origin, (byte*)target, remainder);
            if (sizeof(T) is sizeof(short))
                StoreRemainingInt16((short*)origin, (short*)target, remainder);
            if (sizeof(T) is sizeof(int))
                StoreRemainingInt32((int*)origin, (int*)target, remainder);
            if (sizeof(T) is sizeof(long))
                StoreRemainingInt64((long*)origin, (long*)target, remainder);

            PointerArithmetic.Increment(ref origin, ref target, remainder);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void StoreRemainingByte(byte* origin, byte* target, uint remainder)
    {
        if (remainder == 8)
            Store<byte, long>(origin, target);
        else if (remainder == 4)
            Store<byte, int>(origin, target);
        else if (remainder == 2)
            Store<byte, short>(origin, target);
        else if (remainder == 1)
            Store<byte, byte>(origin, target);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void StoreRemainingInt16(short* origin, short* target, uint remainder)
    {
        if (remainder == 4)
            Store<short, long>(origin, target);
        else if (remainder == 2)
            Store<short, int>(origin, target);
        else if (remainder == 1)
            Store<short, short>(origin, target);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void StoreRemainingInt32(int* origin, int* target, uint remainder)
    {
        if (remainder == 2)
            Store<int, long>(origin, target);
        else if (remainder == 1)
            Store<int, int>(origin, target);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void StoreRemainingInt64(long* origin, long* target, uint remainder)
    {
        if (remainder == 1)
            Store<long, long>(origin, target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreLastElementsVector128<T>(T* origin, double* target, uint index, uint length)
        where T : unmanaged
    {
        PointerArithmetic.Increment(ref origin, ref target, index);

        uint count = length - index;

        StoreRemainingElements(1, ref origin, ref target, length);

        static void StoreRemainingElements(uint remainder, ref T* origin, ref double* target, uint count)
        {
            if ((count & remainder) > 0)
            {
                if (typeof(T) == typeof(byte))
                    *target = *(byte*)origin;
                else if (typeof(T) == typeof(short))
                    *target = *(short*)origin;
                else if (typeof(T) == typeof(int))
                    *target = *(int*)origin;
                else if (typeof(T) == typeof(float))
                    *target = *(float*)origin;

                PointerArithmetic.Increment(ref origin, ref target, remainder);
            }
        }
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreLastElementsVector128(double* origin, int* target, uint index, uint length)
    {
        PointerArithmetic.Increment(ref origin, ref target, index);

        uint count = length - index;

        StoreRemainingElements(1, ref origin, ref target, count);

        static void StoreRemainingElements(uint remainder, ref double* origin, ref int* target, uint count)
        {
            if ((count & remainder) > 0)
            {
                *target = (int)*origin;
                PointerArithmetic.Increment(ref origin, ref target, remainder);
            }
        }
    }
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Store<TOriginal, TNew>(TOriginal* origin, TOriginal* target)
        where TOriginal : unmanaged
        where TNew : unmanaged
    {
        Store<TOriginal, TNew>(origin, target, 0);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Store<TOriginal, TNew>(TOriginal* origin, TOriginal* target, uint index)
        where TOriginal : unmanaged
        where TNew : unmanaged
    {
        var o = (TNew*)(origin + index);
        var t = (TNew*)(target + index);
        *t = *o;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Store<TOrigin, TTarget, TNew>(TOrigin* origin, TTarget* target)
        where TOrigin : unmanaged
        where TTarget : unmanaged
        where TNew : unmanaged
    {
        *(TNew*)target = *(TNew*)origin;
    }

    #region Vector64
    #region T* -> byte*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreVector64(byte* origin, byte* target, uint index) => Store<byte, long>(origin, target, index);
    #endregion
    #region T* -> short*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreVector64(short* origin, short* target, uint index) => Store<short, long>(origin, target, index);
    #endregion
    #region T* -> int*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreVector64(int* origin, int* target, uint index) => Store<int, long>(origin, target, index);
    #endregion
    #region T* -> float*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreVector64(float* origin, float* target, uint index) => Store<float, long>(origin, target, index);
    #endregion
    #region T* -> long*
    public static void StoreVector64(byte* origin, long* target, uint index) => target[index] = origin[index];
    public static void StoreVector64(short* origin, long* target, uint index) => target[index] = origin[index];
    public static void StoreVector64(int* origin, long* target, uint index) => target[index] = origin[index];
    public static void StoreVector64(float* origin, long* target, uint index) => target[index] = (long)origin[index];
    public static void StoreVector64(double* origin, long* target, uint index) => target[index] = (long)origin[index];
    #endregion
    #region T* -> double*
    public static void StoreVector64(byte* origin, double* target, uint index) => target[index] = origin[index];
    public static void StoreVector64(short* origin, double* target, uint index) => target[index] = origin[index];
    public static void StoreVector64(int* origin, double* target, uint index) => target[index] = origin[index];
    public static void StoreVector64(long* origin, double* target, uint index) => target[index] = origin[index];
    public static void StoreVector64(float* origin, double* target, uint index) => target[index] = origin[index];
    #endregion
    #endregion

    #region Vector32
    #region T* -> byte*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreVector32(byte* origin, byte* target, uint index) => Store<byte, int>(origin, target, index);
    #endregion
    #region T* -> short*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreVector32(short* origin, short* target, uint index) => Store<short, int>(origin, target, index);
    #endregion
    #region T* -> int*
    public static void StoreVector32(byte* origin, int* target, uint index) => target[index] = origin[index];
    public static void StoreVector32(short* origin, int* target, uint index) => target[index] = origin[index];
    public static void StoreVector32(float* origin, int* target, uint index) => target[index] = (int)origin[index];
    #endregion
    #region T* -> float*
    public static void StoreVector32(byte* origin, float* target, uint index) => target[index] = origin[index];
    public static void StoreVector32(short* origin, float* target, uint index) => target[index] = origin[index];
    public static void StoreVector32(int* origin, float* target, uint index) => target[index] = origin[index];
    #endregion
    #endregion

    #region Vector16
    #region T* -> byte*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void StoreVector16(byte* origin, byte* target, uint index) => Store<byte, short>(origin, target, index);
    #endregion
    #region T* -> short*
    public static void StoreVector16(byte* origin, short* target, uint index) => target[index] = origin[index];
    #endregion
    #endregion
}
