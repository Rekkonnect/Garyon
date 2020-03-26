using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the SSE4.1 CPU instruction set. Every function checks whether the SSE4.1 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public unsafe class SSE41Helper : SSSE3Helper
    {
        #region Store
        #region Vector128
        #region T* -> short*
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

            StoreRemainingElements(8);
            StoreLastElementsVector128(origin, target, index, length);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder);
                    index |= remainder;
                }
            }
            void StoreRemainingByte(uint remainder)
            {
                if (remainder == 8)
                    StoreVector128((byte*)origin, target, index);
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

            StoreRemainingElements(4);
            StoreRemainingElements(2);
            StoreRemainingElements(1);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder);
                    index |= remainder;
                }
            }
            void StoreRemainingByte(uint remainder)
            {
                byte* originByte = (byte*)origin;
                if (remainder == 4)
                    StoreVector64(originByte, target, index);
                if (remainder == 2)
                    StoreVector32(originByte, target, index);
                if (remainder == 1)
                    StoreVector16(originByte, target, index);
            }
        }
        #endregion
        #region T* -> int*
        public static void StoreVector128(byte* origin, int* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int32(CreateVector128From32(origin, index)));
        }
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

            StoreRemainingElements(4);
            StoreLastElementsVector128(origin, target, index, length);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreVector128((byte*)origin, target, index);
                    else if (typeof(T) == typeof(short))
                        StoreVector128((short*)origin, target, index);
                    else if (typeof(T) == typeof(float))
                        StoreVector128((float*)origin, target, index);
                    else if (typeof(T) == typeof(double))
                        StoreVector128((double*)origin, target, index);
                    index |= remainder;
                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128<T>(T* origin, int* target, uint index, uint length)
            where T : unmanaged
        {
            if (typeof(T) == typeof(float))
                SSE2Helper.StoreLastElementsVector128((float*)origin, target, index, length);
            if (typeof(T) == typeof(double))
                SIMDIntrinsicsHelper.StoreLastElementsVector128((double*)origin, target, index, length);

            if (!Sse41.IsSupported)
                return;

            StoreRemainingElements(2);
            StoreRemainingElements(1);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder);
                    else if (typeof(T) == typeof(short))
                        StoreRemainingInt16(remainder);
                    index |= remainder;
                }
            }
            void StoreRemainingByte(uint remainder)
            {
                if (remainder == 2)
                    StoreVector64((byte*)origin, target, index);
                if (remainder == 1)
                    target[index] = ((byte*)origin)[index];
            }
            void StoreRemainingInt16(uint remainder)
            {
                if (remainder == 2)
                    StoreVector64((short*)origin, target, index);
                if (remainder == 1)
                    target[index] = ((short*)origin)[index];
            }
        }
        #endregion
        #region T* -> long*
        public static void StoreVector128(byte* origin, long* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int64(CreateVector128From16(origin, index)));
        }
        public static void StoreVector128(short* origin, long* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int64(CreateVector128From32(origin, index)));
        }
        public static void StoreVector128(int* origin, long* target, uint index)
        {
            if (Sse41.IsSupported)
                Sse41.Store(&target[index], Sse41.ConvertToVector128Int64(CreateVector128From64(origin, index)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector256<T>(T* origin, long* target, uint index, uint length)
            where T : unmanaged
        {
            StoreRemainingElements(2);
            StoreLastElementsVector128(origin, target, index, length);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreVector128((byte*)origin, target, index);
                    else if (typeof(T) == typeof(short))
                        StoreVector128((short*)origin, target, index);
                    else if (typeof(T) == typeof(int))
                        StoreVector128((int*)origin, target, index);
                    index |= remainder;
                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128<T>(T* origin, long* target, uint index, uint length)
            where T : unmanaged
        {
            StoreRemainingElements(1);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        target[index] = ((byte*)origin)[index];
                    else if (typeof(T) == typeof(short))
                        target[index] = ((short*)origin)[index];
                    else if (typeof(T) == typeof(int))
                        target[index] = ((int*)origin)[index];
                    index |= remainder;
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

            StoreRemainingElements(4);
            StoreLastElementsVector128(origin, target, index, length);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder);
                    else if (typeof(T) == typeof(short))
                        StoreRemainingInt16(remainder);
                    index |= remainder;
                }
            }
            void StoreRemainingByte(uint remainder)
            {
                if (remainder == 4)
                    StoreVector128((byte*)origin, target, index);
            }
            void StoreRemainingInt16(uint remainder)
            {
                if (remainder == 4)
                    StoreVector128((short*)origin, target, index);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreLastElementsVector128<T>(T* origin, float* target, uint index, uint length)
            where T : unmanaged
        {
            if (!Sse41.IsSupported)
                return;

            StoreRemainingElements(2);
            StoreRemainingElements(1);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreRemainingByte(remainder);
                    else if (typeof(T) == typeof(short))
                        StoreRemainingInt16(remainder);
                    index |= remainder;
                }
            }
            void StoreRemainingByte(uint remainder)
            {
                if (remainder == 2)
                    StoreVector64((byte*)origin, target, index);
                if (remainder == 1)
                    target[index] = ((byte*)origin)[index];
            }
            void StoreRemainingInt16(uint remainder)
            {
                if (remainder == 2)
                    StoreVector64((short*)origin, target, index);
                if (remainder == 1)
                    target[index] = ((short*)origin)[index];
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
            StoreRemainingElements(2);

            StoreLastElementsVector128(origin, target, index, length);

            void StoreRemainingElements(uint remainder)
            {
                if ((length & remainder) > 0)
                {
                    if (typeof(T) == typeof(byte))
                        StoreVector128((byte*)origin, target, index);
                    else if (typeof(T) == typeof(short))
                        StoreVector128((short*)origin, target, index);
                    else if (typeof(T) == typeof(int))
                        StoreVector128((int*)origin, target, index);
                    else if (typeof(T) == typeof(float))
                        StoreVector128((float*)origin, target, index);
                    index |= remainder;
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
