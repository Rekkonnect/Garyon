using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the SSE CPU instruction set. Every function checks whether the SSE2 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public abstract unsafe class SSEHelper : SIMDIntrinsicsHelper
    {
        protected static readonly Vector128<byte> Vector128Max = Vector128.Create(byte.MaxValue);

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

        #region Bitwise Operations
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> NOTVector128<T>(T* origin, uint index)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                return NOTVector128(Sse.LoadVector128((float*)(origin + index))).As<float, T>();
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> NOTVector128<T>(Vector128<T> vector)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                return Sse.Xor(vector.AsSingle(), Vector128Max.AsSingle()).As<float, T>();
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> ANDVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                return Sse.And(Sse.LoadVector128((float*)(origin + index)), mask.AsSingle()).As<float, T>();
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> ORVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                return Sse.Or(Sse.LoadVector128((float*)(origin + index)), mask.AsSingle()).As<float, T>();
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> XORVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                return Sse.Xor(Sse.LoadVector128((float*)(origin + index)), mask.AsSingle()).As<float, T>();
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> NANDVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                return NOTVector128(ANDVector128(origin, mask, index));
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> NORVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                return NOTVector128(ORVector128(origin, mask, index));
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<T> XNORVector128<T>(T* origin, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            if (Sse.IsSupported)
                return NOTVector128(XORVector128(origin, mask, index));
            return default;
        }
        #endregion

        #region Create
        /// <summary>Creates a new <seealso cref="Vector128"/> out of the first 64 bits of the provided sequence at the specified index.</summary>
        /// <typeparam name="TPointer">The type of the elements in the provided sequence.</typeparam>
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
        /// <typeparam name="TPointer">The type of the elements in the provided sequence.</typeparam>
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
        /// <typeparam name="TPointer">The type of the elements in the provided sequence.</typeparam>
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
        /// <typeparam name="TPointer">The type of the elements in the provided sequence.</typeparam>
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
        /// <typeparam name="TPointer">The type of the elements in the provided sequence.</typeparam>
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
        /// <typeparam name="TPointer">The type of the elements in the provided sequence.</typeparam>
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
        /// <typeparam name="TPointer">The type of the elements in the provided sequence.</typeparam>
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
        /// <typeparam name="TPointer">The type of the elements in the provided sequence.</typeparam>
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
