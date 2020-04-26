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

            if (sizeof(T) <= sizeof(byte))
                StoreRemainingElements(ref origin, ref target, count, 16);
            StoreLastElementsVector128(origin, target, count);

            static void StoreRemainingElements(ref T* origin, ref T* target, uint count, uint remainder)
            {
                if ((count & remainder) > 0)
                {
                    if (sizeof(T) == sizeof(byte))
                        StoreRemainingByte((byte*)origin, (byte*)target, count, remainder);

                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingByte(byte* origin, byte* target, uint count, uint remainder)
            {
                if (remainder == 16)
                    StoreVector128(origin, target, count);
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
            StoreLastElementsVector128(origin, target, index, count);

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

        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static new Vector128<T> ANDVector128<T>(T* origin, Vector128<T> and, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return ANDVector128((byte*)origin, and.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return ANDVector128((short*)origin, and.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(long))
                return ANDVector128((long*)origin, and.As<T, long>(), index).As<long, T>();

            return SSEHelper.ANDVector128(origin, and, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> ANDVector128(byte* origin, Vector128<byte> and, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.And(Sse2.LoadVector128(origin + index), and);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<short> ANDVector128(short* origin, Vector128<short> and, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.And(Sse2.LoadVector128(origin + index), and);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<long> ANDVector128(long* origin, Vector128<long> and, uint index)
        {
            return ANDVector128((double*)origin, and.As<long, double>(), index).As<double, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<double> ANDVector128(double* origin, Vector128<double> and, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.And(Sse2.LoadVector128(origin + index), and);
            return default;
        }
        #endregion

        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static new Vector128<T> ORVector128<T>(T* origin, Vector128<T> or, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return ORVector128((byte*)origin, or.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return ORVector128((short*)origin, or.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(long))
                return ORVector128((long*)origin, or.As<T, long>(), index).As<long, T>();

            return SSEHelper.ORVector128(origin, or, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> ORVector128(byte* origin, Vector128<byte> or, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.Or(Sse2.LoadVector128(origin + index), or);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<short> ORVector128(short* origin, Vector128<short> or, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.Or(Sse2.LoadVector128(origin + index), or);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<long> ORVector128(long* origin, Vector128<long> or, uint index)
        {
            return ORVector128((double*)origin, or.As<long, double>(), index).As<double, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<double> ORVector128(double* origin, Vector128<double> or, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.Or(Sse2.LoadVector128(origin + index), or);
            return default;
        }
        #endregion

        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static new Vector128<T> XORVector128<T>(T* origin, Vector128<T> xor, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return XORVector128((byte*)origin, xor.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return XORVector128((short*)origin, xor.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(long))
                return XORVector128((long*)origin, xor.As<T, long>(), index).As<long, T>();

            return SSEHelper.XORVector128(origin, xor, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> XORVector128(byte* origin, Vector128<byte> xor, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.Xor(Sse2.LoadVector128(origin + index), xor);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<short> XORVector128(short* origin, Vector128<short> xor, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.Xor(Sse2.LoadVector128(origin + index), xor);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<long> XORVector128(long* origin, Vector128<long> xor, uint index)
        {
            return XORVector128((double*)origin, xor.As<long, double>(), index).As<double, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<double> XORVector128(double* origin, Vector128<double> xor, uint index)
        {
            if (Sse2.IsSupported)
                return Sse2.Xor(Sse2.LoadVector128(origin + index), xor);
            return default;
        }
        #endregion
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
