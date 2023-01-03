#if HAS_INTRINSICS

using Garyon.Functions.IntrinsicsHelpers;
using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.PointerHelpers;

/// <summary>Contains unsafe helper functions for array copying using SIMD. All functions check whether the minimum supported instruction set is included; in the case that the set is unavailable, the functions simply do nothing.</summary>
public static unsafe class SIMDPointerConversion
{
    private interface ISIMDPointerConversion<TFrom, TTo> : ISIMDOperation
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        public abstract void CopyRemainingElements(TFrom* origin, TTo* target, uint index, uint length);
        public abstract void PerformIteration(TFrom* origin, TTo* target, uint index);
    }

    #region Vector128
    private class SameTypeSIMDPointerConverterVector128<T>
        where T : unmanaged
    {
        public static bool PerformConversion(T* origin, T* target, uint length)
        {
            return SIMDPointerConverterVector128<T, T, SameTypeConversionVector128<T>>.PerformConversion(origin, target, length);
        }
    }
    private class SameSizeSIMDPointerConverterVector128<TFrom, TTo>
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        public static bool PerformConversion(TFrom* origin, TTo* target, uint length)
        {
            return SIMDPointerConverterVector128<TFrom, TTo, SameSizeConversionVector128<TFrom, TTo>>.PerformConversion(origin, target, length);
        }
    }
    private class SIMDPointerConverterVector128<TFrom, TTo, TConversion>
        where TFrom : unmanaged
        where TTo : unmanaged
        where TConversion : struct, ISIMDPointerConversionVector128<TFrom, TTo>
    {
        private static readonly TConversion conversion = new();

        public static bool PerformConversion(TFrom* origin, TTo* target, uint length)
        {
            if (!conversion.IsSupported)
                return false;

            uint size = (uint)Math.Min(Vector128<TFrom>.Count, Vector128<TTo>.Count);

            uint i = 0;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                conversion.PerformIteration(origin, target, i);
            conversion.CopyRemainingElements(origin, target, i, length);

            return true;
        }
    }
    private class SIMDPointerConverterVector128<TFrom, TTo>
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        public static bool PerformConversion(TFrom* origin, TTo* target, uint length)
        {
            if (typeof(TFrom) == typeof(TTo))
                return SameTypeSIMDPointerConverterVector128<TFrom>.PerformConversion(origin, (TFrom*)target, length);

            if (typeof(TFrom) == typeof(double))
            {
                if (typeof(TTo) == typeof(float))
                    return SIMDPointerConverterVector128<double, float, DoubleToSingleConversionVector128>.PerformConversion((double*)origin, (float*)target, length);
                
                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector128<double, int, DoubleToInt32ConversionVector128>.PerformConversion((double*)origin, (int*)target, length);
            }

            if (typeof(TFrom) == typeof(float))
            {
                if (typeof(TTo) == typeof(double))
                    return SIMDPointerConverterVector128<float, double, SingleToDoubleConversionVector128>.PerformConversion((float*)origin, (double*)target, length);

                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector128<float, int, SingleToInt32ConversionVector128>.PerformConversion((float*)origin, (int*)target, length);
            }

            if (typeof(TTo) == typeof(double))
            {
                if (sizeof(TFrom) is sizeof(byte))
                    return SIMDPointerConverterVector128<byte, double, ByteToDoubleConversionVector128>.PerformConversion((byte*)origin, (double*)target, length);
                if (sizeof(TFrom) is sizeof(short))
                    return SIMDPointerConverterVector128<short, double, Int16ToDoubleConversionVector128>.PerformConversion((short*)origin, (double*)target, length);
                if (sizeof(TFrom) is sizeof(int))
                    return SIMDPointerConverterVector128<int, double, Int32ToDoubleConversionVector128>.PerformConversion((int*)origin, (double*)target, length);
            }
            if (typeof(TTo) == typeof(float))
            {
                if (sizeof(TFrom) is sizeof(byte))
                    return SIMDPointerConverterVector128<byte, float, ByteToSingleConversionVector128>.PerformConversion((byte*)origin, (float*)target, length);
                if (sizeof(TFrom) is sizeof(short))
                    return SIMDPointerConverterVector128<short, float, Int16ToSingleConversionVector128>.PerformConversion((short*)origin, (float*)target, length);
                if (sizeof(TFrom) is sizeof(int))
                    return SIMDPointerConverterVector128<int, float, Int32ToSingleConversionVector128>.PerformConversion((int*)origin, (float*)target, length);
            }

            if (sizeof(TFrom) == sizeof(TTo))
                return SameSizeSIMDPointerConverterVector128<TFrom, TTo>.PerformConversion(origin, target, length);

            if (sizeof(TFrom) is sizeof(byte))
            {
                if (sizeof(TTo) is sizeof(short))
                    return SIMDPointerConverterVector128<byte, short, ByteToInt16ConversionVector128>.PerformConversion((byte*)origin, (short*)target, length);
                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector128<byte, int, ByteToInt32ConversionVector128>.PerformConversion((byte*)origin, (int*)target, length);
                if (sizeof(TTo) is sizeof(long))
                    return SIMDPointerConverterVector128<byte, long, ByteToInt64ConversionVector128>.PerformConversion((byte*)origin, (long*)target, length);
            }
            if (sizeof(TFrom) is sizeof(short))
            {
                if (sizeof(TTo) is sizeof(byte))
                    return SIMDPointerConverterVector128<short, byte, Int16ToByteConversionVector128>.PerformConversion((short*)origin, (byte*)target, length);
                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector128<short, int, Int16ToInt32ConversionVector128>.PerformConversion((short*)origin, (int*)target, length);
                if (sizeof(TTo) is sizeof(long))
                    return SIMDPointerConverterVector128<short, long, Int16ToInt64ConversionVector128>.PerformConversion((short*)origin, (long*)target, length);
            }
            if (sizeof(TFrom) is sizeof(int))
            {
                if (sizeof(TTo) is sizeof(byte))
                    return SIMDPointerConverterVector128<int, byte, Int32ToByteConversionVector128>.PerformConversion((int*)origin, (byte*)target, length);
                if (sizeof(TTo) is sizeof(short))
                    return SIMDPointerConverterVector128<int, short, Int32ToInt16ConversionVector128>.PerformConversion((int*)origin, (short*)target, length);
                if (sizeof(TTo) is sizeof(long))
                    return SIMDPointerConverterVector128<int, long, Int32ToInt64ConversionVector128>.PerformConversion((int*)origin, (long*)target, length);
            }
            if (sizeof(TFrom) is sizeof(long))
            {
                if (sizeof(TTo) is sizeof(byte))
                    return SIMDPointerConverterVector128<long, byte, Int64ToByteConversionVector128>.PerformConversion((long*)origin, (byte*)target, length);
                if (sizeof(TTo) is sizeof(short))
                    return SIMDPointerConverterVector128<long, short, Int64ToInt16ConversionVector128>.PerformConversion((long*)origin, (short*)target, length);
                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector128<long, int, Int64ToInt32ConversionVector128>.PerformConversion((long*)origin, (int*)target, length);
            }

            return false;
        }
    }

    private interface ISIMDPointerConversionVector128<TFrom, TTo> : ISIMDPointerConversion<TFrom, TTo>
        where TFrom : unmanaged
        where TTo : unmanaged
    {
    }
    private struct SameSizeConversionVector128<TFrom, TTo> : ISIMDPointerConversionVector128<TFrom, TTo>
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        public bool IsSupported => Sse.IsSupported;

        public void CopyRemainingElements(TFrom* origin, TTo* target, uint index, uint length)
        {
            SIMDIntrinsicsHelper.StoreLastElementsVector128((float*)origin, (float*)target, index, length);
        }
        public void PerformIteration(TFrom* origin, TTo* target, uint index)
        {
            SSEHelper.StoreVector128((float*)origin, (float*)target, index);
        }
    }
    private struct SameTypeConversionVector128<T> : ISIMDPointerConversionVector128<T, T>
        where T : unmanaged
    {
        public bool IsSupported => Sse.IsSupported;

        public void CopyRemainingElements(T* origin, T* target, uint index, uint length)
        {
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(T* origin, T* target, uint index)
        {
            SSEHelper.StoreVector128(origin, target, index);
        }
    }

    #region Byte* -> T*
    private struct ByteToInt16ConversionVector128 : ISIMDPointerConversionVector128<byte, short>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(byte* origin, short* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, short* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    private struct ByteToInt32ConversionVector128 : ISIMDPointerConversionVector128<byte, int>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(byte* origin, int* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, int* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    private struct ByteToInt64ConversionVector128 : ISIMDPointerConversionVector128<byte, long>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(byte* origin, long* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, long* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    private struct ByteToSingleConversionVector128 : ISIMDPointerConversionVector128<byte, float>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(byte* origin, float* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, float* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    private struct ByteToDoubleConversionVector128 : ISIMDPointerConversionVector128<byte, double>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(byte* origin, double* target, uint index, uint length)
        {
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, double* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    #endregion
    #region Int16* -> T*
    private struct Int16ToByteConversionVector128 : ISIMDPointerConversionVector128<short, byte>
    {
        public bool IsSupported => Ssse3.IsSupported;

        public void CopyRemainingElements(short* origin, byte* target, uint index, uint length)
        {
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, index, length);
        }
        public void PerformIteration(short* origin, byte* target, uint index)
        {
            SSSE3Helper.StoreVector64(origin, target, index);
        }
    }
    private struct Int16ToInt32ConversionVector128 : ISIMDPointerConversionVector128<short, int>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(short* origin, int* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(short* origin, int* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    private struct Int16ToInt64ConversionVector128 : ISIMDPointerConversionVector128<short, long>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(short* origin, long* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(short* origin, long* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    private struct Int16ToSingleConversionVector128 : ISIMDPointerConversionVector128<short, float>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(short* origin, float* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(short* origin, float* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    private struct Int16ToDoubleConversionVector128 : ISIMDPointerConversionVector128<short, double>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(short* origin, double* target, uint index, uint length)
        {
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(short* origin, double* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    #endregion
    #region Int32* -> T*
    private struct Int32ToByteConversionVector128 : ISIMDPointerConversionVector128<int, byte>
    {
        public bool IsSupported => Ssse3.IsSupported;

        public void CopyRemainingElements(int* origin, byte* target, uint index, uint length)
        {
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, index, length);
        }
        public void PerformIteration(int* origin, byte* target, uint index)
        {
            SSSE3Helper.StoreVector32(origin, target, index);
        }
    }
    private struct Int32ToInt16ConversionVector128 : ISIMDPointerConversionVector128<int, short>
    {
        public bool IsSupported => Ssse3.IsSupported;

        public void CopyRemainingElements(int* origin, short* target, uint index, uint length)
        {
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, index, length);
        }
        public void PerformIteration(int* origin, short* target, uint index)
        {
            SSSE3Helper.StoreVector64(origin, target, index);
        }
    }
    private struct Int32ToInt64ConversionVector128 : ISIMDPointerConversionVector128<int, long>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(int* origin, long* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(int* origin, long* target, uint index)
        {
            SSE41Helper.StoreVector128(origin, target, index);
        }
    }
    private struct Int32ToSingleConversionVector128 : ISIMDPointerConversionVector128<int, float>
    {
        public bool IsSupported => Sse2.IsSupported;

        public void CopyRemainingElements(int* origin, float* target, uint index, uint length)
        {
            SSE2Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(int* origin, float* target, uint index)
        {
            SSE2Helper.StoreVector128(origin, target, index);
        }
    }
    private struct Int32ToDoubleConversionVector128 : ISIMDPointerConversionVector128<int, double>
    {
        public bool IsSupported => Sse2.IsSupported;

        public void CopyRemainingElements(int* origin, double* target, uint index, uint length)
        {
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(int* origin, double* target, uint index)
        {
            SSE2Helper.StoreVector128(origin, target, index);
        }
    }
    #endregion
    #region Int64* -> T*
    private struct Int64ToByteConversionVector128 : ISIMDPointerConversionVector128<long, byte>
    {
        public bool IsSupported => Ssse3.IsSupported;

        public void CopyRemainingElements(long* origin, byte* target, uint index, uint length)
        {
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, index, length);
        }
        public void PerformIteration(long* origin, byte* target, uint index)
        {
            SSSE3Helper.StoreVector16(origin, target, index);
        }
    }
    private struct Int64ToInt16ConversionVector128 : ISIMDPointerConversionVector128<long, short>
    {
        public bool IsSupported => Ssse3.IsSupported;

        public void CopyRemainingElements(long* origin, short* target, uint index, uint length)
        {
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, index, length);
        }
        public void PerformIteration(long* origin, short* target, uint index)
        {
            SSSE3Helper.StoreVector32(origin, target, index);
        }
    }
    private struct Int64ToInt32ConversionVector128 : ISIMDPointerConversionVector128<long, int>
    {
        public bool IsSupported => Sse41.IsSupported;

        public void CopyRemainingElements(long* origin, int* target, uint index, uint length)
        {
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, index, length);
        }
        public void PerformIteration(long* origin, int* target, uint index)
        {
            SSSE3Helper.StoreVector64(origin, target, index);
        }
    }
    #endregion
    #region Single* -> T*
    private struct SingleToInt32ConversionVector128 : ISIMDPointerConversionVector128<float, int>
    {
        public bool IsSupported => Sse2.IsSupported;

        public void CopyRemainingElements(float* origin, int* target, uint index, uint length)
        {
            SSE2Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(float* origin, int* target, uint index)
        {
            SSE2Helper.StoreVector128(origin, target, index);
        }
    }
    private struct SingleToDoubleConversionVector128 : ISIMDPointerConversionVector128<float, double>
    {
        public bool IsSupported => Sse2.IsSupported;

        public void CopyRemainingElements(float* origin, double* target, uint index, uint length)
        {
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(float* origin, double* target, uint index)
        {
            SSE2Helper.StoreVector128(origin, target, index);
        }
    }
    #endregion
    #region Double* -> T*
    private struct DoubleToInt32ConversionVector128 : ISIMDPointerConversionVector128<double, int>
    {
        public bool IsSupported => Sse2.IsSupported;

        public void CopyRemainingElements(double* origin, int* target, uint index, uint length)
        {
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(double* origin, int* target, uint index)
        {
            SSE2Helper.StoreVector128(origin, target, index);
        }
    }
    private struct DoubleToSingleConversionVector128 : ISIMDPointerConversionVector128<double, float>
    {
        public bool IsSupported => Sse2.IsSupported;

        public void CopyRemainingElements(double* origin, float* target, uint index, uint length)
        {
            SSE2Helper.StoreLastElementsVector128(origin, target, index, length);
        }
        public void PerformIteration(double* origin, float* target, uint index)
        {
            SSE2Helper.StoreVector64(origin, target, index);
        }
    }
    #endregion
    #endregion

    #region Vector256
    private interface ISIMDPointerConversionVector256<TFrom, TTo> : ISIMDPointerConversion<TFrom, TTo>
        where TFrom : unmanaged
        where TTo : unmanaged
    {
    }

    private class SameTypeSIMDPointerConverterVector256<T>
        where T : unmanaged
    {
        public static bool PerformConversion(T* origin, T* target, uint length)
        {
            return SIMDPointerConverterVector256<T, T, SameTypeConversionVector256<T>>.PerformConversion(origin, target, length);
        }
    }
    private class SameSizeSIMDPointerConverterVector256<TFrom, TTo>
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        public static bool PerformConversion(TFrom* origin, TTo* target, uint length)
        {
            return SIMDPointerConverterVector256<TFrom, TTo, SameSizeConversionVector256<TFrom, TTo>>.PerformConversion(origin, target, length);
        }
    }
    private class SIMDPointerConverterVector256<TFrom, TTo, TConversion>
        where TFrom : unmanaged
        where TTo : unmanaged
        where TConversion : struct, ISIMDPointerConversionVector256<TFrom, TTo>
    {
        private static readonly TConversion conversion = new();

        public static bool PerformConversion(TFrom* origin, TTo* target, uint length)
        {
            if (!conversion.IsSupported)
                return false;

            uint size = (uint)Math.Min(Vector256<TFrom>.Count, Vector256<TTo>.Count);

            uint i = 0;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                conversion.PerformIteration(origin, target, i);
            conversion.CopyRemainingElements(origin, target, i, length);

            return true;
        }
    }
    private class SIMDPointerConverterVector256<TFrom, TTo>
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        public static bool PerformConversion(TFrom* origin, TTo* target, uint length)
        {
            if (typeof(TFrom) == typeof(TTo))
                return SameTypeSIMDPointerConverterVector256<TFrom>.PerformConversion(origin, (TFrom*)target, length);

            if (typeof(TFrom) == typeof(double))
            {
                if (typeof(TTo) == typeof(float))
                    return SIMDPointerConverterVector256<double, float, DoubleToSingleConversionVector256>.PerformConversion((double*)origin, (float*)target, length);

                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector256<double, int, DoubleToInt32ConversionVector256>.PerformConversion((double*)origin, (int*)target, length);
            }

            // This is unsupported because downcasting would require interacting with two Vector128, since
            // the instructions are implemented through 2 128-bit lanes, in which case using normal Vector128
            // instructions can be more viable and requires less time to develop while offering the same, if not
            // better, performance
            if (sizeof(TFrom) > sizeof(TTo))
                return false;

            if (typeof(TFrom) == typeof(float))
            {
                if (typeof(TTo) == typeof(double))
                    return SIMDPointerConverterVector256<float, double, SingleToDoubleConversionVector256>.PerformConversion((float*)origin, (double*)target, length);

                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector256<float, int, SingleToInt32ConversionVector256>.PerformConversion((float*)origin, (int*)target, length);
            }

            if (typeof(TTo) == typeof(double))
            {
                if (sizeof(TFrom) is sizeof(byte))
                    return SIMDPointerConverterVector256<byte, double, ByteToDoubleConversionVector256>.PerformConversion((byte*)origin, (double*)target, length);
                if (sizeof(TFrom) is sizeof(short))
                    return SIMDPointerConverterVector256<short, double, Int16ToDoubleConversionVector256>.PerformConversion((short*)origin, (double*)target, length);
                if (sizeof(TFrom) is sizeof(int))
                    return SIMDPointerConverterVector256<int, double, Int32ToDoubleConversionVector256>.PerformConversion((int*)origin, (double*)target, length);
            }
            if (typeof(TTo) == typeof(float))
            {
                if (sizeof(TFrom) is sizeof(byte))
                    return SIMDPointerConverterVector256<byte, float, ByteToSingleConversionVector256>.PerformConversion((byte*)origin, (float*)target, length);
                if (sizeof(TFrom) is sizeof(short))
                    return SIMDPointerConverterVector256<short, float, Int16ToSingleConversionVector256>.PerformConversion((short*)origin, (float*)target, length);
                if (sizeof(TFrom) is sizeof(int))
                    return SIMDPointerConverterVector256<int, float, Int32ToSingleConversionVector256>.PerformConversion((int*)origin, (float*)target, length);
            }

            if (sizeof(TFrom) == sizeof(TTo))
                return SameSizeSIMDPointerConverterVector256<TFrom, TTo>.PerformConversion(origin, target, length);

            if (sizeof(TFrom) is sizeof(byte))
            {
                if (sizeof(TTo) is sizeof(short))
                    return SIMDPointerConverterVector256<byte, short, ByteToInt16ConversionVector256>.PerformConversion((byte*)origin, (short*)target, length);
                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector256<byte, int, ByteToInt32ConversionVector256>.PerformConversion((byte*)origin, (int*)target, length);
                if (sizeof(TTo) is sizeof(long))
                    return SIMDPointerConverterVector256<byte, long, ByteToInt64ConversionVector256>.PerformConversion((byte*)origin, (long*)target, length);
            }
            if (sizeof(TFrom) is sizeof(short))
            {
                if (sizeof(TTo) is sizeof(int))
                    return SIMDPointerConverterVector256<short, int, Int16ToInt32ConversionVector256>.PerformConversion((short*)origin, (int*)target, length);
                if (sizeof(TTo) is sizeof(long))
                    return SIMDPointerConverterVector256<short, long, Int16ToInt64ConversionVector256>.PerformConversion((short*)origin, (long*)target, length);
            }
            if (sizeof(TFrom) is sizeof(int))
            {
                if (sizeof(TTo) is sizeof(long))
                    return SIMDPointerConverterVector256<int, long, Int32ToInt64ConversionVector256>.PerformConversion((int*)origin, (long*)target, length);
            }

            return false;
        }
    }

    private struct SameSizeConversionVector256<TFrom, TTo> : ISIMDPointerConversionVector256<TFrom, TTo>
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        public bool IsSupported => Sse.IsSupported;

        public void CopyRemainingElements(TFrom* origin, TTo* target, uint index, uint length)
        {
            AVXHelper.StoreLastElementsVector256((float*)origin, (float*)target, index, length);
        }
        public void PerformIteration(TFrom* origin, TTo* target, uint index)
        {
            AVXHelper.StoreVector256((float*)origin, (float*)target, index);
        }
    }
    private struct SameTypeConversionVector256<T> : ISIMDPointerConversionVector256<T, T>
        where T : unmanaged
    {
        public bool IsSupported => Sse.IsSupported;

        public void CopyRemainingElements(T* origin, T* target, uint index, uint length)
        {
            AVXHelper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(T* origin, T* target, uint index)
        {
            AVXHelper.StoreVector256(origin, target, index);
        }
    }

    #region Byte* -> T*
    private struct ByteToInt16ConversionVector256 : ISIMDPointerConversionVector256<byte, short>
    {
        public bool IsSupported => Avx2.IsSupported;

        public void CopyRemainingElements(byte* origin, short* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, short* target, uint index)
        {
            AVX2Helper.StoreVector256(origin, target, index);
        }
    }
    private struct ByteToInt32ConversionVector256 : ISIMDPointerConversionVector256<byte, int>
    {
        public bool IsSupported => Avx2.IsSupported;

        public void CopyRemainingElements(byte* origin, int* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, int* target, uint index)
        {
            AVX2Helper.StoreVector256(origin, target, index);
        }
    }
    private struct ByteToInt64ConversionVector256 : ISIMDPointerConversionVector256<byte, long>
    {
        public bool IsSupported => Avx2.IsSupported;

        public void CopyRemainingElements(byte* origin, long* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, long* target, uint index)
        {
            AVX2Helper.StoreVector256(origin, target, index);
        }
    }
    private struct ByteToSingleConversionVector256 : ISIMDPointerConversionVector256<byte, float>
    {
        public bool IsSupported => Avx2.IsSupported;

        public void CopyRemainingElements(byte* origin, float* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, float* target, uint index)
        {
            AVX2Helper.StoreVector256(origin, target, index);
        }
    }
    private struct ByteToDoubleConversionVector256 : ISIMDPointerConversionVector256<byte, double>
    {
        public bool IsSupported => Avx.IsSupported;

        public void CopyRemainingElements(byte* origin, double* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(byte* origin, double* target, uint index)
        {
            AVXHelper.StoreVector256(origin, target, index);
        }
    }
    #endregion
    #region Int16* -> T*
    private struct Int16ToInt32ConversionVector256 : ISIMDPointerConversionVector256<short, int>
    {
        public bool IsSupported => Avx2.IsSupported;

        public void CopyRemainingElements(short* origin, int* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(short* origin, int* target, uint index)
        {
            AVX2Helper.StoreVector256(origin, target, index);
        }
    }
    private struct Int16ToInt64ConversionVector256 : ISIMDPointerConversionVector256<short, long>
    {
        public bool IsSupported => Avx2.IsSupported;

        public void CopyRemainingElements(short* origin, long* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(short* origin, long* target, uint index)
        {
            AVX2Helper.StoreVector256(origin, target, index);
        }
    }
    private struct Int16ToSingleConversionVector256 : ISIMDPointerConversionVector256<short, float>
    {
        public bool IsSupported => Avx2.IsSupported;

        public void CopyRemainingElements(short* origin, float* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(short* origin, float* target, uint index)
        {
            AVX2Helper.StoreVector256(origin, target, index);
        }
    }
    private struct Int16ToDoubleConversionVector256 : ISIMDPointerConversionVector256<short, double>
    {
        public bool IsSupported => Avx.IsSupported;

        public void CopyRemainingElements(short* origin, double* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(short* origin, double* target, uint index)
        {
            AVXHelper.StoreVector256(origin, target, index);
        }
    }
    #endregion
    #region Int32* -> T*
    private struct Int32ToInt64ConversionVector256 : ISIMDPointerConversionVector256<int, long>
    {
        public bool IsSupported => Avx2.IsSupported;

        public void CopyRemainingElements(int* origin, long* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(int* origin, long* target, uint index)
        {
            AVX2Helper.StoreVector256(origin, target, index);
        }
    }
    private struct Int32ToSingleConversionVector256 : ISIMDPointerConversionVector256<int, float>
    {
        public bool IsSupported => Avx.IsSupported;

        public void CopyRemainingElements(int* origin, float* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(int* origin, float* target, uint index)
        {
            AVXHelper.StoreVector256(origin, target, index);
        }
    }
    private struct Int32ToDoubleConversionVector256 : ISIMDPointerConversionVector256<int, double>
    {
        public bool IsSupported => Avx.IsSupported;

        public void CopyRemainingElements(int* origin, double* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(int* origin, double* target, uint index)
        {
            AVXHelper.StoreVector256(origin, target, index);
        }
    }
    #endregion
    #region Single* -> T*
    private struct SingleToInt32ConversionVector256 : ISIMDPointerConversionVector256<float, int>
    {
        public bool IsSupported => Avx.IsSupported;

        public void CopyRemainingElements(float* origin, int* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(float* origin, int* target, uint index)
        {
            AVXHelper.StoreVector256(origin, target, index);
        }
    }
    private struct SingleToDoubleConversionVector256 : ISIMDPointerConversionVector256<float, double>
    {
        public bool IsSupported => Avx.IsSupported;

        public void CopyRemainingElements(float* origin, double* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(float* origin, double* target, uint index)
        {
            AVXHelper.StoreVector256(origin, target, index);
        }
    }
    #endregion
    #region Double* -> T*
    private struct DoubleToInt32ConversionVector256 : ISIMDPointerConversionVector256<double, int>
    {
        public bool IsSupported => Avx.IsSupported;

        public void CopyRemainingElements(double* origin, int* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(double* origin, int* target, uint index)
        {
            AVXHelper.StoreVector256(origin, target, index);
        }
    }
    private struct DoubleToSingleConversionVector256 : ISIMDPointerConversionVector256<double, float>
    {
        public bool IsSupported => Avx.IsSupported;

        public void CopyRemainingElements(double* origin, float* target, uint index, uint length)
        {
            SSE41Helper.StoreLastElementsVector256(origin, target, index, length);
        }
        public void PerformIteration(double* origin, float* target, uint index)
        {
            AVXHelper.StoreVector128(origin, target, index);
        }
    }
    #endregion
    #endregion

    public static bool CopyToArrayVector128<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        return SIMDPointerConverterVector128<TFrom, TTo>.PerformConversion(origin, target, length);
    }
    public static bool CopyToArrayVector256<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        return SIMDPointerConverterVector256<TFrom, TTo>.PerformConversion(origin, target, length);
    }
}

#endif
