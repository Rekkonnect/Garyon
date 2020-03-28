using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the AVX2 CPU instruction set. Every function checks whether the AVX2 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public abstract unsafe class AVX2Helper : AVXHelper
    {
        #region Store
        #region Vector256
        #region T* -> short*
        public static void StoreVector256(byte* origin, short* target, uint index)
        {
            if (Avx2.IsSupported)
                Avx2.Store(&target[index], Avx2.ConvertToVector256Int16(&origin[index]));
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
        #endregion
        #endregion
    }
}
