using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the AVX2 CPU instruction set. Every function checks whether the AVX2 CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public unsafe class AVX2Helper : AVXHelper
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
        #region T* -> byte*
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<byte> ConvertToVector256Byte(short* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<byte>(origin + index, ShuffleMaskVector256i16i8);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<byte> ConvertToVector256Byte(int* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<byte>(origin + index, ShuffleMaskVector256i32i8);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<byte> ConvertToVector256Byte(long* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<byte>(origin + index, ShuffleMaskVector256i64i8);
            return default;
        }
        #endregion
        #region T* -> short*
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<short> ConvertToVector256Int16(int* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<short>(origin + index, ShuffleMaskVector256i32i16);
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<short> ConvertToVector256Int16(long* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<short>(origin + index, ShuffleMaskVector256i64i16);
            return default;
        }
        #endregion
        #region T* -> int*
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<int> ConvertToVector256Int32(long* origin, uint index)
        {
            if (!Avx2.IsSupported)
                return ConvertToVector256<int>(origin + index, ShuffleMaskVector256i64i32);
            return default;
        }
        #endregion
        #region T* -> float*
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<float> ConvertToVector256Single(byte* origin, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.ConvertToVector256Single(Avx2.ConvertToVector256Int32(&origin[index]));
            return default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<float> ConvertToVector256Single(short* origin, uint index)
        {
            if (Avx2.IsSupported)
                return Avx2.ConvertToVector256Single(Avx2.ConvertToVector256Int32(&origin[index]));
            return default;
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<T> ConvertToVector256<T>(void* origin, Vector256<byte> shuffleMask)
            where T : unmanaged
        {
            if (Avx2.IsSupported)
                return Avx2.Shuffle(Avx2.LoadVector256((byte*)origin), shuffleMask).As<byte, T>();
            return default;
        }
        #endregion
        #endregion
    }
}
