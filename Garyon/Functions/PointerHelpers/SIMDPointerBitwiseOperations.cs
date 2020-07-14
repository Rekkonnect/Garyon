using Garyon.Functions.IntrinsicsHelpers;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.PointerHelpers
{
    /// <summary>Contains unsafe helper functions for bitwise operations using SIMD. All functions check whether the minimum supported instruction set is included; in the case that the set is unavailable, the functions simply do nothing.</summary>
    public abstract unsafe class SIMDPointerBitwiseOperations : PointerBitwiseOperationsBase
    {
        // TODO: Cleanup some functions that are not necessary
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool PerformBitwiseOperationAs<TOrigin, TNew>(TOrigin* origin, TOrigin* target, TOrigin* mask, uint length, BitwiseOperation operation, ArrayBitwiseOperationDelegateReturner<TNew> delegateReturner)
            where TOrigin : unmanaged
            where TNew : unmanaged
        {
            uint newLength = length * (uint)sizeof(TOrigin) / (uint)sizeof(TNew);
            return delegateReturner(operation)?.Invoke((TNew*)origin, (TNew*)target, *(TNew*)mask, newLength) == true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool EvaluateMaskConvertibility<TOrigin, TNew>(TOrigin* mask)
            where TOrigin : unmanaged
            where TNew : unmanaged
        {
            if (sizeof(TOrigin) == sizeof(TNew))
                return true;

            if (sizeof(TOrigin) % sizeof(TNew) != 0)
                return false;

            // Consider the mask as an array of TNew, and evaluate all its elements
            var reinterpretedMask = (TNew*)mask;
            uint length = (uint)sizeof(TOrigin) / (uint)sizeof(TNew);
            for (uint i = 1; i < length; i++)
                if (!reinterpretedMask[i].Equals(reinterpretedMask[0]))
                    return false;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool PerformArrayBitwiseOperationVector256CustomType<T>(T* origin, T* target, T mask, uint length, BitwiseOperation operation)
            where T : unmanaged
        {
            if (EvaluateMaskConvertibility<T, byte>(&mask))
                return PerformBitwiseOperationVector256As<T, byte>(origin, target, &mask, length, operation);
            if (EvaluateMaskConvertibility<T, short>(&mask))
                return PerformBitwiseOperationVector256As<T, short>(origin, target, &mask, length, operation);
            if (EvaluateMaskConvertibility<T, int>(&mask))
                return PerformBitwiseOperationVector256As<T, int>(origin, target, &mask, length, operation);
            if (EvaluateMaskConvertibility<T, long>(&mask))
                return PerformBitwiseOperationVector256As<T, long>(origin, target, &mask, length, operation);

            return false;
        }

        private static ArrayBitwiseOperationDelegateReturner<T> GetBitwiseOperationDelegate<T>(SIMDVectorType vectorType)
            where T : unmanaged
        {
            switch (vectorType)
            {
                case SIMDVectorType.Vector128:
                    return GetBitwiseOperationDelegateVector128<T>;
                case SIMDVectorType.Vector256:
                    return GetBitwiseOperationDelegateVector256<T>;
            }
            return null;
        }

        #region Vector256
        #region NOT
        /// <summary>Performs the bitwise NOT operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: AVX.</summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the NOT operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
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
        /// <summary>Performs the bitwise NOT operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: AVX.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="NOTArrayVector256CustomType{T}(T*, T*, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the NOT operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool PerformBitwiseOperationVector256As<TOrigin, TNew>(TOrigin* origin, TOrigin* target, TOrigin* mask, uint length, BitwiseOperation operation)
            where TOrigin : unmanaged
            where TNew : unmanaged
        {
            return PerformBitwiseOperationAs(origin, target, mask, length, operation, GetBitwiseOperationDelegateVector256<TNew>);
        }

        #region AND
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ANDArrayVector256CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformArrayBitwiseOperationVector256CustomType(origin, target, mask, length, BitwiseOperation.AND);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ANDArrayVector256<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
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
        public static bool ORArrayVector256CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformArrayBitwiseOperationVector256CustomType(origin, target, mask, length, BitwiseOperation.OR);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ORArrayVector256<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
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
        public static bool XORArrayVector256CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformArrayBitwiseOperationVector256CustomType(origin, target, mask, length, BitwiseOperation.XOR);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector256<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector256<T>())
                return false;

            uint size = (uint)Vector256<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector256Helper.Create(mask);
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
        public static bool NANDArrayVector256CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformArrayBitwiseOperationVector256CustomType(origin, target, mask, length, BitwiseOperation.NAND);
        }

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
        public static bool NORArrayVector256CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformArrayBitwiseOperationVector256CustomType(origin, target, mask, length, BitwiseOperation.NOR);
        }

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
        public static bool XNORArrayVector256CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformArrayBitwiseOperationVector256CustomType(origin, target, mask, length, BitwiseOperation.XNOR);
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool PerformBitwiseOperationCustomType<T>(T* origin, T* target, T mask, uint length, BitwiseOperation operation, SIMDVectorType vectorType)
            where T : unmanaged
        {
            switch (sizeof(T))
            {
                case sizeof(byte):
                case sizeof(short):
                case sizeof(int):
                case sizeof(long):
                    // Okay this is kinda ridiculous
                    return GetBitwiseOperationDelegateReturner<T>(vectorType)(operation)(origin, target, mask, length);
            }
            // TODO: Support types with different sizes too
            return false;
        }

        private static ArrayBitwiseOperationDelegateReturner<T> GetBitwiseOperationDelegateReturner<T>(SIMDVectorType vectorType)
            where T : unmanaged
        {
            switch (vectorType)
            {
                case SIMDVectorType.Vector128:
                    return GetBitwiseOperationDelegateVector128<T>;
                case SIMDVectorType.Vector256:
                    return GetBitwiseOperationDelegateVector256<T>;
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PerformBitwiseOperationVector256CustomType<T>(T* origin, T* target, T mask, uint length, BitwiseOperation operation)
            where T : unmanaged
        {
            switch (sizeof(T))
            {
                case sizeof(byte):
                case sizeof(short):
                case sizeof(int):
                case sizeof(long):
                    return GetBitwiseOperationDelegateVector256<T>(operation)(origin, target, mask, length);
            }
            // TODO: Support types with different sizes too
            return false;
        }
        public static ArrayBitwiseOperation<T> GetBitwiseOperationDelegateVector256<T>(BitwiseOperation operation)
            where T : unmanaged
        {
            switch (operation)
            {
                case BitwiseOperation.AND:
                    return ANDArrayVector256;
                case BitwiseOperation.OR:
                    return ORArrayVector256;
                case BitwiseOperation.XOR:
                    return XORArrayVector256;
                case BitwiseOperation.NAND:
                    return NANDArrayVector256;
                case BitwiseOperation.NOR:
                    return NORArrayVector256;
                case BitwiseOperation.XNOR:
                    return XNORArrayVector256;
            }
            return null;
        }
        #endregion

        #region Vector128
        #region NOT
        /// <summary>Performs the bitwise NOT operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
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
        /// <summary>Performs the bitwise AND operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the AND operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the AND operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ANDArrayVector128CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformBitwiseOperationVector128CustomType(origin, target, mask, length, BitwiseOperation.AND);
        }
        /// <summary>Performs the bitwise AND operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="ANDArrayVector128CustomType{T}(T*, T*, T, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the AND operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the AND operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ANDArrayVector128<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(mask);
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
        /// <summary>Performs the bitwise OR operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the OR operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the OR operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ORArrayVector128CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformBitwiseOperationVector128CustomType(origin, target, mask, length, BitwiseOperation.OR);
        }
        /// <summary>Performs the bitwise OR operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="ORArrayVector128CustomType{T}(T*, T*, T, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the OR operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the OR operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ORArrayVector128<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(mask);
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
        /// <summary>Performs the bitwise XOR operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the XOR operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the XOR operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector128CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformBitwiseOperationVector128CustomType(origin, target, mask, length, BitwiseOperation.XOR);
        }
        /// <summary>Performs the bitwise XOR operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="XORArrayVector128CustomType{T}(T*, T*, T, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the XOR operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the XOR operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XORArrayVector128<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(mask);
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
        /// <summary>Performs the bitwise NAND operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the NAND operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the NAND operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NANDArrayVector128CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformBitwiseOperationVector128CustomType(origin, target, mask, length, BitwiseOperation.NAND);
        }
        /// <summary>Performs the bitwise NAND operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="NANDArrayVector128CustomType{T}(T*, T*, T, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the NAND operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the NAND operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NANDArrayVector128<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(mask);
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
        /// <summary>Performs the bitwise NOR operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the NOR operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the NOR operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NORArrayVector128CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformBitwiseOperationVector128CustomType(origin, target, mask, length, BitwiseOperation.NOR);
        }
        /// <summary>Performs the bitwise NOR operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="NORArrayVector128CustomType{T}(T*, T*, T, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the NOR operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the NOR operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NORArrayVector128<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(mask);
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
        /// <summary>Performs the bitwise XNOR operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the XNOR operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the XNOR operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XNORArrayVector128CustomType<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformBitwiseOperationVector128CustomType(origin, target, mask, length, BitwiseOperation.XNOR);
        }
        /// <summary>Performs the bitwise XNOR operation to all the elements of the origin array and stores the results into the target array using SIMD instructions. Minimum instruction set: SSE.</summary>
        /// <typeparam name="T">The type of the elements of the array. It must be one of the supported types for <seealso cref="Vector128{T}"/>; for other types consider using <seealso cref="XNORArrayVector128CustomType{T}(T*, T*, T, uint)"/>.</typeparam>
        /// <param name="origin">The origin array of the elements to perform the XNOR operation on, passed as a pointer.</param>
        /// <param name="target">The target array to store the results, passed as a pointer.</param>
        /// <param name="mask">The mask that is applied to all elements during the XNOR operation.</param>
        /// <param name="length">The length of both arrays.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XNORArrayVector128<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            if (!GetSupportedInstructionSetVector128<T>())
                return false;

            uint size = (uint)Vector128<T>.Count;

            uint i = 0;
            uint limit = length & ~(size - 1);
            var maskVector = Vector128Helper.Create(mask);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PerformBitwiseOperationVector128CustomType<T>(T* origin, T* target, T mask, uint length, BitwiseOperation operation)
            where T : unmanaged
        {
            switch (sizeof(T))
            {
                case sizeof(byte):
                case sizeof(short):
                case sizeof(int):
                case sizeof(long):
                    return GetBitwiseOperationDelegateVector128<T>(operation)(origin, target, mask, length);
            }
            // TODO: Support types with different sizes too
            return false;
        }
        public static ArrayBitwiseOperation<T> GetBitwiseOperationDelegateVector128<T>(BitwiseOperation operation)
            where T : unmanaged
        {
            switch (operation)
            {
                case BitwiseOperation.AND:
                    return ANDArrayVector128;
                case BitwiseOperation.OR:
                    return ORArrayVector128;
                case BitwiseOperation.XOR:
                    return XORArrayVector128;
                case BitwiseOperation.NAND:
                    return NANDArrayVector128;
                case BitwiseOperation.NOR:
                    return NORArrayVector128;
                case BitwiseOperation.XNOR:
                    return XNORArrayVector128;
            }
            return null;
        }
        #endregion
    }
}