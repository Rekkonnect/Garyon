using Garyon.Functions.IntrinsicsHelpers;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Extensions.ArrayExtensions
{
    /// <summary>Contains unsafe helper functions for array zeroing using specialized CPU instructions. All functions check whether the minimum supported instruction set is included; in the case that the set is unavailable, the functions simply do nothing.</summary>
    public static unsafe class UnsafeArrayZeroingHelpers
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ZeroOutArray<T>(T* origin, uint length)
            where T : unmanaged
        {
            return ZeroOutArray((byte*)origin, length * (uint)sizeof(T));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ZeroOutArray(byte* origin, uint length)
        {
            if (ZeroOutByteArrayVector256(origin, length))
                return true;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ZeroOutByteArrayVector256(byte* origin, uint length)
        {
            if (!Sse.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector256<byte>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                AVXHelper.ZeroOutVector256(origin, i);
            AVXHelper.ZeroOutLastBytesVector256(origin, i, length);

            return true;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ZeroOutByteArrayVector128(byte* origin, uint length)
        {
            if (!Sse.IsSupported)
                return false;

            uint i = 0;
            uint size = (uint)Vector128<byte>.Count;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                SSEHelper.ZeroOutVector128(origin, i);
            SSEHelper.ZeroOutLastBytesVector128(origin, i, length);

            return true;
        }
    }
}