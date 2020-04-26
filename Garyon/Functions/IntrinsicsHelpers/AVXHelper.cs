using Garyon.Functions.PointerHelpers;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the AVX CPU instruction set. Every function checks whether the AVX CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public abstract unsafe class AVXHelper : SSE42Helper
    {
        #region Store
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
        public static void Store<TTarget>(Vector256<TTarget> vector, TTarget* target, uint index)
            where TTarget : unmanaged
        {
            StoreVector256((TTarget*)&vector, target + index, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Store<TTarget, TNew>(Vector256<TTarget> vector, TTarget* target, uint index)
            where TTarget : unmanaged
            where TNew : unmanaged
        {
            Store<TTarget, TNew>((TTarget*)&vector, target + index);
        }
        #endregion

        #region Zeroing Out
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ZeroOutVector256<T>(T* pointer, uint index)
            where T : unmanaged
        {
            if (Avx.IsSupported)
                Avx.Store((byte*)&pointer[index], Vector256<byte>.Zero);
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

        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<T> ANDVector256<T>(T* origin, Vector256<T> and, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(int))
                return ANDVector256((int*)origin, and.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return ANDVector256((long*)origin, and.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> ANDVector256(int* origin, Vector256<int> and, uint index)
        {
            return ANDVector256((float*)origin, and.As<int, float>(), index).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<long> ANDVector256(long* origin, Vector256<long> and, uint index)
        {
            return ANDVector256((double*)origin, and.As<long, double>(), index).As<double, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> ANDVector256(float* origin, Vector256<float> and, uint index)
        {
            if (Avx.IsSupported)
                return Avx.And(Avx.LoadVector256(origin + index), and);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ANDVector256(double* origin, Vector256<double> and, uint index)
        {
            if (Avx.IsSupported)
                return Avx.And(Avx.LoadVector256(origin + index), and);
            return default;
        }
        #endregion

        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<T> ORVector256<T>(T* origin, Vector256<T> or, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(int))
                return ORVector256((int*)origin, or.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return ORVector256((long*)origin, or.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> ORVector256(int* origin, Vector256<int> or, uint index)
        {
            return ORVector256((float*)origin, or.As<int, float>(), index).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<long> ORVector256(long* origin, Vector256<long> or, uint index)
        {
            return ORVector256((double*)origin, or.As<long, double>(), index).As<double, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> ORVector256(float* origin, Vector256<float> or, uint index)
        {
            if (Avx.IsSupported)
                return Avx.Or(Avx.LoadVector256(origin + index), or);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ORVector256(double* origin, Vector256<double> or, uint index)
        {
            if (Avx.IsSupported)
                return Avx.Or(Avx.LoadVector256(origin + index), or);
            return default;
        }
        #endregion

        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<T> XORVector256<T>(T* origin, Vector256<T> xor, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(int))
                return XORVector256((int*)origin, xor.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return XORVector256((long*)origin, xor.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> XORVector256(int* origin, Vector256<int> xor, uint index)
        {
            return XORVector256((float*)origin, xor.As<int, float>(), index).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<long> XORVector256(long* origin, Vector256<long> xor, uint index)
        {
            return XORVector256((double*)origin, xor.As<long, double>(), index).As<double, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> XORVector256(float* origin, Vector256<float> xor, uint index)
        {
            if (Avx.IsSupported)
                return Avx.Xor(Avx.LoadVector256(origin + index), xor);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> XORVector256(double* origin, Vector256<double> xor, uint index)
        {
            if (Avx.IsSupported)
                return Avx.Xor(Avx.LoadVector256(origin + index), xor);
            return default;
        }
        #endregion
    }
}
