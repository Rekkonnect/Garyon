﻿using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the SSSE3 CPU instruction set. Every function checks whether the SSSE3 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public unsafe class SSSE3Helper : SSE3Helper
    {
        #region Store
        #region T* -> byte*
        /// <summary>Converts a Vector128 of <seealso cref="short"/> into a Vector64 of <seealso cref="byte"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector128<TPortionSize>(short* origin, byte* target, uint index)
            where TPortionSize : unmanaged
        {
            if (Ssse3.IsSupported)
                Store<byte, TPortionSize>(ConvertToVector128Byte(origin, index), target, index);
        }
        /// <summary>Converts a Vector128 of <seealso cref="short"/> into a Vector64 of <seealso cref="byte"/> and stores it in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector64(short* origin, byte* target, uint index)
        {
            StoreFromVector128<long>(origin, target, index);
        }
        /// <summary>Converts a Vector128 of <seealso cref="short"/> into a Vector64 of <seealso cref="byte"/> and stores the first 4 bytes of the result in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector32(short* origin, byte* target, uint index)
        {
            StoreFromVector128<int>(origin, target, index);
        }
        /// <summary>Converts a Vector128 of <seealso cref="short"/> into a Vector64 of <seealso cref="byte"/> and stores the first 2 bytes of the result in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="short"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector16(short* origin, byte* target, uint index)
        {
            StoreFromVector128<short>(origin, target, index);
        }

        /// <summary>Converts a Vector128 of <seealso cref="int"/> into a Vector32 of <seealso cref="byte"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector128<TPortionSize>(int* origin, byte* target, uint index)
            where TPortionSize : unmanaged
        {
            if (Ssse3.IsSupported)
                Store<byte, TPortionSize>(ConvertToVector128Byte(origin, index), target, index);
        }
        /// <summary>Converts a Vector128 of <seealso cref="int"/> into a Vector32 of <seealso cref="byte"/> and stores it in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector32(int* origin, byte* target, uint index)
        {
            StoreFromVector128<int>(origin, target, index);
        }
        /// <summary>Converts a Vector128 of <seealso cref="int"/> into a Vector32 of <seealso cref="byte"/> and stores the first 2 bytes of the result in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector16(int* origin, byte* target, uint index)
        {
            StoreFromVector128<short>(origin, target, index);
        }

        /// <summary>Converts a Vector128 of <seealso cref="long"/> into a Vector16 of <seealso cref="byte"/> and stores it in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="byte"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector16(long* origin, byte* target, uint index)
        {
            if (Ssse3.IsSupported)
                Store<byte, short>(ConvertToVector128Byte(origin, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128Downcast<T>(T* origin, byte* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Ssse3.IsSupported)
                return;

            StoreRemainingElements(4);
            StoreRemainingElements(2);
            StoreRemainingElements(1);

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
                if (remainder == 4)
                    StoreVector32((short*)origin, target, index);
                if (remainder == 2)
                    StoreVector16((short*)origin, target, index);
                if (remainder == 1)
                    target[index] = (byte)((short*)origin)[index];
            }
            void StoreRemainingInt32(uint remainder)
            {
                if (remainder == 2)
                    StoreVector16((int*)origin, target, index);
                if (remainder == 1)
                    target[index] = (byte)((int*)origin)[index];
            }
            void StoreRemainingInt64(uint remainder)
            {
                if (remainder == 1)
                    target[index] = (byte)((long*)origin)[index];
            }
        }
        #endregion
        #region T* -> short*
        /// <summary>Converts a Vector128 of <seealso cref="int"/> into a Vector32 of <seealso cref="short"/> and stores a portion it in the given address, based on the given sequences.</summary>
        /// <typeparam name="TPortionSize">The type representing the size of the potion of the vector to store.</typeparam>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreFromVector128<TPortionSize>(int* origin, short* target, uint index)
            where TPortionSize : unmanaged
        {
            if (Ssse3.IsSupported)
                Store<short, TPortionSize>(ConvertToVector128Int16(origin, index), target, index);
        }
        /// <summary>Converts a Vector128 of <seealso cref="int"/> into a Vector64 of <seealso cref="short"/> and stores it in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector64(int* origin, short* target, uint index)
        {
            StoreFromVector128<long>(origin, target, index);
        }
        /// <summary>Converts a Vector128 of <seealso cref="int"/> into a Vector64 of <seealso cref="short"/> and stores the first 4 bytes of the result in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="int"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector32(int* origin, short* target, uint index)
        {
            StoreFromVector128<int>(origin, target, index);
        }

        /// <summary>Converts a Vector128 of <seealso cref="long"/> into a Vector32 of <seealso cref="short"/> and stores it in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="short"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector32(long* origin, short* target, uint index)
        {
            if (Ssse3.IsSupported)
                Store<short, int>(ConvertToVector128Int16(origin, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128Downcast<T>(T* origin, short* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Ssse3.IsSupported)
                return;

            StoreRemainingElements(2);
            StoreRemainingElements(1);

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
                if (remainder == 2)
                    StoreVector32((int*)origin, target, index);
                if (remainder == 1)
                    target[index] = (byte)((int*)origin)[index];
            }
            void StoreRemainingInt64(uint remainder)
            {
                if (remainder == 1)
                    target[index] = (byte)((long*)origin)[index];
            }
        }
        #endregion
        #region T* -> int*
        /// <summary>Converts a Vector128 of <seealso cref="long"/> into a Vector64 of <seealso cref="int"/> and stores it in the given address, based on the given sequences.</summary>
        /// <param name="origin">The origin <seealso cref="long"/> sequence.</param>
        /// <param name="target">The target <seealso cref="int"/> sequence.</param>
        /// <param name="index">The index of the sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector64(long* origin, int* target, uint index)
        {
            if (Ssse3.IsSupported)
                Store<int, long>(ConvertToVector128Int32(origin, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128Downcast<T>(T* origin, int* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Ssse3.IsSupported)
                return;

            StoreRemainingElements(1);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(long))
                        target[index] = (byte)((long*)origin)[index];
                    index |= remainder;
                }
            }
        }
        #endregion
        #endregion

        #region Convert
        #region Vector128
        #region T* -> byte*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> ConvertToVector128Byte(short* origin, uint index)
        {
            if (Ssse3.IsSupported)
                return ConvertToVector128<byte>(origin + index, ShuffleMaskVector128i16i8);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> ConvertToVector128Byte(int* origin, uint index)
        {
            if (Ssse3.IsSupported)
                return ConvertToVector128<byte>(origin + index, ShuffleMaskVector128i32i8);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> ConvertToVector128Byte(long* origin, uint index)
        {
            if (Ssse3.IsSupported)
                return ConvertToVector128<byte>(origin + index, ShuffleMaskVector128i64i8);
            return default;
        }
        #endregion
        #region T* -> short*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<short> ConvertToVector128Int16(int* origin, uint index)
        {
            if (Ssse3.IsSupported)
                return ConvertToVector128<short>(origin + index, ShuffleMaskVector128i32i16);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<short> ConvertToVector128Int16(long* origin, uint index)
        {
            if (Ssse3.IsSupported)
                return ConvertToVector128<short>(origin + index, ShuffleMaskVector128i64i16);
            return default;
        }
        #endregion
        #region T* -> int*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<int> ConvertToVector128Int32(long* origin, uint index)
        {
            if (Ssse3.IsSupported)
                return ConvertToVector128<int>(origin + index, ShuffleMaskVector128i64i32);
            return default;
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> ConvertToVector128<T>(void* origin, Vector128<byte> shuffleMask)
            where T : unmanaged
        {
            if (Ssse3.IsSupported)
                return Ssse3.Shuffle(Ssse3.LoadVector128((byte*)origin), shuffleMask).As<byte, T>();
            return default;
        }
        #endregion
        #endregion
    }
}
