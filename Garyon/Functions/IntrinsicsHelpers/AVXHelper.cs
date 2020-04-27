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
        public static Vector256<T> ANDVector256<T>(T* origin, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return ANDVector256((byte*)origin, mask.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return ANDVector256((short*)origin, mask.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(int))
                return ANDVector256((int*)origin, mask.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return ANDVector256((long*)origin, mask.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> ANDVector256(byte* origin, Vector256<byte> mask, uint index)
        {
            return ANDVector256((float*)(origin + index), mask.As<byte, float>(), 0).As<float, byte>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> ANDVector256(short* origin, Vector256<short> mask, uint index)
        {
            return ANDVector256((float*)(origin + index), mask.As<short, float>(), 0).As<float, short>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> ANDVector256(int* origin, Vector256<int> mask, uint index)
        {
            return ANDVector256((float*)(origin + index), mask.As<int, float>(), 0).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<long> ANDVector256(long* origin, Vector256<long> mask, uint index)
        {
            return ANDVector256((float*)(origin + index), mask.As<long, float>(), 0).As<float, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> ANDVector256(float* origin, Vector256<float> mask, uint index)
        {
            if (Avx.IsSupported)
                return Avx.And(Avx.LoadVector256(origin + index), mask);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ANDVector256(double* origin, Vector256<double> mask, uint index)
        {
            return ANDVector256((float*)(origin + index), mask.As<double, float>(), 0).As<float, double>();
        }
        #endregion

        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<T> ORVector256<T>(T* origin, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return ORVector256((byte*)origin, mask.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return ORVector256((short*)origin, mask.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(int))
                return ORVector256((int*)origin, mask.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return ORVector256((long*)origin, mask.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> ORVector256(byte* origin, Vector256<byte> mask, uint index)
        {
            return ORVector256((float*)(origin + index), mask.As<byte, float>(), 0).As<float, byte>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> ORVector256(short* origin, Vector256<short> mask, uint index)
        {
            return ORVector256((float*)(origin + index), mask.As<short, float>(), 0).As<float, short>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> ORVector256(int* origin, Vector256<int> mask, uint index)
        {
            return ORVector256((float*)(origin + index), mask.As<int, float>(), 0).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<long> ORVector256(long* origin, Vector256<long> mask, uint index)
        {
            return ORVector256((float*)(origin + index), mask.As<long, float>(), 0).As<float, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> ORVector256(float* origin, Vector256<float> mask, uint index)
        {
            if (Avx.IsSupported)
                return Avx.Or(Avx.LoadVector256(origin + index), mask);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> ORVector256(double* origin, Vector256<double> mask, uint index)
        {
            return ORVector256((float*)(origin + index), mask.As<double, float>(), 0).As<float, double>();
        }
        #endregion

        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<T> XORVector256<T>(T* origin, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return XORVector256((byte*)origin, mask.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return XORVector256((short*)origin, mask.As<T, short>(), index).As<short, T>();
            if (sizeof(T) == sizeof(int))
                return XORVector256((int*)origin, mask.As<T, int>(), index).As<int, T>();
            if (sizeof(T) == sizeof(long))
                return XORVector256((long*)origin, mask.As<T, long>(), index).As<long, T>();

            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> XORVector256(byte* origin, Vector256<byte> mask, uint index)
        {
            return XORVector256((float*)(origin + index), mask.As<byte, float>(), 0).As<float, byte>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> XORVector256(short* origin, Vector256<short> mask, uint index)
        {
            return XORVector256((float*)(origin + index), mask.As<short, float>(), 0).As<float, short>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<int> XORVector256(int* origin, Vector256<int> mask, uint index)
        {
            return XORVector256((float*)(origin + index), mask.As<int, float>(), 0).As<float, int>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<long> XORVector256(long* origin, Vector256<long> mask, uint index)
        {
            return XORVector256((float*)(origin + index), mask.As<long, float>(), 0).As<float, long>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<float> XORVector256(float* origin, Vector256<float> mask, uint index)
        {
            if (Avx.IsSupported)
                return Avx.Xor(Avx.LoadVector256(origin + index), mask);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<double> XORVector256(double* origin, Vector256<double> mask, uint index)
        {
            return XORVector256((float*)(origin + index), mask.As<double, float>(), 0).As<float, double>();
        }
        #endregion

    }
}
