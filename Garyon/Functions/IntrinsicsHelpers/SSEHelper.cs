using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the SSE CPU instruction set. Every function checks whether the SSE2 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public unsafe class SSEHelper : SIMDIntrinsicsHelper
    {
        #region Vector128
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
    }
}
