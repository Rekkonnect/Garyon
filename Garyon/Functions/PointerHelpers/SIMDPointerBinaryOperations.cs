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
        #region Decide on keeping or not
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector256<T>(T* origin, T* target, T xor, uint length)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
            {

            }
            if (sizeof(T) == sizeof(short))
            {

            }
            if (sizeof(T) == sizeof(int))
            {

            }
            if (sizeof(T) == sizeof(long))
            {

            }

            return false;
        }

        // TODO: Do something about non-binary-sized data types
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToArrayVector256<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (CopyToArrayVector256<T, long>(origin, target, length))
                return true;
            if (CopyToArrayVector256<T, int>(origin, target, length))
                return true;
            if (CopyToArrayVector256<T, short>(origin, target, length))
                return true;
            return CopyToArrayVector256<T, byte>(origin, target, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CopyToArrayVector256<TPointer, TReinterpret>(TPointer* origin, TPointer* target, uint length)
            where TPointer : unmanaged
            where TReinterpret : unmanaged
        {
            if (sizeof(TPointer) % sizeof(TReinterpret) == 0)
                return CopyToReinterpretedArrayVector256((TReinterpret*)origin, (TReinterpret*)target, length * (uint)sizeof(TPointer) / (uint)sizeof(TReinterpret));
            return false;
        }
        private static bool CopyToReinterpretedArrayVector256<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            // ?
            return false;
        }
        #endregion

        #region NOT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NOTArrayVector256Generic<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                PerformCurrentNOTIterationVector256(origin, target, i, length);
            NOTLastElementsVector256(origin, target, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNOTIterationVector256<T>(T* origin, T* target, uint index, uint length)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.NOTVector256(origin, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NOTLastElementsVector256<T>(T* origin, T* target, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var anded = AVXHelper.NOTVector256(origin, index);
            SSE2Helper.StoreLastElementsVector256((T*)&anded, target + index, 0, count);
        }
        #endregion
        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ANDArrayVector256Generic<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var andVector = Vector256Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentANDIterationVector256(origin, target, andVector, i, length);
            ANDLastElementsVector256(origin, target, andVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentANDIterationVector256<T>(T* origin, T* target, Vector256<T> and, uint index, uint length)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.ANDVector256(origin, and, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ANDLastElementsVector256<T>(T* origin, T* target, Vector256<T> and, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var anded = AVXHelper.ANDVector256(origin, and, index);
            SSE2Helper.StoreLastElementsVector256((T*)&anded, target + index, 0, count);
        }
        #endregion
        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ORArrayVector256Generic<T>(T* origin, T* target, T or, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var orVector = Vector256Helper.Create(or);
            for (; i < limit; i += size)
                PerformCurrentORIterationVector256(origin, target, orVector, i, length);
            ORLastElementsVector256(origin, target, orVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentORIterationVector256<T>(T* origin, T* target, Vector256<T> or, uint index, uint length)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.ORVector256(origin, or, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ORLastElementsVector256<T>(T* origin, T* target, Vector256<T> or, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var ored = AVXHelper.ORVector256(origin, or, index);
            SSE2Helper.StoreLastElementsVector256((T*)&ored, target + index, 0, count);
        }
        #endregion
        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector256Generic<T>(T* origin, T* target, T xor, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var xorVector = Vector256Helper.Create(xor);
            for (; i < limit; i += size)
                PerformCurrentXORIterationVector256(origin, target, xorVector, i, length);
            XORLastElementsVector256(origin, target, xorVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentXORIterationVector256<T>(T* origin, T* target, Vector256<T> xor, uint index, uint length)
            where T : unmanaged
        {
            AVXHelper.Store(AVXHelper.XORVector256(origin, xor, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void XORLastElementsVector256<T>(T* origin, T* target, Vector256<T> xor, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var xored = AVXHelper.XORVector256(origin, xor, index);
            SSE2Helper.StoreLastElementsVector256((T*)&xored, target + index, 0, count);
        }
        #endregion
        #region NAND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NANDArrayVector256Generic<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
            for (; i < limit; i += size)
                PerformCurrentNANDIterationVector256(origin, target, maskVector, i, length);
            NANDLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNANDIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
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
        public static bool NORArrayVector256Generic<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
            for (; i < limit; i += size)
                PerformCurrentNORIterationVector256(origin, target, maskVector, i, length);
            NORLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNORIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
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
        public static bool XNORArrayVector256Generic<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
            for (; i < limit; i += size)
                PerformCurrentXNORIterationVector256(origin, target, maskVector, i, length);
            XNORLastElementsVector256(origin, target, maskVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentXNORIterationVector256<T>(T* origin, T* target, Vector256<T> mask, uint index, uint length)
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
        #region Decide on keeping or not
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector128<T>(T* origin, T* target, T xor, uint length)
            where T : unmanaged
        {
            if (sizeof(T) == sizeof(byte))
            {

            }
            if (sizeof(T) == sizeof(short))
            {

            }
            if (sizeof(T) == sizeof(int))
            {

            }
            if (sizeof(T) == sizeof(long))
            {

            }

            return false;
        }

        // TODO: Do something about non-binary-sized data types
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyToArrayVector128<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (CopyToArrayVector128<T, long>(origin, target, length))
                return true;
            if (CopyToArrayVector128<T, int>(origin, target, length))
                return true;
            if (CopyToArrayVector128<T, short>(origin, target, length))
                return true;
            return CopyToArrayVector128<T, byte>(origin, target, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CopyToArrayVector128<TPointer, TReinterpret>(TPointer* origin, TPointer* target, uint length)
            where TPointer : unmanaged
            where TReinterpret : unmanaged
        {
            if (sizeof(TPointer) % sizeof(TReinterpret) == 0)
                return CopyToReinterpretedArrayVector128((TReinterpret*)origin, (TReinterpret*)target, length * (uint)sizeof(TPointer) / (uint)sizeof(TReinterpret));
            return false;
        }
        private static bool CopyToReinterpretedArrayVector128<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            // ?
            return false;
        }
        #endregion

        #region NOT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NOTArrayVector128Generic<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            for (; i < limit; i += size)
                PerformCurrentNOTIterationVector128(origin, target, i, length);
            NOTLastElementsVector128(origin, target, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNOTIterationVector128<T>(T* origin, T* target, uint index, uint length)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.NOTVector128(origin, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NOTLastElementsVector128<T>(T* origin, T* target, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var anded = SSEHelper.NOTVector128(origin, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&anded, target + index, 0, count);
        }
        #endregion
        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ANDArrayVector128Generic<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var andVector = Vector128Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentANDIterationVector128(origin, target, andVector, i, length);
            ANDLastElementsVector128(origin, target, andVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentANDIterationVector128<T>(T* origin, T* target, Vector128<T> and, uint index, uint length)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.ANDVector128(origin, and, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ANDLastElementsVector128<T>(T* origin, T* target, Vector128<T> and, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var anded = SSEHelper.ANDVector128(origin, and, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&anded, target + index, 0, count);
        }
        #endregion
        #region OR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ORArrayVector128Generic<T>(T* origin, T* target, T or, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var orVector = Vector128Helper.Create(or);
            for (; i < limit; i += size)
                PerformCurrentORIterationVector128(origin, target, orVector, i, length);
            ORLastElementsVector128(origin, target, orVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentORIterationVector128<T>(T* origin, T* target, Vector128<T> or, uint index, uint length)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.ORVector128(origin, or, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ORLastElementsVector128<T>(T* origin, T* target, Vector128<T> or, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var ored = SSEHelper.ORVector128(origin, or, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&ored, target + index, 0, count);
        }
        #endregion
        #region XOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector128Generic<T>(T* origin, T* target, T xor, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var xorVector = Vector128Helper.Create(xor);
            for (; i < limit; i += size)
                PerformCurrentXORIterationVector128(origin, target, xorVector, i, length);
            XORLastElementsVector128(origin, target, xorVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentXORIterationVector128<T>(T* origin, T* target, Vector128<T> xor, uint index, uint length)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.XORVector128(origin, xor, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void XORLastElementsVector128<T>(T* origin, T* target, Vector128<T> xor, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var xored = SSEHelper.XORVector128(origin, xor, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&xored, target + index, 0, count);
        }
        #endregion
        #region NAND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NANDArrayVector128Generic<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var andVector = Vector128Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentNANDIterationVector128(origin, target, andVector, i, length);
            NANDLastElementsVector128(origin, target, andVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNANDIterationVector128<T>(T* origin, T* target, Vector128<T> and, uint index, uint length)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.NANDVector128(origin, and, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NANDLastElementsVector128<T>(T* origin, T* target, Vector128<T> and, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var anded = SSEHelper.NANDVector128(origin, and, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&anded, target + index, 0, count);
        }
        #endregion
        #region NOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NORArrayVector128Generic<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var andVector = Vector128Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentNORIterationVector128(origin, target, andVector, i, length);
            NORLastElementsVector128(origin, target, andVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentNORIterationVector128<T>(T* origin, T* target, Vector128<T> and, uint index, uint length)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.NORVector128(origin, and, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NORLastElementsVector128<T>(T* origin, T* target, Vector128<T> and, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var anded = SSEHelper.NORVector128(origin, and, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&anded, target + index, 0, count);
        }
        #endregion
        #region XNOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XNORArrayVector128Generic<T>(T* origin, T* target, T and, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var andVector = Vector128Helper.Create(and);
            for (; i < limit; i += size)
                PerformCurrentXNORIterationVector128(origin, target, andVector, i, length);
            XNORLastElementsVector128(origin, target, andVector, i, length);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PerformCurrentXNORIterationVector128<T>(T* origin, T* target, Vector128<T> and, uint index, uint length)
            where T : unmanaged
        {
            SSEHelper.Store(SSEHelper.XNORVector128(origin, and, index), target, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void XNORLastElementsVector128<T>(T* origin, T* target, Vector128<T> and, uint index, uint length)
            where T : unmanaged
        {
            uint count = length - index;
            var anded = SSEHelper.XNORVector128(origin, and, index);
            SIMDIntrinsicsHelper.StoreLastElementsVector128((T*)&anded, target + index, 0, count);
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