using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Garyon.Functions.IntrinsicsHelpers;

namespace Garyon.Functions.PointerHelpers
{
    /// <summary>Contains unsafe helper functions for binary operations using SIMD. All functions check whether the minimum supported instruction set is included; in the case that the set is unavailable, the functions simply do nothing.</summary>
    public static unsafe class SIMDPointerBinaryOperations
    {
        #region Vector256
        #region NOT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NOTArrayVector256CustomType<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            switch (sizeof(T))
            {
                case sizeof(byte):
                case sizeof(short):
                case sizeof(int):
                case sizeof(long):
                    return NOTArrayVector256(origin, target, length);
            }
            return NOTArrayVector256((byte*)origin, (byte*)target, length * (uint)sizeof(T));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NOTArrayVector256<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                PerformCurrentNOTIterationVector256(origin, target, i);
            NOTLastElementsVector256(origin, target, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNOTIterationVector256<T>(T* origin, T* target, uint index)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.NOTVector256(origin, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NOTLastElementsVector256<T>(T* origin, T* target, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = AVXHelper.NOTVector256(origin, index);
            SSE2Helper.StoreLastElementsVector256((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ANDArrayVector256<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentANDIterationVector256(origin, target, maskVector, i);
            ANDLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentANDIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.ANDVector256(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ANDLastElementsVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = AVXHelper.ANDVector256(origin, mask, index);
            SSE2Helper.StoreLastElementsVector256((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ORArrayVector256<T>(T* origin, T* target, T or, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(or);
            for (; i < limit; i += size)
                PerformCurrentORIterationVector256(origin, target, maskVector, i);
            ORLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentORIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.ORVector256(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ORLastElementsVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = AVXHelper.ORVector256(origin, mask, index);
            SSE2Helper.StoreLastElementsVector256((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector256<T>(T* origin, T* target, T xor, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(xor);
            for (; i < limit; i += size)
                PerformCurrentXORIterationVector256(origin, target, maskVector, i);
            XORLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentXORIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.XORVector256(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void XORLastElementsVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = AVXHelper.XORVector256(origin, mask, index);
            SSE2Helper.StoreLastElementsVector256((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region NAND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NANDArrayVector256<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
            for (; i < limit; i += size)
                PerformCurrentNANDIterationVector256(origin, target, maskVector, i);
            NANDLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNANDIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.NANDVector256(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NANDLastElementsVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = AVXHelper.NANDVector256(origin, mask, index);
            SSE2Helper.StoreLastElementsVector256((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region NOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NORArrayVector256<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
            for (; i < limit; i += size)
                PerformCurrentNORIterationVector256(origin, target, maskVector, i);
            NORLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNORIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.NORVector256(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NORLastElementsVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = AVXHelper.NORVector256(origin, mask, index);
            SSE2Helper.StoreLastElementsVector256((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region XNOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XNORArrayVector256<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
            for (; i < limit; i += size)
                PerformCurrentXNORIterationVector256(origin, target, maskVector, i);
            XNORLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentXNORIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.XNORVector256(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void XNORLastElementsVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = AVXHelper.XNORVector256(origin, mask, index);
            SSE2Helper.StoreLastElementsVector256((T*)&masked, target + index, 0, count);
        }
        #endregion
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetSupportedInstructionSetVector256<T>()
            where T : unmanaged
        {
            return Avx.IsSupported;
        }
        #endregion

        #region Vector128
        #region NOT
        /// <summary>Performs the bitwise NOT operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="NOTArrayVector128CustomType{T}(T*, T*, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the NOT operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NOTArrayVector128CustomType<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            switch (sizeof(T))
            {
                case sizeof(byte):
                case sizeof(short):
                case sizeof(int):
                case sizeof(long):
                    return NOTArrayVector128(origin, target, length);
            }
            return NOTArrayVector128((byte*)origin, (byte*)target, length * (uint)sizeof(T));
        }
        /// <summary>Performs the bitwise NOT operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="NOTArrayVector128CustomType{T}(T*, T*, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the NOT operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NOTArrayVector128<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                PerformCurrentNOTIterationVector128(origin, target, i);
            NOTLastElementsVector128(origin, target, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNOTIterationVector128<T>(T* origin, T* target, uint index)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.NOTVector128(origin, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NOTLastElementsVector128<T>(T* origin, T* target, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = SSEHelper.NOTVector128(origin, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ANDArrayVector128<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentANDIterationVector128(origin, target, maskVector, i);
            ANDLastElementsVector128(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentANDIterationVector128<T>(T* origin, T* target, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.ANDVector128(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ANDLastElementsVector128<T>(T* origin, T* target, Vector128<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = SSEHelper.ANDVector128(origin, mask, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ORArrayVector128<T>(T* origin, T* target, T or, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(or);
            for (; i < limit; i += size)
                PerformCurrentORIterationVector128(origin, target, maskVector, i);
            ORLastElementsVector128(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentORIterationVector128<T>(T* origin, T* target, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.ORVector128(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ORLastElementsVector128<T>(T* origin, T* target, Vector128<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = SSEHelper.ORVector128(origin, mask, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector128<T>(T* origin, T* target, T xor, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(xor);
            for (; i < limit; i += size)
                PerformCurrentXORIterationVector128(origin, target, maskVector, i);
            XORLastElementsVector128(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentXORIterationVector128<T>(T* origin, T* target, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.XORVector128(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void XORLastElementsVector128<T>(T* origin, T* target, Vector128<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = SSEHelper.XORVector128(origin, mask, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region NAND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NANDArrayVector128<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentNANDIterationVector128(origin, target, maskVector, i);
            NANDLastElementsVector128(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNANDIterationVector128<T>(T* origin, T* target, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.NANDVector128(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NANDLastElementsVector128<T>(T* origin, T* target, Vector128<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = SSEHelper.NANDVector128(origin, mask, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region NOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NORArrayVector128<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentNORIterationVector128(origin, target, maskVector, i);
            NORLastElementsVector128(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNORIterationVector128<T>(T* origin, T* target, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.NORVector128(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NORLastElementsVector128<T>(T* origin, T* target, Vector128<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = SSEHelper.NORVector128(origin, mask, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&masked, target + index, 0, count);
        }
        #endregion
        #region XNOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XNORArrayVector128<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentXNORIterationVector128(origin, target, maskVector, i);
            XNORLastElementsVector128(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentXNORIterationVector128<T>(T* origin, T* target, Vector128<T> mask, uint index)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.XNORVector128(origin, mask, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void XNORLastElementsVector128<T>(T* origin, T* target, Vector128<T> mask, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var masked = SSEHelper.XNORVector128(origin, mask, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&masked, target + index, 0, count);
        }
        #endregion
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetSupportedInstructionSetVector128<T>()
            where T : unmanaged
        {
            return Sse.IsSupported;
        }
        #endregion
    }
}