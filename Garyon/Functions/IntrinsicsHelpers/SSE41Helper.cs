using Garyon.Functions.PointerHelpers;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the SSE4.1 CPU instruction set. Every function checks whether the SSE4.1 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public abstract unsafe class SSE41Helper : SSSE3Helper
    {
        #region Store
        #region Vector128
        #region T* -> short*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector128(byte* origin, short* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int16(CreateVector128From64(origin, index)));
        }

        /// <summary>Stores the last elements of a sequence into the target sequence of <seealso cref="short"/>, using AVX2 CPU instructions. This function is made with regards to storing elements through <seealso cref="Vector256"/>s, thus storing up to 31 bytes that remain to be processed from the original sequence.</summary>
        /// <typeparam name="T">The type of the elements in the origin sequence. Its size in bytes has to be a power of 2, up to 16.</typeparam>
        /// <param name="origin">The origin sequence, passed as a pointer.</param>
        /// <param name="target">The target sequence, passed as a pointer.</param>
        /// <param name="index">The first index of the sequence that will be processed.</param>
        /// <param name="length">The length of the sequence.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256<T>(T* origin, short* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(8, ref origin, ref target, count);
            StoreLastElementsVector128(origin, target, 0, count);

            static void StoreRemainingElements(uint remainder, ref T* origin, ref short* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder, origin, target);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingByte(uint remainder, T* origin, short* target)
            {
                if (remainder == 8)
                    StoreVector128((byte*)origin, target, 0);
            }
        }
        /// <summary>Stores the last elements of a sequence into the target sequence of <seealso cref="short"/>, using AVX2 CPU instructions. This function is made with regards to storing elements through <seealso cref="Vector256"/>s, thus storing up to 31 bytes that remain to be processed from the original sequence.</summary>
        /// <typeparam name="T">The type of the elements in the origin sequence. Its size in bytes has to be a power of 2, up to 8.</typeparam>
        /// <param name="origin">The origin sequence, passed as a pointer.</param>
        /// <param name="target">The target sequence, passed as a pointer.</param>
        /// <param name="index">The first index of the sequence that will be processed.</param>
        /// <param name="length">The length of the sequence.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128<T>(T* origin, short* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(4, ref origin, ref target, count);
            StoreRemainingElements(2, ref origin, ref target, count);
            StoreRemainingElements(1, ref origin, ref target, count);

            static void StoreRemainingElements(uint remainder, ref T* origin, ref short* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder, origin, target);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingByte(uint remainder, T* origin, short* target)
            {
                byte* originByte = (byte*)origin;
                if (remainder == 4)
                    StoreVector64(originByte, target, 0);
                if (remainder == 2)
                    StoreVector32(originByte, target, 0);
                if (remainder == 1)
                    StoreVector16(originByte, target, 0);
            }
        }
        #endregion
        #region T* -> int*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector128(byte* origin, int* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int32(CreateVector128From32(origin, index)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector128(short* origin, int* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int32(CreateVector128From64(origin, index)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256<T>(T* origin, int* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(4, ref origin, ref target, count);
            StoreLastElementsVector128(origin, target, 0, count);

            static void StoreRemainingElements(uint remainder, ref T* origin, ref int* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreVector128((byte*)origin, target, 0);
                    else if (typeof(T) == typeof(short))
                        StoreVector128((short*)origin, target, 0);
                    else if (typeof(T) == typeof(float))
                        StoreVector128((float*)origin, target, 0);
                    else if (typeof(T) == typeof(double))
                        StoreVector128((double*)origin, target, 0);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128<T>(T* origin, int* target, uint index, uint length)
            where T : unmanaged
        {
            if (typeof(T) == typeof(float))
            {
                SSE2Helper.StoreLastElementsVector128((float*)origin, target, index, length);
                return;
            }
            if (typeof(T) == typeof(double))
            {
                SIMDIntrinsicsHelper.StoreLastElementsVector128((double*)origin, target, index, length);
                return;
            }

            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(2, ref origin, ref target, count);
            StoreRemainingElements(1, ref origin, ref target, count);

            static void StoreRemainingElements(uint remainder, ref T* origin, ref int* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder, origin, target);
                    else if (typeof(T) == typeof(short))
                        StoreRemainingInt16(remainder, origin, target);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingByte(uint remainder, T* origin, int* target)
            {
                if (remainder == 2)
                    StoreVector64((byte*)origin, target, 0);
                if (remainder == 1)
                    *target = *(byte*)origin;
            }
            static void StoreRemainingInt16(uint remainder, T* origin, int* target)
            {
                if (remainder == 2)
                    StoreVector64((short*)origin, target, 0);
                if (remainder == 1)
                    *target = *(short*)origin;
            }
        }
        #endregion
        #region T* -> long*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector128(byte* origin, long* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int64(CreateVector128From16(origin, index)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector128(short* origin, long* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int64(CreateVector128From32(origin, index)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector128(int* origin, long* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int64(CreateVector128From64(origin, index)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256<T>(T* origin, long* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(2, ref origin, ref target, count);
            StoreLastElementsVector128(origin, target, 0, count);

            static void StoreRemainingElements(uint remainder, ref T* origin, ref long* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreVector128((byte*)origin, target, 0);
                    else if (typeof(T) == typeof(short))
                        StoreVector128((short*)origin, target, 0);
                    else if (typeof(T) == typeof(int))
                        StoreVector128((int*)origin, target, 0);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128<T>(T* origin, long* target, uint index, uint length)
            where T : unmanaged
        {
            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(1, origin, target, ref index, length);

            static void StoreRemainingElements(uint remainder, T* origin, long* target, ref uint index, uint length)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        target[index] = ((byte*)origin)[index];
                    else if (typeof(T) == typeof(short))
                        target[index] = ((short*)origin)[index];
                    else if (typeof(T) == typeof(int))
                        target[index] = ((int*)origin)[index];
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
        }
        #endregion
        #region T* -> float*
        public static void StoreVector128(byte* origin, float* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Single(Sse41.ConvertToVector128Int32(CreateVector128From32(origin, index))));
        }
        public static void StoreVector128(short* origin, float* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Single(Sse41.ConvertToVector128Int32(CreateVector128From64(origin, index))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256<T>(T* origin, float* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Sse41.IsSupported)
                return;

            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(4, ref origin, ref target, count);
            StoreLastElementsVector128(origin, target, index, count);

            static void StoreRemainingElements(uint remainder, ref T* origin, ref float* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder, origin, target);
                    else if (typeof(T) == typeof(short))
                        StoreRemainingInt16(remainder, origin, target);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingByte(uint remainder, T* origin, float* target)
            {
                if (remainder == 4)
                    StoreVector128((byte*)origin, target, 0);
            }
            static void StoreRemainingInt16(uint remainder, T* origin, float* target)
            {
                if (remainder == 4)
                    StoreVector128((short*)origin, target, 0);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128<T>(T* origin, float* target, uint index, uint length)
            where T : unmanaged
        {
            // Fallback to already handled cases from lower requirements
            if (typeof(T) == typeof(int) || typeof(T) == typeof(double))
            {
                SSE2Helper.StoreLastElementsVector128(origin, target, index, length);
                return;
            }

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
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder, origin, target);
                    else if (typeof(T) == typeof(short))
                        StoreRemainingInt16(remainder, origin, target);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
            static void StoreRemainingByte(uint remainder, T* origin, float* target)
            {
                if (remainder == 2)
                    StoreVector64((byte*)origin, target, 0);
                if (remainder == 1)
                    *target = *(byte*)origin;
            }
            static void StoreRemainingInt16(uint remainder, T* origin, float* target)
            {
                if (remainder == 2)
                    StoreVector64((short*)origin, target, 0);
                if (remainder == 1)
                    *target = *(short*)origin;
            }
        }
        #endregion
        #region T* -> double*
        public static void StoreVector128(byte* origin, double* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Double(Sse41.ConvertToVector128Int32(&origin[index])));
        }
        public static void StoreVector128(short* origin, double* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Double(Sse41.ConvertToVector128Int32(&origin[index])));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256<T>(T* origin, double* target, uint index, uint length)
            where T : unmanaged
        {
            PointerArithmetic.Increment(ref origin, ref target, index);

            uint count = length - index;

            StoreRemainingElements(2, ref origin, ref target, count);
            StoreLastElementsVector128(origin, target, 0, count);

            static void StoreRemainingElements(uint remainder, ref T* origin, ref double* target, uint count)
            {
                if ((count & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreVector128((byte*)origin, target, 0);
                    else if (typeof(T) == typeof(short))
                        StoreVector128((short*)origin, target, 0);
                    else if (typeof(T) == typeof(int))
                        StoreVector128((int*)origin, target, 0);
                    else if (typeof(T) == typeof(float))
                        StoreVector128((float*)origin, target, 0);
                    PointerArithmetic.Increment(ref origin, ref target, remainder);
                }
            }
        }
        #endregion
        #endregion

        #region Vector64
        #region T* -> short*
        public static void StoreVector64(byte* origin, short* target, uint index)
        {
            if (Sse41.IsSupported)
                Store<short, long>(Sse41.ConvertToVector128Int16(CreateVector128From32(origin, index)), target, index);
        }
        #endregion
        #region T* -> int*
        public static void StoreVector64(byte* origin, int* target, uint index)
        {
            if (Sse41.IsSupported)
                Store<int, long>(Sse41.ConvertToVector128Int32(CreateVector128From16(origin, index)), target, index);
        }
        public static void StoreVector64(short* origin, int* target, uint index)
        {
            if (Sse41.IsSupported)
                Store<int, long>(Sse41.ConvertToVector128Int32(CreateVector128From32(origin, index)), target, index);
        }
        #endregion
        #region T* -> float*
        public static void StoreVector64(byte* origin, float* target, uint index)
        {
            if (Sse41.IsSupported)
                Store<float, long>(ConvertToVector128Single(origin, index), target, index);
        }
        public static void StoreVector64(short* origin, float* target, uint index)
        {
            if (Sse41.IsSupported)
                Store<float, long>(ConvertToVector128Single(origin, index), target, index);
        }
        #endregion
        #endregion

        #region Vector32
        #region T* -> short*
        public static void StoreVector32(byte* origin, short* target, uint index)
        {
            if (Sse41.IsSupported)
                Store<short, int>(Sse41.ConvertToVector128Int16(CreateVector128From16(origin, index)), target, index);
        }
        #endregion
        #endregion
        #endregion

        #region Convert
        #region T* -> float*
        public static Vector128<float> ConvertToVector128Single(byte* origin, uint index)
        {
            if (Sse41.IsSupported)
                return Sse41.ConvertToVector128Single(Sse41.ConvertToVector128Int32(CreateVector128From32(origin, index)));
            return default;
        }
        public static Vector128<float> ConvertToVector128Single(short* origin, uint index)
        {
            if (Sse41.IsSupported)
                return Sse41.ConvertToVector128Single(Sse41.ConvertToVector128Int32(CreateVector128From64(origin, index)));
            return default;
        }
        #endregion
        #region T* -> double*
        public static Vector128<double> ConvertToVector128Double(byte* origin, uint index)
        {
            if (Sse41.IsSupported)
                return Sse41.ConvertToVector128Double(Sse41.ConvertToVector128Int32(CreateVector128From32(origin, index)));
            return default;
        }
        public static Vector128<double> ConvertToVector128Double(short* origin, uint index)
        {
            if (Sse41.IsSupported)
                return Sse41.ConvertToVector128Double(Sse41.ConvertToVector128Int32(CreateVector128From64(origin, index)));
            return default;
        }
        #endregion
        #endregion
    }
}
