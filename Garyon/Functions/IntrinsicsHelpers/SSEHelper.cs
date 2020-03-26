using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the SSE CPU instruction set. Every function checks whether the SSE2 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public unsafe class SSEHelper : SIMDIntrinsicsHelper
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
        public static void Store<TTarget, TNew>(Vector128<TTarget> vector, TTarget* target, uint index)
            where TTarget : unmanaged
            where TNew : unmanaged
        {
            Store<TTarget, TTarget, TNew>((TTarget*)&vector, target + index);
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
                return Vector128.Create(*(long*)(origin + index), 0).As<long, TReinterpret>();
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
                return Vector128.Create(*(int*)(origin + index), 0, 0, 0).As<int, TReinterpret>();
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
                return Vector128.Create(*(short*)(origin + index), 0, 0, 0, 0, 0, 0, 0).As<short, TReinterpret>();
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
                return Vector128.Create(*(byte*)(origin + index), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).As<byte, TReinterpret>();
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
