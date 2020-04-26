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

        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static new Vector256<T> ANDVector256<T>(T* origin, Vector256<T> and, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return ANDVector256((byte*)origin, and.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return ANDVector256((short*)origin, and.As<T, short>(), index).As<short, T>();

            return AVXHelper.ANDVector256(origin, and, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> ANDVector256(byte* origin, Vector256<byte> and, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.And(Avx.LoadVector256(origin + index), and);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> ANDVector256(short* origin, Vector256<short> and, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.And(Avx.LoadVector256(origin + index), and);
            return default;
        }
        #endregion

        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static new Vector256<T> ORVector256<T>(T* origin, Vector256<T> or, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return ORVector256((byte*)origin, or.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return ORVector256((short*)origin, or.As<T, short>(), index).As<short, T>();

            return AVXHelper.ORVector256(origin, or, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> ORVector256(byte* origin, Vector256<byte> or, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.Or(Avx.LoadVector256(origin + index), or);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> ORVector256(short* origin, Vector256<short> or, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.Or(Avx.LoadVector256(origin + index), or);
            return default;
        }
        #endregion

        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static new Vector256<T> XORVector256<T>(T* origin, Vector256<T> xor, uint index)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
                return XORVector256((byte*)origin, xor.As<T, byte>(), index).As<byte, T>();
            if (sizeof(T) == sizeof(short))
                return XORVector256((short*)origin, xor.As<T, short>(), index).As<short, T>();

            return AVXHelper.XORVector256(origin, xor, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<byte> XORVector256(byte* origin, Vector256<byte> xor, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.Xor(Avx.LoadVector256(origin + index), xor);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<short> XORVector256(short* origin, Vector256<short> xor, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.Xor(Avx.LoadVector256(origin + index), xor);
            return default;
        }
        #endregion
    }
}
