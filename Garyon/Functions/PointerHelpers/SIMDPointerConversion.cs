using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Garyon.Functions.IntrinsicsHelpers;

namespace Garyon.Functions.PointerHelpers
{
    /// <summary>Contains unsafe helper functions for array copying using SIMD. All functions check whether the minimum supported instruction set is included; in the case that the set is unavailable, the functions simply do nothing.</summary>
    public static unsafe class SIMDPointerConversion
    {
        // Copying from T1* to T2* where sizeof(T1) > sizeof(T2) is not supported with Vector256, consider using once new intrinsics support this feature more optimally
        #region Vector256
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToArrayVector256<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            // This is unsupported because downcasting would require interacting with two Vector128s, since
            // the instructions are implemented through 2 128-bit lanes, in which case using normal Vector128
            // instructions can be more viable and requires less time to develop while offering the same, if not
            // better, performance
            if (sizeof(TFrom) > sizeof(TTo))
                return false;

            if (typeof(TFrom) == typeof(TTo))
                return CopyToArrayVector256<TFrom>(origin, (TFrom*)target, length);

            if (typeof(TTo) == typeof(float))
            {
                if (typeof(TFrom) == typeof(double))
                    return CopyToSingleArrayVector256((double*)origin, (float*)target, length);

                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToSingleArrayVector256((byte*)origin, (float*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToSingleArrayVector256((short*)origin, (float*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToSingleArrayVector256((int*)origin, (float*)target, length);

                return false;
            }
            if (typeof(TTo) == typeof(double))
            {
                if (typeof(TFrom) == typeof(float))
                    return CopyToDoubleArrayVector256((float*)origin, (double*)target, length);

                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToDoubleArrayVector256((byte*)origin, (double*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToDoubleArrayVector256((short*)origin, (double*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToDoubleArrayVector256((int*)origin, (double*)target, length);

                return false;
            }

            if (sizeof(TTo) == sizeof(short))
                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToInt16ArrayVector256((byte*)origin, (short*)target, length);
            if (sizeof(TTo) == sizeof(int))
            {
                if (typeof(TFrom) == typeof(float))
                    return CopyToInt32ArrayVector256((float*)origin, (int*)target, length);
                if (typeof(TFrom) == typeof(double))
                    return CopyToInt32ArrayVector256((double*)origin, (int*)target, length);

                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToInt32ArrayVector256((byte*)origin, (int*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToInt32ArrayVector256((short*)origin, (int*)target, length);
            }
            if (sizeof(TTo) == sizeof(long))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToInt64ArrayVector256((byte*)origin, (long*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToInt64ArrayVector256((short*)origin, (long*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToInt64ArrayVector256((int*)origin, (long*)target, length);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToArrayVector256<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (CopyToArrayVector256<T, long>(origin, target, length))
                return true;
            if (CopyToArrayVector256<T, int>(origin, target, length))
                return true;
            if (CopyToArrayVector256<T, short>(origin, target, length))
                return true;
            return CopyToArrayVector256<T, byte>(origin, target, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CopyToArrayVector256<TPointer, TReinterpret>(TPointer* origin, TPointer* target, uint length)
            where TPointer : unmanaged
            where TReinterpret : unmanaged
        {
            if (sizeof(TPointer) % sizeof(TReinterpret) == 0)
                return CopyToReinterpretedArrayVector256((TReinterpret*)origin, (TReinterpret*)target, length * (uint)sizeof(TPointer) / (uint)sizeof(TReinterpret));
            return false;
        }
        private static bool CopyToReinterpretedArrayVector256<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(long))
                return CopyToInt64ArrayVector256((long*)origin, (long*)target, length);
            if (sizeof(T) == sizeof(int))
                return CopyToInt32ArrayVector256((int*)origin, (int*)target, length);
            if (sizeof(T) == sizeof(short))
                return CopyToInt16ArrayVector256((short*)origin, (short*)target, length);
            if (sizeof(T) == sizeof(byte))
                return CopyToByteArrayVector256((byte*)origin, (byte*)target, length);
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToArrayVector256Generic<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<TFrom, TTo>())
                return false;

            uint size = (uint)Math.Min(Vector256<TFrom>.Count, Vector256<TTo>.Count);

            uint i = 0;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                PerformCurrentConversionIterationVector256(origin, target, i, length);
            ConvertLastElementsVector256(origin, target, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetSupportedInstructionSetVector256<TFrom, TTo>()
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (typeof(TFrom) == typeof(TTo))
                return Avx.IsSupported;

            if (typeof(TTo) == typeof(float))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return Avx2.IsSupported;
                if (sizeof(TFrom) == sizeof(short))
                    return Avx2.IsSupported;
                if (sizeof(TFrom) == sizeof(int))
                    return Avx.IsSupported;

                return false;
            }
            if (typeof(TTo) == typeof(double))
            {
                if (typeof(TFrom) == typeof(float))
                    return Avx.IsSupported;

                if (sizeof(TFrom) == sizeof(byte))
                    return Avx.IsSupported;
                if (sizeof(TFrom) == sizeof(short))
                    return Avx.IsSupported;
                if (sizeof(TFrom) == sizeof(int))
                    return Avx.IsSupported;

                return false;
            }

            if (typeof(TTo) == typeof(byte))
                return Avx.IsSupported;
            if (sizeof(TTo) == sizeof(short))
                if (sizeof(TFrom) == sizeof(byte))
                    return Avx2.IsSupported;
            if (sizeof(TTo) == sizeof(int))
            {
                if (typeof(TFrom) == typeof(float))
                    return Avx.IsSupported;
                if (typeof(TFrom) == typeof(double))
                    return Avx.IsSupported;

                if (sizeof(TFrom) == sizeof(byte))
                    return Avx2.IsSupported;
                if (sizeof(TFrom) == sizeof(short))
                    return Avx2.IsSupported;
            }
            if (sizeof(TTo) == sizeof(long))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return Avx2.IsSupported;
                if (sizeof(TFrom) == sizeof(short))
                    return Avx2.IsSupported;
                if (sizeof(TFrom) == sizeof(int))
                    return Avx2.IsSupported;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentConversionIterationVector256<TFrom, TTo>(TFrom* origin, TTo* target, uint index, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            // Directly copy their bytes, allowing custom unmanaged structs to be copied through this method
            if (typeof(TFrom) == typeof(TTo))
                AVXHelper.StoreVector256(origin, (TFrom*)target, index);
            else if (typeof(TTo) == typeof(float))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    AVX2Helper.StoreVector256((byte*)origin, (float*)target, index);
                if (sizeof(TFrom) == sizeof(short))
                    AVX2Helper.StoreVector256((short*)origin, (float*)target, index);
                if (sizeof(TFrom) == sizeof(int))
                    AVXHelper.StoreVector256((int*)origin, (float*)target, index);
            }
            else if (typeof(TTo) == typeof(double))
                if (typeof(TFrom) == typeof(float))
                    AVXHelper.StoreVector256((float*)origin, (double*)target, index);
                else
                {
                    if (sizeof(TFrom) == sizeof(byte))
                        AVXHelper.StoreVector256((byte*)origin, (double*)target, index);
                    if (sizeof(TFrom) == sizeof(short))
                        AVXHelper.StoreVector256((short*)origin, (double*)target, index);
                    if (sizeof(TFrom) == sizeof(int))
                        AVXHelper.StoreVector256((int*)origin, (double*)target, index);
                }
            else
            {
                if (sizeof(TFrom) == sizeof(TTo))
                    AVXHelper.StoreVector256(origin, (TFrom*)target, index);
                if (sizeof(TTo) == sizeof(short))
                    if (sizeof(TFrom) == sizeof(byte))
                        AVX2Helper.StoreVector256((byte*)origin, (short*)target, index);
                if (sizeof(TTo) == sizeof(int))
                    if (typeof(TFrom) == typeof(float))
                        AVXHelper.StoreVector256((float*)origin, (int*)target, index);
                    else if (typeof(TFrom) == typeof(double))
                        AVXHelper.StoreVector256((double*)origin, (int*)target, index);
                    else
                    {
                        if (sizeof(TFrom) == sizeof(byte))
                            AVX2Helper.StoreVector256((byte*)origin, (int*)target, index);
                        if (sizeof(TFrom) == sizeof(short))
                            AVX2Helper.StoreVector256((short*)origin, (int*)target, index);
                    }
                if (sizeof(TTo) == sizeof(long))
                {
                    if (sizeof(TFrom) == sizeof(byte))
                        AVX2Helper.StoreVector256((byte*)origin, (long*)target, index);
                    if (sizeof(TFrom) == sizeof(short))
                        AVX2Helper.StoreVector256((short*)origin, (long*)target, index);
                    if (sizeof(TFrom) == sizeof(int))
                        AVX2Helper.StoreVector256((int*)origin, (long*)target, index);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertLastElementsVector256<TFrom, TTo>(TFrom* origin, TTo* target, uint index, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            // Directly copy their bytes, allowing custom unmanaged structs to be copied through this method
            if (typeof(TFrom) == typeof(TTo))
                SSE2Helper.StoreLastElementsVector256(origin, (TFrom*)target, index, length);
            else if (typeof(TTo) == typeof(float))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    SSE41Helper.StoreLastElementsVector256((byte*)origin, (float*)target, index, length);
                if (sizeof(TFrom) == sizeof(short))
                    SSE41Helper.StoreLastElementsVector256((short*)origin, (float*)target, index, length);
                if (sizeof(TFrom) == sizeof(int))
                    SSE2Helper.StoreLastElementsVector256((int*)origin, (float*)target, index, length);
            }
            else if (typeof(TTo) == typeof(double))
            {
                if (typeof(TFrom) == typeof(float))
                    SSE41Helper.StoreLastElementsVector256((float*)origin, (double*)target, index, length);
                else
                {
                    if (sizeof(TFrom) == sizeof(byte))
                        SSE41Helper.StoreLastElementsVector256((byte*)origin, (double*)target, index, length);
                    if (sizeof(TFrom) == sizeof(short))
                        SSE41Helper.StoreLastElementsVector256((short*)origin, (double*)target, index, length);
                    if (sizeof(TFrom) == sizeof(int))
                        SSE41Helper.StoreLastElementsVector256((int*)origin, (double*)target, index, length);
                }
            }
            else
            {
                if (sizeof(TFrom) == sizeof(TTo))
                    SSE2Helper.StoreLastElementsVector256(origin, (TFrom*)target, index, length);
                if (sizeof(TTo) == sizeof(short))
                    if (sizeof(TFrom) == sizeof(byte))
                        SSE41Helper.StoreLastElementsVector256((byte*)origin, (short*)target, index, length);
                if (sizeof(TTo) == sizeof(int))
                {
                    if (typeof(TFrom) == typeof(float))
                        SSE2Helper.StoreLastElementsVector256((float*)origin, (int*)target, index, length);
                    else if (typeof(TFrom) == typeof(double))
                        SSE41Helper.StoreLastElementsVector256((double*)origin, (int*)target, index, length);
                    else
                    {
                        if (sizeof(TFrom) == sizeof(byte))
                            SSE41Helper.StoreLastElementsVector256((byte*)origin, (int*)target, index, length);
                        if (sizeof(TFrom) == sizeof(short))
                            SSE41Helper.StoreLastElementsVector256((short*)origin, (int*)target, index, length);
                    }
                }
                if (sizeof(TTo) == sizeof(long))
                {
                    if (sizeof(TFrom) == sizeof(byte))
                        SSE41Helper.StoreLastElementsVector256((byte*)origin, (long*)target, index, length);
                    if (sizeof(TFrom) == sizeof(short))
                        SSE41Helper.StoreLastElementsVector256((short*)origin, (long*)target, index, length);
                    if (sizeof(TFrom) == sizeof(int))
                        SSE41Helper.StoreLastElementsVector256((int*)origin, (long*)target, index, length);
                }
            }
        }

        #region T* -> long*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt64ArrayVector256(byte* origin, long* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt64ArrayVector256(short* origin, long* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt64ArrayVector256(int* origin, long* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt64ArrayVector256(long* origin, long* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> int*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt32ArrayVector256(byte* origin, int* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt32ArrayVector256(short* origin, int* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt32ArrayVector256(int* origin, int* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="float"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt32ArrayVector256(float* origin, int* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="double"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt32ArrayVector256(double* origin, int* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> short*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt16ArrayVector256(byte* origin, short* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<short>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt16ArrayVector256(short* origin, short* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<short>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> byte*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToByteArrayVector256(byte* origin, byte* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<byte>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE2Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> float*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="float"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToSingleArrayVector256(byte* origin, float* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<float>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="float"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToSingleArrayVector256(short* origin, float* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<float>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="float"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToSingleArrayVector256(int* origin, float* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<float>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>* into a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="double"/> sequence.</param>
        /// <param name="target">The target <seealso cref="float"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToSingleArrayVector256(double* origin, float* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> double*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="double"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToDoubleArrayVector256(byte* origin, double* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>*. Minimum required instruction set: AVX2.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="double"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToDoubleArrayVector256(short* origin, double* target, uint length)
        {
            if (!Avx2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="double"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToDoubleArrayVector256(int* origin, double* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>* into a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>*. Minimum required instruction set: AVX.</summary>
        /// <param name="origin">The origin <seealso cref="float"/> sequence.</param>
        /// <param name="target">The target <seealso cref="double"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToDoubleArrayVector256(float* origin, double* target, uint length)
        {
            if (!Avx.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            SSE41Helper.StoreLastElementsVector256(origin, target, i, length);

            return true;
        }
        #endregion
        #endregion

        #region Vector128
        /// <summary>Copies the elements of a sequence passed as a pointer into another sequence passed as a pointer using Vector128 SIMD instructions. If both types are the same, a direct copy of their bytes will be used to allow custom structs to be copied.</summary>
        /// <typeparam name="TFrom">The type of the elements in the original sequence.</typeparam>
        /// <typeparam name="TTo">The type of the elements in the target sequence.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="target">The target sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToArrayVector128<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            // Directly copy their bytes, allowing custom unmanaged structs to be copied through this method
            // Attempt to optimize few CPU cycles

            // TODO: Remove all these functions after having migrated to generic function (hopefully be inlined)
            if (typeof(TFrom) == typeof(TTo))
                return CopyToArrayVector128<TFrom>(origin, (TFrom*)target, length);

            if (typeof(TTo) == typeof(float))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToSingleArrayVector128((byte*)origin, (float*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToSingleArrayVector128((short*)origin, (float*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToSingleArrayVector128((int*)origin, (float*)target, length);

                return false;
            }
            if (typeof(TTo) == typeof(double))
            {
                if (typeof(TFrom) == typeof(float))
                    return CopyToDoubleArrayVector128((float*)origin, (double*)target, length);

                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToDoubleArrayVector128((byte*)origin, (double*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToDoubleArrayVector128((short*)origin, (double*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToDoubleArrayVector128((int*)origin, (double*)target, length);

                return false;
            }

            if (sizeof(TTo) == sizeof(byte))
            {
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToByteArrayVector128((short*)origin, (byte*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToByteArrayVector128((int*)origin, (byte*)target, length);
                if (sizeof(TFrom) == sizeof(long))
                    return CopyToByteArrayVector128((long*)origin, (byte*)target, length);
            }
            if (sizeof(TTo) == sizeof(short))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToInt16ArrayVector128((byte*)origin, (short*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToInt16ArrayVector128((int*)origin, (short*)target, length);
                if (sizeof(TFrom) == sizeof(long))
                    return CopyToInt16ArrayVector128((long*)origin, (short*)target, length);
            }
            if (sizeof(TTo) == sizeof(int))
            {
                if (typeof(TFrom) == typeof(float))
                    return CopyToInt32ArrayVector128((float*)origin, (int*)target, length);
                if (typeof(TFrom) == typeof(double))
                    return CopyToInt32ArrayVector128((double*)origin, (int*)target, length);

                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToInt32ArrayVector128((byte*)origin, (int*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToInt32ArrayVector128((short*)origin, (int*)target, length);
                if (sizeof(TFrom) == sizeof(long))
                    return CopyToInt32ArrayVector128((long*)origin, (int*)target, length);
            }
            if (sizeof(TTo) == sizeof(long))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToInt64ArrayVector128((byte*)origin, (long*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToInt64ArrayVector128((short*)origin, (long*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToInt64ArrayVector128((int*)origin, (long*)target, length);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToArrayVector128<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (CopyToArrayVector128<T, long>(origin, target, length))
                return true;
            if (CopyToArrayVector128<T, int>(origin, target, length))
                return true;
            if (CopyToArrayVector128<T, short>(origin, target, length))
                return true;
            return CopyToArrayVector128<T, byte>(origin, target, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CopyToArrayVector128<TPointer, TReinterpret>(TPointer* origin, TPointer* target, uint length)
            where TPointer : unmanaged
            where TReinterpret : unmanaged
        {
            if (sizeof(TPointer) % sizeof(TReinterpret) == 0)
                return CopyToReinterpretedArrayVector128((TReinterpret*)origin, (TReinterpret*)target, length * (uint)sizeof(TPointer) / (uint)sizeof(TReinterpret));
            return false;
        }
        private static bool CopyToReinterpretedArrayVector128<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(long))
                return CopyToInt64ArrayVector128((long*)origin, (long*)target, length);
            if (sizeof(T) == sizeof(int))
                return CopyToInt32ArrayVector128((int*)origin, (int*)target, length);
            if (sizeof(T) == sizeof(short))
                return CopyToInt16ArrayVector128((short*)origin, (short*)target, length);
            if (sizeof(T) == sizeof(byte))
                return CopyToByteArrayVector128((byte*)origin, (byte*)target, length);
            return false;
        }

        #region T* -> double*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="double"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToDoubleArrayVector128(byte* origin, double* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="double"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToDoubleArrayVector128(short* origin, double* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="double"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToDoubleArrayVector128(int* origin, double* target, uint length)
        {
            if (!Sse2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>* into a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="float"/> sequence.</param>
        /// <param name="target">The target <seealso cref="double"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToDoubleArrayVector128(float* origin, double* target, uint length)
        {
            if (!Sse2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> long*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt64ArrayVector128(byte* origin, long* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt64ArrayVector128(short* origin, long* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt64ArrayVector128(int* origin, long* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: SSE.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt64ArrayVector128(long* origin, long* target, uint length)
        {
            if (!Sse.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSEHelper.StoreVector128(origin, target, i);
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> float*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="float"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToSingleArrayVector128(byte* origin, float* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<float>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="float"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToSingleArrayVector128(short* origin, float* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<float>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="float"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToSingleArrayVector128(int* origin, float* target, uint length)
        {
            if (!Sse2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<float>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
            SSE2Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>* into a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="double"/> sequence.</param>
        /// <param name="target">The target <seealso cref="float"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToSingleArrayVector128(double* origin, float* target, uint length)
        {
            if (!Sse2.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE2Helper.StoreVector64(origin, target, i);
            SSE2Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> int*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt32ArrayVector128(byte* origin, int* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt32ArrayVector128(short* origin, int* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSE.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt32ArrayVector128(int* origin, int* target, uint length)
        {
            if (!Sse.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSEHelper.StoreVector128(origin, target, i);
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="float"/> sequence passed as a <seealso cref="float"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="float"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt32ArrayVector128(float* origin, int* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<float>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt32ArrayVector128(long* origin, int* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSSE3Helper.StoreVector64(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="double"/> sequence passed as a <seealso cref="double"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="double"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt32ArrayVector128(double* origin, int* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<double>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> short*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt16ArrayVector128(byte* origin, short* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<short>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: SSE.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt16ArrayVector128(short* origin, short* target, uint length)
        {
            if (!Sse.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<short>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSEHelper.StoreVector128(origin, target, i);
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt16ArrayVector128(int* origin, short* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSSE3Helper.StoreVector64(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToInt16ArrayVector128(long* origin, short* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSSE3Helper.StoreVector32(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> byte*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: SSE.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToByteArrayVector128(byte* origin, byte* target, uint length)
        {
            if (!Sse.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<byte>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSEHelper.StoreVector128(origin, target, i);
            SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToByteArrayVector128(short* origin, byte* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<short>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSSE3Helper.StoreVector64(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToByteArrayVector128(int* origin, byte* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<int>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSSE3Helper.StoreVector32(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToByteArrayVector128(long* origin, byte* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<long>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSSE3Helper.StoreVector16(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        #endregion

        public static bool CopyToArrayVector128Generic<TFrom, TTo>(TFrom* origin, TTo* target, uint index, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            PointerArithmetic.Increment(ref origin, ref target, index);
            return CopyToArrayVector128Generic(origin, target, length);
        }
        public static bool CopyToArrayVector128Generic<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<TFrom, TTo>())
                return false;

            uint size = (uint)Math.Min(Vector128<TFrom>.Count, Vector128<TTo>.Count);

            uint i = 0;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                PerformCurrentConversionIterationVector128(origin, target, i);
            ConvertLastElementsVector128(origin, target, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetSupportedInstructionSetVector128<TFrom, TTo>()
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (typeof(TFrom) == typeof(TTo))
                return Sse.IsSupported;

            if (typeof(TTo) == typeof(float))
                return GetSupportToSingle();
            if (typeof(TTo) == typeof(double))
                return GetSupportToDouble();

            if (sizeof(TFrom) == sizeof(TTo))
                return Sse.IsSupported;
            if (sizeof(TTo) == sizeof(byte))
                return GetSupportToByte();
            if (sizeof(TTo) == sizeof(short))
                return GetSupportToInt16();
            if (sizeof(TTo) == sizeof(int))
                return GetSupportToInt32();
            if (sizeof(TTo) == sizeof(long))
                return GetSupportToInt64();

            return false;

            // TODO: Remove to outer parts for consistency?
            static bool GetSupportToSingle()
            {
                if (typeof(TFrom) == typeof(double))
                    return Sse2.IsSupported;

                if (sizeof(TFrom) == sizeof(byte))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(short))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(int))
                    return Sse2.IsSupported;

                return false;
            }
            static bool GetSupportToDouble()
            {
                if (typeof(TFrom) == typeof(float))
                    return Sse2.IsSupported;

                if (sizeof(TFrom) == sizeof(byte))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(short))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(int))
                    return Sse41.IsSupported;

                return false;
            }
            static bool GetSupportToByte()
            {
                if (sizeof(TFrom) == sizeof(short))
                    return Ssse3.IsSupported;
                if (sizeof(TFrom) == sizeof(int))
                    return Ssse3.IsSupported;
                if (sizeof(TFrom) == sizeof(long))
                    return Ssse3.IsSupported;

                return false;
            }
            static bool GetSupportToInt16()
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(int))
                    return Ssse3.IsSupported;
                if (sizeof(TFrom) == sizeof(long))
                    return Ssse3.IsSupported;

                return false;
            }
            static bool GetSupportToInt32()
            {
                if (typeof(TFrom) == typeof(float))
                    return Sse2.IsSupported;
                if (typeof(TFrom) == typeof(double))
                    return Sse2.IsSupported;

                if (sizeof(TFrom) == sizeof(byte))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(short))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(long))
                    return Ssse3.IsSupported;

                return false;
            }
            static bool GetSupportToInt64()
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(short))
                    return Sse41.IsSupported;
                if (sizeof(TFrom) == sizeof(int))
                    return Sse41.IsSupported;

                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentConversionIterationVector128<TFrom, TTo>(TFrom* origin, TTo* target, uint index)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            // Directly copy their bytes, allowing custom unmanaged structs to be copied through this method
            if (typeof(TFrom) == typeof(TTo))
                SSEHelper.StoreVector128(origin, (TFrom*)target, index);
            else if (typeof(TTo) == typeof(float))
                ConvertToSingle(origin, (float*)target, index);
            else if (typeof(TTo) == typeof(double))
                ConvertToDouble(origin, (double*)target, index);
            else
            {
                // Controversial functionality; consider providing that feature through the following design:
                /*  
                 *  Permitting all types of the same size (int -> T, uint -> T, float(?) -> T, vice versable)
                 *  interface IUnsafelyConvertibleFrom
                 *  interface IUnsafelyConvertibleTo
                 *  interface IUnsafelyConvertible : IUnsafelyConvertibleFrom, IUnsafelyConvertibleTo
                 *  
                 *  Permitting only select types of the same size
                 *  interface IUnsafelyConvertibleFrom<TFrom>
                 *  interface IUnsafelyConvertibleTo<TTo>
                 *  interface IUnsafelyConvertible<T> : IUnsafelyConvertibleFrom<T>, IUnsafelyConvertibleTo<T>
                 */
                // Useful for cases where:
                // - a custom struct wraps a single predefined struct (a single int for instance)
                // - two custom structs have the exact same memory layout (float, int in both structs)

                //if (sizeof(TFrom) == sizeof(TTo))
                //    SSEHelper.StoreVector128(origin, (TFrom*)target, index);

                if (sizeof(TTo) == sizeof(byte))
                    ConvertToByte(origin, (byte*)target, index);
                else if (sizeof(TTo) == sizeof(short))
                    ConvertToInt16(origin, (short*)target, index);
                else if (sizeof(TTo) == sizeof(int))
                    ConvertToInt32(origin, (int*)target, index);
                else if (sizeof(TTo) == sizeof(long))
                    ConvertToInt64(origin, (long*)target, index);
            }
        }

        #region PerformCurrentConversionIterationVector128 Parts
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertToSingle<TFrom>(TFrom* origin, float* target, uint index)
            where TFrom : unmanaged
        {
            if (typeof(TFrom) == typeof(double))
                SSE2Helper.StoreVector64((double*)origin, target, index);
            else
            {
                if (sizeof(TFrom) == sizeof(byte))
                    SSE41Helper.StoreVector128((byte*)origin, target, index);
                else if (sizeof(TFrom) == sizeof(short))
                    SSE41Helper.StoreVector128((short*)origin, target, index);
                else if (sizeof(TFrom) == sizeof(int))
                    SSE2Helper.StoreVector128((int*)origin, target, index);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertToDouble<TFrom>(TFrom* origin, double* target, uint index)
            where TFrom : unmanaged
        {
            if (typeof(TFrom) == typeof(float))
                SSE2Helper.StoreVector128((float*)origin, target, index);
            else
            {
                if (sizeof(TFrom) == sizeof(byte))
                    SSE41Helper.StoreVector128((byte*)origin, target, index);
                else if (sizeof(TFrom) == sizeof(short))
                    SSE41Helper.StoreVector128((short*)origin, target, index);
                else if (sizeof(TFrom) == sizeof(int))
                    SSE2Helper.StoreVector128((int*)origin, target, index);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertToByte<TFrom>(TFrom* origin, byte* target, uint index)
            where TFrom : unmanaged
        {
            if (sizeof(TFrom) == sizeof(short))
                SSSE3Helper.StoreVector64((short*)origin, target, index);
            else if (sizeof(TFrom) == sizeof(int))
                SSSE3Helper.StoreVector32((int*)origin, target, index);
            else if (sizeof(TFrom) == sizeof(long))
                SSSE3Helper.StoreVector16((long*)origin, target, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertToInt16<TFrom>(TFrom* origin, short* target, uint index)
            where TFrom : unmanaged
        {
            if (sizeof(TFrom) == sizeof(byte))
                SSE41Helper.StoreVector128((byte*)origin, target, index);
            else if (sizeof(TFrom) == sizeof(int))
                SSSE3Helper.StoreVector64((int*)origin, target, index);
            else if (sizeof(TFrom) == sizeof(long))
                SSSE3Helper.StoreVector32((long*)origin, target, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertToInt32<TFrom>(TFrom* origin, int* target, uint index)
            where TFrom : unmanaged
        {
            if (typeof(TFrom) == typeof(float))
                SSE2Helper.StoreVector128((float*)origin, target, index);
            else if (typeof(TFrom) == typeof(double))
                SSE2Helper.StoreVector128((double*)origin, target, index);
            else
            {
                if (sizeof(TFrom) == sizeof(byte))
                    SSE41Helper.StoreVector128((byte*)origin, target, index);
                else if (sizeof(TFrom) == sizeof(short))
                    SSE41Helper.StoreVector128((short*)origin, target, index);
                else if (sizeof(TFrom) == sizeof(long))
                    SSSE3Helper.StoreVector64((long*)origin, target, index);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertToInt64<TFrom>(TFrom* origin, long* target, uint index)
            where TFrom : unmanaged
        {
            if (sizeof(TFrom) == sizeof(byte))
                SSE41Helper.StoreVector128((byte*)origin, target, index);
            else if (sizeof(TFrom) == sizeof(short))
                SSE41Helper.StoreVector128((short*)origin, target, index);
            else if (sizeof(TFrom) == sizeof(int))
                SSE41Helper.StoreVector128((int*)origin, target, index);
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertLastElementsVector128<TFrom, TTo>(TFrom* origin, TTo* target, uint index, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (typeof(TFrom) == typeof(TTo))
                SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, (TFrom*)target, index, length);
            else if (typeof(TTo) == typeof(float))
                ConvertLastElementsToSingle(origin, (float*)target, index, length);
            else if (typeof(TTo) == typeof(double))
                ConvertLastElementsToDouble(origin, (double*)target, index, length);
            else
            {
                // Refer to comments in the above function, at the respective location
                //if (sizeof(TFrom) == sizeof(TTo))
                //    SIMDIntrinsicsHelper.StoreLastElementsVector128(origin, (TFrom*)target, index, length);
                if (sizeof(TTo) == sizeof(byte))
                    ConvertLastElementsToByte(origin, (byte*)target, index, length);
                else if (sizeof(TTo) == sizeof(short))
                    ConvertLastElementsToInt16(origin, (short*)target, index, length);
                else if (sizeof(TTo) == sizeof(int))
                    ConvertLastElementsToInt32(origin, (int*)target, index, length);
                else if (sizeof(TTo) == sizeof(long))
                    ConvertLastElementsToInt64(origin, (long*)target, index, length);
            }
        }
        #region ConvertLastElementsVector128 Parts
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertLastElementsToSingle<TFrom>(TFrom* origin, float* target, uint index, uint length)
            where TFrom : unmanaged
        {
            if (typeof(TFrom) == typeof(double))
                SSE2Helper.StoreLastElementsVector128((double*)origin, target, index, length);
            else
            {
                if (sizeof(TFrom) == sizeof(byte))
                    SSE41Helper.StoreLastElementsVector128((byte*)origin, target, index, length);
                else if (sizeof(TFrom) == sizeof(short))
                    SSE41Helper.StoreLastElementsVector128((short*)origin, target, index, length);
                else if (sizeof(TFrom) == sizeof(int))
                    SSE2Helper.StoreLastElementsVector128((int*)origin, target, index, length);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertLastElementsToDouble<TFrom>(TFrom* origin, double* target, uint index, uint length)
            where TFrom : unmanaged
        {
            if (typeof(TFrom) == typeof(float))
                SIMDIntrinsicsHelper.StoreLastElementsVector128((float*)origin, target, index, length);
            else
            {
                if (sizeof(TFrom) == sizeof(byte))
                    SIMDIntrinsicsHelper.StoreLastElementsVector128((byte*)origin, target, index, length);
                else if (sizeof(TFrom) == sizeof(short))
                    SIMDIntrinsicsHelper.StoreLastElementsVector128((short*)origin, target, index, length);
                else if (sizeof(TFrom) == sizeof(int))
                    SIMDIntrinsicsHelper.StoreLastElementsVector128((int*)origin, target, index, length);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertLastElementsToByte<TFrom>(TFrom* origin, byte* target, uint index, uint length)
            where TFrom : unmanaged
        {
            if (sizeof(TFrom) == sizeof(short))
                SSSE3Helper.StoreLastElementsVector128Downcast((short*)origin, target, index, length);
            else if (sizeof(TFrom) == sizeof(int))
                SSSE3Helper.StoreLastElementsVector128Downcast((int*)origin, target, index, length);
            else if (sizeof(TFrom) == sizeof(long))
                SSSE3Helper.StoreLastElementsVector128Downcast((long*)origin, target, index, length);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertLastElementsToInt16<TFrom>(TFrom* origin, short* target, uint index, uint length)
            where TFrom : unmanaged
        {
            if (sizeof(TFrom) == sizeof(byte))
                SSE41Helper.StoreLastElementsVector128((byte*)origin, target, index, length);
            else if (sizeof(TFrom) == sizeof(int))
                SSSE3Helper.StoreLastElementsVector128Downcast((int*)origin, target, index, length);
            else if (sizeof(TFrom) == sizeof(long))
                SSSE3Helper.StoreLastElementsVector128Downcast((long*)origin, target, index, length);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertLastElementsToInt32<TFrom>(TFrom* origin, int* target, uint index, uint length)
            where TFrom : unmanaged
        {
            if (typeof(TFrom) == typeof(float))
                SSE2Helper.StoreLastElementsVector128((float*)origin, target, index, length);
            else if (typeof(TFrom) == typeof(double))
                SIMDIntrinsicsHelper.StoreLastElementsVector128((double*)origin, target, index, length);
            else
            {
                if (sizeof(TFrom) == sizeof(byte))
                    SSE41Helper.StoreLastElementsVector128((byte*)origin, target, index, length);
                else if (sizeof(TFrom) == sizeof(short))
                    SSE41Helper.StoreLastElementsVector128((short*)origin, target, index, length);
                else if (sizeof(TFrom) == sizeof(long))
                    SSSE3Helper.StoreLastElementsVector128Downcast((long*)origin, target, index, length);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertLastElementsToInt64<TFrom>(TFrom* origin, long* target, uint index, uint length)
            where TFrom : unmanaged
        {
            if (sizeof(TFrom) == sizeof(byte))
                SSE41Helper.StoreLastElementsVector128((byte*)origin, target, index, length);
            else if (sizeof(TFrom) == sizeof(short))
                SSE41Helper.StoreLastElementsVector128((short*)origin, target, index, length);
            else if (sizeof(TFrom) == sizeof(int))
                SSE41Helper.StoreLastElementsVector128((int*)origin, target, index, length);
        }
        #endregion
        #endregion
    }
}