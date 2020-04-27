using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the SSE CPU instruction set. Every function checks whether the SSE2 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public abstract unsafe class SSEHelper : SIMDIntrinsicsHelper
    {
        #region Store
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector128<T>(T* origin, T* target, uint index)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                Sse.Store((float*)&target[index], Sse.LoadVector128((float*)&origin[index]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Store<TTarget>(Vector128<TTarget> vector, TTarget* target, uint index)
            where TTarget : unmanaged
        {
            StoreVector128((TTarget*)&vector, target + index, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Store<TTarget, TNew>(Vector128<TTarget> vector, TTarget* target, uint index)
            where TTarget : unmanaged
            where TNew : unmanaged
        {
            Store<TTarget, TNew>((TTarget*)&vector, target + index);
        }
        #endregion

        #region Zeroing Out
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ZeroOutVector128<T>(T* pointer, uint index = 0)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                Sse.Store((float*)&pointer[index], Vector128<float>.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ZeroOutLastBytesVector256(byte* pointer, uint index, uint length)
        {
            pointer += index;

            uint count = length - index;

            ZeroOutRemainingElements(16, ref pointer, count);
            ZeroOutLastBytesVector128(pointer, 0, count);

            static void ZeroOutRemainingElements(uint remainder, ref byte* pointer, uint count)
            {
                if ((count & remainder) > 0)
                {
                    ZeroOutVector128(pointer, 0);
                    pointer += remainder;
                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ZeroOutLastBytesVector128(byte* pointer, uint index, uint length)
        {
            pointer += index;

            uint count = length - index;

            ZeroOutRemainingElements<long>(ref pointer, count);
            ZeroOutRemainingElements<int>(ref pointer, count);
            ZeroOutRemainingElements<short>(ref pointer, count);
            ZeroOutRemainingElements<byte>(ref pointer, count);

            static void ZeroOutRemainingElements<T>(ref byte* pointer, uint count)
                where T : unmanaged
            {
                if ((count & sizeof(T)) > 0)
                {
                    *(T*)pointer = default;
                    pointer += (uint)sizeof(T);
                }
            }
        }
        #endregion

        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> ANDVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return ANDVector128((byte*)origin, mask.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return ANDVector128((short*)origin, mask.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(int))
                return ANDVector128((int*)origin, mask.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return ANDVector128((long*)origin, mask.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> ANDVector128(byte* origin, Vector128<byte> mask, uint index)
        {
            return ANDVector128((float*)(origin + index), mask.As<byte, float>(), 0).As<float, byte>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<short> ANDVector128(short* origin, Vector128<short> mask, uint index)
        {
            return ANDVector128((float*)(origin + index), mask.As<short, float>(), 0).As<float, short>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<int> ANDVector128(int* origin, Vector128<int> mask, uint index)
        {
            return ANDVector128((float*)(origin + index), mask.As<int, float>(), 0).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<long> ANDVector128(long* origin, Vector128<long> mask, uint index)
        {
            return ANDVector128((float*)(origin + index), mask.As<long, float>(), 0).As<float, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<float> ANDVector128(float* origin, Vector128<float> mask, uint index)
        {
            if (Sse.IsSupported)
                return Sse.And(Sse.LoadVector128(origin + index), mask);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<double> ANDVector128(double* origin, Vector128<double> mask, uint index)
        {
            return ANDVector128((float*)(origin + index), mask.As<double, float>(), 0).As<float, double>();
        }
        #endregion

        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> ORVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return ORVector128((byte*)origin, mask.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return ORVector128((short*)origin, mask.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(int))
                return ORVector128((int*)origin, mask.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return ORVector128((long*)origin, mask.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> ORVector128(byte* origin, Vector128<byte> mask, uint index)
        {
            return ORVector128((float*)(origin + index), mask.As<byte, float>(), 0).As<float, byte>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<short> ORVector128(short* origin, Vector128<short> mask, uint index)
        {
            return ORVector128((float*)(origin + index), mask.As<short, float>(), 0).As<float, short>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<int> ORVector128(int* origin, Vector128<int> mask, uint index)
        {
            return ORVector128((float*)(origin + index), mask.As<int, float>(), 0).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<long> ORVector128(long* origin, Vector128<long> mask, uint index)
        {
            return ORVector128((float*)(origin + index), mask.As<long, float>(), 0).As<float, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<float> ORVector128(float* origin, Vector128<float> mask, uint index)
        {
            if (Sse.IsSupported)
                return Sse.Or(Sse.LoadVector128(origin + index), mask);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<double> ORVector128(double* origin, Vector128<double> mask, uint index)
        {
            return ORVector128((float*)(origin + index), mask.As<double, float>(), 0).As<float, double>();
        }
        #endregion

        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> XORVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return XORVector128((byte*)origin, mask.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return XORVector128((short*)origin, mask.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(int))
                return XORVector128((int*)origin, mask.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return XORVector128((long*)origin, mask.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<byte> XORVector128(byte* origin, Vector128<byte> mask, uint index)
        {
            return XORVector128((float*)(origin + index), mask.As<byte, float>(), 0).As<float, byte>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<short> XORVector128(short* origin, Vector128<short> mask, uint index)
        {
            return XORVector128((float*)(origin + index), mask.As<short, float>(), 0).As<float, short>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<int> XORVector128(int* origin, Vector128<int> mask, uint index)
        {
            return XORVector128((float*)(origin + index), mask.As<int, float>(), 0).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<long> XORVector128(long* origin, Vector128<long> mask, uint index)
        {
            return XORVector128((float*)(origin + index), mask.As<long, float>(), 0).As<float, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<float> XORVector128(float* origin, Vector128<float> mask, uint index)
        {
            if (Sse.IsSupported)
                return Sse.Xor(Sse.LoadVector128(origin + index), mask);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<double> XORVector128(double* origin, Vector128<double> mask, uint index)
        {
            return XORVector128((float*)(origin + index), mask.As<double, float>(), 0).As<float, double>();
        }
        #endregion

        #region Create
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 64 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provied sequence.</typeparam>
        /// <typeparam name="TReinterpret">The type of the elements that are contained in the <seealso cref="Vector128"/>.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="index">The index at the origin sequence.</param>
        /// <returns>The <seealso cref="Vector128"/> containing the first 64 bits of the sequence, with the rest being zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<TReinterpret> CreateVector128From64<TPointer, TReinterpret>(TPointer* origin, uint index)
            where TPointer : unmanaged
            where TReinterpret : unmanaged
        {
            if (Sse.IsSupported)
                return Vector128.CreateScalar(*(long*)(origin + index)).As<long, TReinterpret>();
            return default;
        }
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 64 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provied sequence.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="index">The index at the origin sequence.</param>
        /// <returns>The <seealso cref="Vector128"/> containing the first 64 bits of the sequence, with the rest being zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<TPointer> CreateVector128From64<TPointer>(TPointer* origin, uint index = 0)
            where TPointer : unmanaged
        {
            return CreateVector128From64<TPointer, TPointer>(origin, index);
        }
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 32 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provied sequence.</typeparam>
        /// <typeparam name="TReinterpret">The type of the elements that are contained in the <seealso cref="Vector128"/>.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="index">The index at the origin sequence.</param>
        /// <returns>The <seealso cref="Vector128"/> containing the first 32 bits of the sequence, with the rest being zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<TReinterpret> CreateVector128From32<TPointer, TReinterpret>(TPointer* origin, uint index = 0)
            where TPointer : unmanaged
            where TReinterpret : unmanaged
        {
            if (Sse.IsSupported)
                return Vector128.CreateScalar(*(int*)(origin + index)).As<int, TReinterpret>();
            return default;
        }
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 32 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provied sequence.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="index">The index at the origin sequence.</param>
        /// <returns>The <seealso cref="Vector128"/> containing the first 32 bits of the sequence, with the rest being zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<TPointer> CreateVector128From32<TPointer>(TPointer* origin, uint index = 0)
            where TPointer : unmanaged
        {
            return CreateVector128From32<TPointer, TPointer>(origin, index);
        }
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 16 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provied sequence.</typeparam>
        /// <typeparam name="TReinterpret">The type of the elements that are contained in the <seealso cref="Vector128"/>.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="index">The index at the origin sequence.</param>
        /// <returns>The <seealso cref="Vector128"/> containing the first 16 bits of the sequence, with the rest being zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<TReinterpret> CreateVector128From16<TPointer, TReinterpret>(TPointer* origin, uint index = 0)
            where TPointer : unmanaged
            where TReinterpret : unmanaged
        {
            if (Sse.IsSupported)
                return Vector128.CreateScalar(*(short*)(origin + index)).As<short, TReinterpret>();
            return default;
        }
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 16 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provied sequence.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="index">The index at the origin sequence.</param>
        /// <returns>The <seealso cref="Vector128"/> containing the first 16 bits of the sequence, with the rest being zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<TPointer> CreateVector128From16<TPointer>(TPointer* origin, uint index = 0)
            where TPointer : unmanaged
        {
            return CreateVector128From16<TPointer, TPointer>(origin, index);
        }
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 8 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provied sequence.</typeparam>
        /// <typeparam name="TReinterpret">The type of the elements that are contained in the <seealso cref="Vector128"/>.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="index">The index at the origin sequence.</param>
        /// <returns>The <seealso cref="Vector128"/> containing the first 8 bits of the sequence, with the rest being zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<TReinterpret> CreateVector128From8<TPointer, TReinterpret>(TPointer* origin, uint index = 0)
            where TPointer : unmanaged
            where TReinterpret : unmanaged
        {
            if (Sse.IsSupported)
                return Vector128.CreateScalar(*(byte*)(origin + index)).As<byte, TReinterpret>();
            return default;
        }
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 8 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provied sequence.</typeparam>
        /// <param name="origin">The origin sequence.</param>
        /// <param name="index">The index at the origin sequence.</param>
        /// <returns>The <seealso cref="Vector128"/> containing the first 8 bits of the sequence, with the rest being zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<TPointer> CreateVector128From8<TPointer>(TPointer* origin, uint index = 0)
            where TPointer : unmanaged
        {
            return CreateVector128From8<TPointer, TPointer>(origin, index);
        }
        #endregion
    }
}
