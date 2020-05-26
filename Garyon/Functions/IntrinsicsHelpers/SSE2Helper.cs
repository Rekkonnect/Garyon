using Garyon.Functions.PointerHelpers;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the SSE2 CPU instruction set. Every function checks whether the SSE2 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public abstract unsafe class SSE2Helper : SSEHelper
    {
        #region Vector128 Shuffle Masks
        protected static readonly byte[] ShuffleMaskBytesVector128i64i32 = new byte[16];
        protected static readonly byte[] ShuffleMaskBytesVector128i64i16 = new byte[16];
        protected static readonly byte[] ShuffleMaskBytesVector128i64i8 = new byte[16];
        protected static readonly byte[] ShuffleMaskBytesVector128i32i16 = new byte[16];
        protected static readonly byte[] ShuffleMaskBytesVector128i32i8 = new byte[16];
        protected static readonly byte[] ShuffleMaskBytesVector128i16i8 = new byte[16];

        protected static readonly Vector128<byte> ShuffleMaskVector128i64i32;
        protected static readonly Vector128<byte> ShuffleMaskVector128i64i16;
        protected static readonly Vector128<byte> ShuffleMaskVector128i64i8;
        protected static readonly Vector128<byte> ShuffleMaskVector128i32i16;
        protected static readonly Vector128<byte> ShuffleMaskVector128i32i8;
        protected static readonly Vector128<byte> ShuffleMaskVector128i16i8;
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static SSE2Helper()
        {
            if (Sse2.IsSupported)
            {
                GenerateMask<long, int>(ShuffleMaskBytesVector128i64i32, ref ShuffleMaskVector128i64i32);
                GenerateMask<long, short>(ShuffleMaskBytesVector128i64i16, ref ShuffleMaskVector128i64i16);
                GenerateMask<long, byte>(ShuffleMaskBytesVector128i64i8, ref ShuffleMaskVector128i64i8);
                GenerateMask<int, short>(ShuffleMaskBytesVector128i32i16, ref ShuffleMaskVector128i32i16);
                GenerateMask<int, byte>(ShuffleMaskBytesVector128i32i8, ref ShuffleMaskVector128i32i8);
                GenerateMask<short, byte>(ShuffleMaskBytesVector128i16i8, ref ShuffleMaskVector128i16i8);
            }

            static void GenerateMask<TFrom, TTo>(byte[] maskBytes, ref Vector128<byte> mask)
                where TFrom : unmanaged
                where TTo : unmanaged
            {
                // Generate mask
                Array.Fill(maskBytes, (byte)0b1_000_0000);

                // Assume that sizeof(TFrom) > sizeof(TTo)
                for (int i = 0; i < Vector128<TFrom>.Count; i++)
                    for (int j = 0; j < sizeof(TTo); j++)
                        maskBytes[i * sizeof(TTo) + j] = (byte)(i * sizeof(TFrom) + j);

                // Interpret mask as Vector128
                fixed (byte* bytes = maskBytes)
                    mask = Sse2.LoadVector128(bytes);
            }
        }

        #region Vector256
        /// <summary>Stores the last elements of a sequence into the target sequence of the same element type, using SSE2 CPU instructions. This function is made with regards to storing elements through <seealso cref="Vector256"/>s, thus storing up to 31 bytes that remain to be processed from the original sequence.</summary>
        /// <typeparam name="T">The type of the elements in the sequences. Its size in bytes has to be a power of 2, up to 16.</typeparam>
        /// <param name="origin">The origin sequence, passed as a pointer.</param>
        /// <param name="target">The target sequence, passed as a pointer.</param>
        /// <param name="index">The first index of the sequence that will be processed.</param>
        /// <param name="length">The length of the sequence.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256<T>(T* origin, T* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Sse2.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            if (sizeof(T) == sizeof(byte))
                StoreRemainingElements(ref origin, ref target, count, 16);
            if (sizeof(T) == sizeof(short))
                StoreRemainingElements(ref origin, ref target, count, 8);
            if (sizeof(T) == sizeof(int))
                StoreRemainingElements(ref origin, ref target, count, 4);
            if (sizeof(T) == sizeof(long))
                StoreRemainingElements(ref origin, ref target, count, 2);
            StoreLastElementsVector128(origin, target, count);

            static void StoreRemainingElements(ref T* origin, ref T* target, uint count, uint remainder)
            {
                if ((count & remainder) > 0)
                {
                    if (sizeof(T) == sizeof(byte))
                        StoreRemainingByte((byte*)origin, (byte*)target, remainder);
                    if (sizeof(T) == sizeof(short))
                        StoreRemainingInt16((short*)origin, (short*)target, remainder);
                    if (sizeof(T) == sizeof(int))
                        StoreRemainingInt32((int*)origin, (int*)target, remainder);
                    if (sizeof(T) == sizeof(long))
                        StoreRemainingInt64((long*)origin, (long*)target, remainder);

                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingByte(byte* origin, byte* target, uint remainder)
            {
                if (remainder == 16)
                    StoreVector128(origin, target, 0);
            }
            static void StoreRemainingInt16(short* origin, short* target, uint remainder)
            {
                if (remainder == 8)
                    StoreVector128(origin, target, 0);
            }
            static void StoreRemainingInt32(int* origin, int* target, uint remainder)
            {
                if (remainder == 4)
                    StoreVector128(origin, target, 0);
            }
            static void StoreRemainingInt64(long* origin, long* target, uint remainder)
            {
                if (remainder == 2)
                    StoreVector128(origin, target, 0);
            }
        }
        #endregion

        #region Vector128
        #region T* -> int*
        public static void StoreVector128(float* origin, int* target, uint index)
        {
            if (Sse2.IsSupported)
                Sse2.Store(&target[index], Sse2.ConvertToVector128Int32(Sse2.LoadVector128(&origin[index])));
        }
        public static void StoreVector128(double* origin, int* target, uint index)
        {
            if (Sse2.IsSupported)
                Sse2.Store(&target[index], Sse2.ConvertToVector128Int32(Sse2.LoadVector128(&origin[index])));
        }
        #endregion
        #region T* -> float*
        public static void StoreVector128(int* origin, float* target, uint index)
        {
            if (Sse2.IsSupported)
                Sse2.Store(&target[index], Sse2.ConvertToVector128Single(Sse2.LoadVector128(&origin[index])));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256(int* origin, float* target, uint index, uint length)
        {
            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(4, ref origin, ref target, count);
            StoreLastElementsVector128(origin, target, 0, count);

            static void StoreRemainingElements(uint remainder, ref int* origin, ref float* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    StoreVector128(origin, target, 0);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128<T>(T* origin, float* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(2, ref origin, ref target, count);
            StoreRemainingElements(1, ref origin, ref target, count);

            static void StoreRemainingElements(uint remainder, ref T* origin, ref float* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    if (typeof(T) == typeof(int))
                        StoreRemainingInt32(remainder, (int*)origin, target);
                    else if (typeof(T) == typeof(double))
                        StoreRemainingDouble(remainder, (double*)origin, target);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingInt32(uint remainder, int* origin, float* target)
            {
                if (remainder == 2)
                    StoreVector64(origin, target, 0);
                if (remainder == 1)
                    *target = *origin;
            }
            static void StoreRemainingDouble(uint remainder, double* origin, float* target)
            {
                if (remainder == 1)
                    *target = *(float*)origin;
            }
        }
        #endregion
        #region T* -> double*
        public static void StoreVector128(int* origin, double* target, uint index)
        {
            if (Sse2.IsSupported)
                Sse2.Store(&target[index], Sse2.ConvertToVector128Double(Sse2.LoadVector128(&origin[index])));
        }
        public static void StoreVector128(float* origin, double* target, uint index)
        {
            if (Sse2.IsSupported)
                Sse2.Store(&target[index], Sse2.ConvertToVector128Double(Sse2.LoadVector128(&origin[index])));
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256(float* origin, int* target, uint index, uint length)
        {
            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(4, ref origin, ref target, count);
            StoreLastElementsVector128(origin, target, 0, count);

            static void StoreRemainingElements(uint remainder, ref float* origin, ref int* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    StoreVector128(origin, target, 0);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128(float* origin, int* target, uint index, uint length)
        {
            if (!Sse2.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(2, ref origin, ref target, count);
            StoreRemainingElements(1, ref origin, ref target, count);

            static void StoreRemainingElements(uint remainder, ref float* origin, ref int* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    StoreRemainingSingle(remainder, origin, target);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingSingle(uint remainder, float* origin, int* target)
            {
                if (remainder == 2)
                    StoreVector64(origin, target, 0);
                if (remainder == 1)
                    *target = (int)*origin;
            }
        }
        #endregion

        #region Vector64
        #region T* -> int*
        public static void StoreVector64(float* origin, int* target, uint index)
        {
            if (!Sse2.IsSupported)
                return;

            var vec = Sse2.ConvertToVector128Int32(Sse2.LoadVector128(&origin[index]));
            *(long*)(target + index) = *(long*)&vec;
        }
        public static void StoreVector64(double* origin, int* target, uint index)
        {
            if (!Sse2.IsSupported)
                return;

            var vec = Sse2.ConvertToVector128Int32(Sse2.LoadVector128(&origin[index]));
            *(long*)(target + index) = *(long*)&vec;
        }
        #endregion
        #region T* -> float*
        public static void StoreVector64(int* origin, float* target, uint index)
        {
            if (Sse2.IsSupported)
                Store<float, long>(Sse2.ConvertToVector128Single(Sse2.LoadVector128(&origin[index])), target, index);
        }
        public static void StoreVector64(double* origin, float* target, uint index)
        {
            if (Sse2.IsSupported)
                Store<float, long>(Sse2.ConvertToVector128Single(CreateVector128From64(origin, index)), target, index);
        }
        #endregion
        #endregion
    }
}
