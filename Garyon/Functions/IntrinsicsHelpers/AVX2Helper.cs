using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the AVX2 CPU instruction set. Every function checks whether the AVX2 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public unsafe class AVX2Helper : AVXHelper
    {
        #region Store
        #region Vector256
        #region T* -> byte*
        /// <summary>Converts a Vector256 of <seealso cref="short"/> into a Vector128 of <seealso cref="byte"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector256<TPortionSize>(short* origin, byte* target, uint index)
            where TPortionSize : unmanaged
        {
            Store<byte, TPortionSize>(ConvertToVector256Byte(origin, index), target, index);
        }
        public static void StoreVector128(short* origin, byte* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], ConvertToVector256Byte(origin, index).GetLower());
        }

        /// <summary>Converts a Vector256 of <seealso cref="int"/> into a Vector64 of <seealso cref="byte"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector256<TPortionSize>(int* origin, byte* target, uint index)
            where TPortionSize : unmanaged
        {
            Store<byte, TPortionSize>(ConvertToVector256Byte(origin, index), target, index);
        }
        public static void StoreVector64(int* origin, byte* target, uint index)
        {
            if (Avx2.IsSupported)
                StoreFromVector256<long>(origin, target, index);
        }

        /// <summary>Converts a Vector256 of <seealso cref="long"/> into a Vector32 of <seealso cref="byte"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector256<TPortionSize>(long* origin, byte* target, uint index)
            where TPortionSize : unmanaged
        {
            Store<byte, TPortionSize>(ConvertToVector256Byte(origin, index), target, index);
        }
        public static void StoreVector32(long* origin, byte* target, uint index)
        {
            if (Avx2.IsSupported)
                StoreFromVector256<int>(origin, target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256Downcast<T>(T* origin, byte* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Avx2.IsSupported)
                return;
            
            if (sizeof(T) == sizeof(short))
                StoreRemainingElements(8);
            if (sizeof(T) == sizeof(int))
                StoreRemainingElements(4);
            if (sizeof(T) == sizeof(long))
                StoreRemainingElements(2);
            StoreLastElementsVector128Downcast(origin, target, index, length);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(short))
                        StoreRemainingInt16(remainder);
                    if (typeof(T) == typeof(int))
                        StoreRemainingInt32(remainder);
                    if (typeof(T) == typeof(long))
                        StoreRemainingInt64(remainder);
                    index |= remainder;
                }
            }
            void StoreRemainingInt16(uint remainder)
            {
                if (remainder == 8)
                    StoreVector128((short*)origin, target, index);
            }
            void StoreRemainingInt32(uint remainder)
            {
                if (remainder == 4)
                    StoreVector64((int*)origin, target, index);
            }
            void StoreRemainingInt64(uint remainder)
            {
                if (remainder == 2)
                    StoreVector32((long*)origin, target, index);
            }
        }
        #endregion
        #region T* -> short*
        public static void StoreVector256(byte* origin, short* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Int16(&origin[index]));
        }

        /// <summary>Converts a Vector256 of <seealso cref="int"/> into a Vector128 of <seealso cref="short"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector256<TPortionSize>(int* origin, short* target, uint index)
            where TPortionSize : unmanaged
        {
            Store<short, TPortionSize>(ConvertToVector256Int16(origin, index), target, index);
        }
        public static void StoreVector128(int* origin, short* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], ConvertToVector256Int16(origin, index).GetLower());
        }

        /// <summary>Converts a Vector256 of <seealso cref="long"/> into a Vector64 of <seealso cref="short"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector256<TPortionSize>(long* origin, short* target, uint index)
            where TPortionSize : unmanaged
        {
            Store<short, TPortionSize>(ConvertToVector256Int16(origin, index), target, index);
        }
        public static void StoreVector64(long* origin, short* target, uint index)
        {
            if (Avx2.IsSupported)
                StoreFromVector256<long>(origin, target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256Downcast<T>(T* origin, short* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Avx2.IsSupported)
                return;

            if (sizeof(T) == sizeof(int))
                StoreRemainingElements(4);
            if (sizeof(T) == sizeof(long))
                StoreRemainingElements(2);
            StoreLastElementsVector128Downcast(origin, target, index, length);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(int))
                        StoreRemainingInt32(remainder);
                    if (typeof(T) == typeof(long))
                        StoreRemainingInt64(remainder);
                    index |= remainder;
                }
            }
            void StoreRemainingInt32(uint remainder)
            {
                if (remainder == 4)
                    StoreVector128((int*)origin, target, index);
            }
            void StoreRemainingInt64(uint remainder)
            {
                if (remainder == 2)
                    StoreVector64((long*)origin, target, index);
            }
        }
        #endregion
        #region T* -> int*
        public static void StoreVector256(byte* origin, int* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Int32(&origin[index]));
        }
        public static void StoreVector256(short* origin, int* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Int32(&origin[index]));
        }

        /// <summary>Converts a Vector256 of <seealso cref="long"/> into a Vector128 of <seealso cref="int"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector256<TPortionSize>(long* origin, int* target, uint index)
            where TPortionSize : unmanaged
        {
            Store<int, TPortionSize>(ConvertToVector256Int32(origin, index), target, index);
        }
        public static void StoreVector128(long* origin, int* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], ConvertToVector256Int32(origin, index).GetLower());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256Downcast<T>(T* origin, int* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Avx2.IsSupported)
                return;

            if (sizeof(T) == sizeof(long))
                StoreRemainingElements(2);
            StoreLastElementsVector128Downcast(origin, target, index, length);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(long))
                        StoreRemainingInt64(remainder);
                    index |= remainder;
                }
            }
            void StoreRemainingInt64(uint remainder)
            {
                if (remainder == 2)
                    StoreVector64((long*)origin, target, index);
            }
        }
        #endregion
        #region T* -> long*
        public static void StoreVector256(byte* origin, long* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Int64(&origin[index]));
        }
        public static void StoreVector256(short* origin, long* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Int64(&origin[index]));
        }
        public static void StoreVector256(int* origin, long* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Int64(&origin[index]));
        }
        #endregion
        #region T* -> float*
        public static void StoreVector256(byte* origin, float* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Single(Avx2.ConvertToVector256Int32(&origin[index])));
        }
        public static void StoreVector256(short* origin, float* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Single(Avx2.ConvertToVector256Int32(&origin[index])));
        }
        #endregion
        #endregion
        #endregion

        #region Convert
        #region Vector256
        #region T* -> byte*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> ConvertToVector256Byte(short* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<byte>(origin + index, ShuffleMaskVector256i16i8);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> ConvertToVector256Byte(int* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<byte>(origin + index, ShuffleMaskVector256i32i8);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> ConvertToVector256Byte(long* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<byte>(origin + index, ShuffleMaskVector256i64i8);
            return default;
        }
        #endregion
        #region T* -> short*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> ConvertToVector256Int16(int* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<short>(origin + index, ShuffleMaskVector256i32i16);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> ConvertToVector256Int16(long* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<short>(origin + index, ShuffleMaskVector256i64i16);
            return default;
        }
        #endregion
        #region T* -> int*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> ConvertToVector256Int32(long* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<int>(origin + index, ShuffleMaskVector256i64i32);
            return default;
        }
        #endregion
        #region T* -> float*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> ConvertToVector256Single(byte* origin, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.ConvertToVector256Single(Avx2.ConvertToVector256Int32(&origin[index]));
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> ConvertToVector256Single(short* origin, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.ConvertToVector256Single(Avx2.ConvertToVector256Int32(&origin[index]));
            return default;
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<T> ConvertToVector256<T>(void* origin, Vector256<byte> shuffleMask)
            where T : unmanaged
        {
            if (Avx2.IsSupported)
                return Avx2.Shuffle(Avx2.LoadVector256((byte*)origin), shuffleMask).As<byte, T>();
            return default;
        }
        #endregion
        #endregion
    }
}
