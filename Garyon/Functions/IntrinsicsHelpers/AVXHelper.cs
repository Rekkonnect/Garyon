using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the AVX CPU instruction set. Every function checks whether the AVX CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public unsafe class AVXHelper : SSE42Helper
    {
        #region Vector256
        #region T* -> int*
        public static void StoreVector256(float* origin, int* target, uint index)
        {
            if (Avx.IsSupported)
                Avx.Store(&target[index], ConvertToVector256Int32(origin, index));
        }
        public static void StoreVector256(double* origin, int* target, uint index)
        {
            if (Avx.IsSupported)
                Avx.Store(&target[index], ConvertToVector128Int32(origin, index));
        }
        #endregion
        #region T* -> float*
        public static void StoreVector256(int* origin, float* target, uint index)
        {
            if (Avx.IsSupported)
                Avx.Store(&target[index], ConvertToVector256Single(origin, index));
        }
        public static void StoreVector128(double* origin, float* target, uint index)
        {
            if (Avx.IsSupported)
                Avx.Store(&target[index], ConvertToVector128Single(origin, index));
        }
        #endregion
        #region T* -> double*
        public static void StoreVector256(byte* origin, double* target, uint index)
        {
            if (Avx.IsSupported)
                Avx.Store(&target[index], ConvertToVector256Double(origin, index));
        }
        public static void StoreVector256(short* origin, double* target, uint index)
        {
            if (Avx.IsSupported)
                Avx.Store(&target[index], ConvertToVector256Double(origin, index));
        }
        public static void StoreVector256(int* origin, double* target, uint index)
        {
            if (Avx.IsSupported)
                Avx.Store(&target[index], ConvertToVector256Double(origin, index));
        }
        public static void StoreVector256(float* origin, double* target, uint index)
        {
            if (Avx.IsSupported)
                Avx.Store(&target[index], ConvertToVector256Double(origin, index));
        }
        #endregion
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StoreVector256<T>(T* origin, T* target, uint index)
            where T : unmanaged
        {
            if (Avx.IsSupported)
                Avx.Store((byte*)&target[index], Avx.LoadVector256((byte*)&origin[index]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Store<TTarget, TNew>(Vector256<TTarget> vector, TTarget* target, uint index)
            where TTarget : unmanaged
            where TNew : unmanaged
        {
            Store<TTarget, TTarget, TNew>((TTarget*)&vector, target + index);
        }
        #endregion

        #region Convert
        #region Vector256
        #region T* -> int*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> ConvertToVector256Int32(float* origin, uint index)
        {
            if (Avx.IsSupported)
                return Avx.ConvertToVector256Int32(Avx.LoadVector256(&origin[index]));
            return default;
        }
        #endregion
        #region T* -> float*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> ConvertToVector256Single(int* origin, uint index)
        {
            if (Avx.IsSupported)
                return Avx.ConvertToVector256Single(Avx.LoadVector256(&origin[index]));
            return default;
        }
        #endregion
        #region T* -> double*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(byte* origin, uint index)
        {
            if (Avx.IsSupported)
                return Avx.ConvertToVector256Double(Avx.ConvertToVector128Int32(&origin[index]));
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(short* origin, uint index)
        {
            if (Avx.IsSupported)
                return Avx.ConvertToVector256Double(Avx.ConvertToVector128Int32(&origin[index]));
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(int* origin, uint index)
        {
            if (Avx.IsSupported)
                return Avx.ConvertToVector256Double(Avx.LoadVector128(&origin[index]));
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ConvertToVector256Double(float* origin, uint index)
        {
            if (Avx.IsSupported)
                return Avx.ConvertToVector256Double(Avx.LoadVector128(&origin[index]));
            return default;
        }
        #endregion
        #endregion

        #region Vector128
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<int> ConvertToVector128Int32(double* origin, uint index)
        {
            if (Avx.IsSupported)
                return Avx.ConvertToVector128Int32(Avx.LoadVector256(&origin[index]));
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector128<float> ConvertToVector128Single(double* origin, uint index)
        {
            if (Avx.IsSupported)
                return Avx.ConvertToVector128Single(Avx.LoadVector256(&origin[index]));
            return default;
        }
        #endregion
        #endregion
    }
}
