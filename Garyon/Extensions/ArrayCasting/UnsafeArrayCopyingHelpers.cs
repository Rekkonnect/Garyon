﻿using Garyon.Functions.IntrinsicsHelpers;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Extensions.ArrayCasting
{
    /// <summary>Contains unsafe helper functions for array copying using specialized CPU instructions. All functions check whether the minimum supported instruction set is included; in the case that the set is unavailable, the functions simply do nothing.</summary>
    public static unsafe class UnsafeArrayCopyingHelpers
    {
        // TODO: Get rid of those, use ones created in SSE2Helper
        #region Vector128 Shuffle Masks
        private static readonly byte[] shuffleMaskBytesVector128i64i32 = new byte[16];
        private static readonly byte[] shuffleMaskBytesVector128i64i16 = new byte[16];
        private static readonly byte[] shuffleMaskBytesVector128i64i8 = new byte[16];
        private static readonly byte[] shuffleMaskBytesVector128i32i16 = new byte[16];
        private static readonly byte[] shuffleMaskBytesVector128i32i8 = new byte[16];
        private static readonly byte[] shuffleMaskBytesVector128i16i8 = new byte[16];

        private static Vector128<byte> shuffleMaskVector128i64i32;
        private static Vector128<byte> shuffleMaskVector128i64i16;
        private static Vector128<byte> shuffleMaskVector128i64i8;
        private static Vector128<byte> shuffleMaskVector128i32i16;
        private static Vector128<byte> shuffleMaskVector128i32i8;
        private static Vector128<byte> shuffleMaskVector128i16i8;
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static UnsafeArrayCopyingHelpers()
        {
            if (Sse2.IsSupported)
                GenerateMasks();

            // Local Functions

            static void GenerateMasks()
            {
                GenerateMask<long, int>(shuffleMaskBytesVector128i64i32, ref shuffleMaskVector128i64i32);
                GenerateMask<long, short>(shuffleMaskBytesVector128i64i16, ref shuffleMaskVector128i64i16);
                GenerateMask<long, byte>(shuffleMaskBytesVector128i64i8, ref shuffleMaskVector128i64i8);
                GenerateMask<int, short>(shuffleMaskBytesVector128i32i16, ref shuffleMaskVector128i32i16);
                GenerateMask<int, byte>(shuffleMaskBytesVector128i32i8, ref shuffleMaskVector128i32i8);
                GenerateMask<short, byte>(shuffleMaskBytesVector128i16i8, ref shuffleMaskVector128i16i8);
            }

            static void GenerateMask<TFrom, TTo>(byte[] maskBytes, ref Vector128<byte> mask)
                where TFrom : unmanaged
                where TTo : unmanaged
            {
                // Generate mask
                Array.Fill(maskBytes, (byte)0b1_000_0000);

                for (int i = 0; i < sizeof(TFrom) / sizeof(TTo); i++)
                    for (int j = 0; j < sizeof(TTo); j++)
                        maskBytes[i * sizeof(TTo) + j] = (byte)(i * sizeof(TFrom) + j);

                // Interpret mask as Vector128
                fixed (byte* bytes = maskBytes)
                    mask = Sse2.LoadVector128(bytes);
            }
        }

        // TODO: Attempt generalization to the most feasible without hurting performance
        // TODO: Support floats/doubles

        // Copying from T1* to T2* where sizeof(T1) > sizeof(T2) is not supported with Vector256, consider using once new intrinsics support this feature more optimally
        #region Vector256
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool CopyToArrayVector256<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            // Downcasting elements in AVX2 is probably inefficient enough to not care enough
            if (sizeof(TFrom) > sizeof(TTo))
                return false;

            // Directly copy their bytes
            if (typeof(TFrom) == typeof(TTo))
                return CopyToByteArrayVector256((byte*)origin, (byte*)target, length);

            if (typeof(TTo) == typeof(float))
            {
                if (typeof(TFrom) == typeof(byte))
                    return CopyToSingleArrayVector256((byte*)origin, (float*)target, length);
                if (typeof(TFrom) == typeof(short))
                    return CopyToSingleArrayVector256((short*)origin, (float*)target, length);
                if (typeof(TFrom) == typeof(int))
                    return CopyToSingleArrayVector256((int*)origin, (float*)target, length);
            }
            if (typeof(TTo) == typeof(double))
            {
                if (typeof(TFrom) == typeof(byte))
                    return CopyToDoubleArrayVector256((byte*)origin, (double*)target, length);
                if (typeof(TFrom) == typeof(short))
                    return CopyToDoubleArrayVector256((short*)origin, (double*)target, length);
                if (typeof(TFrom) == typeof(int))
                    return CopyToDoubleArrayVector256((int*)origin, (double*)target, length);
            }

            // Even when copying float/double to the same type, the values are an equal sequence of normal int/long

            if (sizeof(TTo) == sizeof(byte))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToByteArrayVector256((byte*)origin, (byte*)target, length);
            }
            if (sizeof(TTo) == sizeof(short))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToInt16ArrayVector256((byte*)origin, (short*)target, length);
            }
            if (sizeof(TTo) == sizeof(int))
            {
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

            uint size = (uint)(sizeof(Vector256<long>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<long>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<long>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<long>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<int>) / sizeof(int));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<int>) / sizeof(int));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<int>) / sizeof(int));

            uint i = 0;
            for (; i < length; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            AVXHelper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<short>) / sizeof(short));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<short>) / sizeof(short));

            uint i = 0;
            for (; i < length; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            AVXHelper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<byte>) / sizeof(byte));

            uint i = 0;
            for (; i < length; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            AVXHelper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<float>) / sizeof(float));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<float>) / sizeof(float));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<float>) / sizeof(float));

            uint i = 0;
            for (; i < length; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<double>) / sizeof(double));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<double>) / sizeof(double));

            uint i = 0;
            for (; i < length; i += size)
                AVX2Helper.StoreVector256(origin, target, i);
            AVX2Helper.StoreLastElementsVector256(origin, target, i, length);

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

            uint size = (uint)(sizeof(Vector256<double>) / sizeof(double));

            uint i = 0;
            for (; i < length; i += size)
                AVXHelper.StoreVector256(origin, target, i);
            AVXHelper.StoreLastElementsVector256(origin, target, i, length);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool CopyToArrayVector128<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            // Directly copy their bytes, allowing custom unmanaged structs to be copied through this method
            // Attempt to optimize few CPU cycles
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
            }
            if (typeof(TTo) == typeof(double))
            {
                if (sizeof(TFrom) == sizeof(byte))
                    return CopyToDoubleArrayVector128((byte*)origin, (double*)target, length);
                if (sizeof(TFrom) == sizeof(short))
                    return CopyToDoubleArrayVector128((short*)origin, (double*)target, length);
                if (sizeof(TFrom) == sizeof(int))
                    return CopyToDoubleArrayVector128((int*)origin, (double*)target, length);
                if (sizeof(TFrom) == sizeof(long))
                    return CopyToDoubleArrayVector128((long*)origin, (double*)target, length);
                if (sizeof(TFrom) == sizeof(float))
                    return CopyToDoubleArrayVector128((float*)origin, (double*)target, length);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

        #region T* -> long*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt64ArrayVector128(byte* origin, long* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<long>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt64ArrayVector128(short* origin, long* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<long>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt64ArrayVector128(int* origin, long* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<long>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>*. Minimum required instruction set: SSE2.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="long"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt64ArrayVector128(long* origin, long* target, uint length)
        {
            if (!Sse2.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<long>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
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
        public static bool CopyToInt32ArrayVector128(byte* origin, int* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<int>) / sizeof(int));

            uint i = 0;
            for (; i < length; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt32ArrayVector128(short* origin, int* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<int>) / sizeof(int));

            uint i = 0;
            for (; i < length; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSE2.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt32ArrayVector128(int* origin, int* target, uint length)
        {
            if (!Sse2.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<int>) / sizeof(int));

            uint i = 0;
            for (; i < length; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
            SSE2Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt32ArrayVector128(long* origin, int* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<int>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                SSSE3Helper.StoreVector64(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> short*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: SSE4.1.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt16ArrayVector128(byte* origin, short* target, uint length)
        {
            if (!Sse41.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<short>) / sizeof(short));

            uint i = 0;
            for (; i < length; i += size)
                SSE41Helper.StoreVector128(origin, target, i);
            SSE41Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: SSE2.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt16ArrayVector128(short* origin, short* target, uint length)
        {
            if (!Sse2.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<short>) / sizeof(short));

            uint i = 0;
            for (; i < length; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
            SSE2Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: SSE2.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt16ArrayVector128(int* origin, short* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<short>) / sizeof(int));

            uint i = 0;
            for (; i < length; i += size)
                SSSE3Helper.StoreVector64(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToInt16ArrayVector128(long* origin, short* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<short>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                SSSE3Helper.StoreVector32(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        #endregion
        #region T* -> byte*
        /// <summary>Copies the elements of a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: SSE2.</summary>
        /// <param name="origin">The origin <seealso cref="byte"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToByteArrayVector128(byte* origin, byte* target, uint length)
        {
            if (!Sse2.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<byte>) / sizeof(byte));

            uint i = 0;
            for (; i < length; i += size)
                SSE2Helper.StoreVector128(origin, target, i);
            SSE2Helper.StoreLastElementsVector128(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="short"/> sequence passed as a <seealso cref="short"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToByteArrayVector128(short* origin, byte* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<byte>) / sizeof(short));

            uint i = 0;
            for (; i < length; i += size)
                SSSE3Helper.StoreVector64(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="int"/> sequence passed as a <seealso cref="int"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToByteArrayVector128(int* origin, byte* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<byte>) / sizeof(int));

            uint i = 0;
            for (; i < length; i += size)
                SSSE3Helper.StoreVector32(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        /// <summary>Copies the elements of a <seealso cref="long"/> sequence passed as a <seealso cref="long"/>* into a <seealso cref="byte"/> sequence passed as a <seealso cref="byte"/>*. Minimum required instruction set: SSSE3.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="length">The length of the origin sequence.</param>
        /// <returns>A value determining whether the operation succeeded, which is determined by the availability of the minimum required CPU instruction set.</returns>
        public static bool CopyToByteArrayVector128(long* origin, byte* target, uint length)
        {
            if (!Ssse3.IsSupported)
                return false;

            uint size = (uint)(sizeof(Vector128<byte>) / sizeof(long));

            uint i = 0;
            for (; i < length; i += size)
                SSSE3Helper.StoreVector16(origin, target, i);
            SSSE3Helper.StoreLastElementsVector128Downcast(origin, target, i, length);

            return true;
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static bool CopyToArrayVector128<TFrom, TTo>(TFrom* origin, TTo* target, uint length, bool supportedInstructionSet)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (!supportedInstructionSet)
                return false;

            uint size = (uint)(sizeof(Vector128<TTo>) / Math.Max(sizeof(TFrom), sizeof(TTo)));

            uint i = 0;

            for (; i < length; i += size)
            {
                // ?
            }
            // We'll get back to this soon:tm:

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static bool CopyToBiggerArrayVector128<TFrom, TTo>(TFrom* origin, TTo* target, uint length, bool supportedInstructionSet)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (!supportedInstructionSet)
                return false;

            uint size = (uint)(sizeof(Vector128<TTo>) / sizeof(TTo));

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static bool CopyToSmallerArrayVector128<TFrom, TTo>(TFrom* origin, TTo* target, uint length, bool supportedInstructionSet, ref Vector128<byte> shuffleMask)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (!supportedInstructionSet)
                return false;

            uint size = (uint)(sizeof(Vector128<TTo>) / sizeof(TFrom));

            uint i = 0;

            for (; i < length; i += size)
            {
                var vec = Ssse3.LoadVector128((byte*)(origin + i));
                Ssse3.Store((byte*)(target + i), Ssse3.Shuffle(vec, shuffleMask));
            }


            return true;
        }
        #endregion
    }
}