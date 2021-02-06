using Garyon.Functions.PointerHelpers;
using System.Runtime.CompilerServices;
using static Garyon.Functions.PointerHelpers.PointerBitwiseOperationsBase;

namespace Garyon.Extensions.ArrayExtensions
{
    /// <summary>Provides extensions for performing bitwise operations on elements of an array.</summary>
    public static class ArrayBitwiseOperationExtensions
    {
        #region NOT
        /// <summary>Performs the NOT operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the NOT operation on.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] NOT<T>(this T[] a)
            where T : unmanaged
        {
            return a.NOT(0, a.Length);
        }
        /// <summary>Performs the NOT operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the NOT operation on.</param>
        /// <param name="startIndex">The index of the first element in the array to perform the NOT operation on.</param>
        /// <param name="length">The length of the resulting array, which is the number of elements to perform the operation on.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] NOT<T>(this T[] a, int startIndex, int length)
            where T : unmanaged
        {
            return a.PerformBitwiseOperation(startIndex, length, default, BitwiseOperation.NOT);
        }
        #endregion
        #region AND
        /// <summary>Performs the AND operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the AND operation on.</param>
        /// <param name="mask">The mask that will be applied in the AND operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] AND<T>(this T[] a, T mask)
            where T : unmanaged
        {
            return a.AND(0, a.Length, mask);
        }
        /// <summary>Performs the AND operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the AND operation on.</param>
        /// <param name="startIndex">The index of the first element in the array to perform the AND operation on.</param>
        /// <param name="length">The length of the resulting array, which is the number of elements to perform the AND operation on.</param>
        /// <param name="mask">The mask that will be applied in the AND operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] AND<T>(this T[] a, int startIndex, int length, T mask)
            where T : unmanaged
        {
            return a.PerformBitwiseOperation(startIndex, length, mask, BitwiseOperation.AND);
        }
        #endregion
        #region OR
        /// <summary>Performs the OR operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the OR operation on.</param>
        /// <param name="mask">The mask that will be applied in the OR operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] OR<T>(this T[] a, T mask)
            where T : unmanaged
        {
            return a.OR(0, a.Length, mask);
        }
        /// <summary>Performs the OR operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the OR operation on.</param>
        /// <param name="startIndex">The index of the first element in the array to perform the OR operation on.</param>
        /// <param name="length">The length of the resulting array, which is the number of elements to perform the OR operation on.</param>
        /// <param name="mask">The mask that will be applied in the OR operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] OR<T>(this T[] a, int startIndex, int length, T mask)
            where T : unmanaged
        {
            return a.PerformBitwiseOperation(startIndex, length, mask, BitwiseOperation.OR);
        }
        #endregion
        #region XOR
        /// <summary>Performs the XOR operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the XOR operation on.</param>
        /// <param name="mask">The mask that will be applied in the XOR operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] XOR<T>(this T[] a, T mask)
            where T : unmanaged
        {
            return a.XOR(0, a.Length, mask);
        }
        /// <summary>Performs the XOR operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the XOR operation on.</param>
        /// <param name="startIndex">The index of the first element in the array to perform the XOR operation on.</param>
        /// <param name="length">The length of the resulting array, which is the number of elements to perform the XOR operation on.</param>
        /// <param name="mask">The mask that will be applied in the XOR operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] XOR<T>(this T[] a, int startIndex, int length, T mask)
            where T : unmanaged
        {
            return a.PerformBitwiseOperation(startIndex, length, mask, BitwiseOperation.XOR);
        }
        #endregion
        #region NAND
        /// <summary>Performs the NAND operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the NAND operation on.</param>
        /// <param name="mask">The mask that will be applied in the NAND operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] NAND<T>(this T[] a, T mask)
            where T : unmanaged
        {
            return a.NAND(0, a.Length, mask);
        }
        /// <summary>Performs the NAND operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the NAND operation on.</param>
        /// <param name="startIndex">The index of the first element in the array to perform the NAND operation on.</param>
        /// <param name="length">The length of the resulting array, which is the number of elements to perform the NAND operation on.</param>
        /// <param name="mask">The mask that will be applied in the NAND operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] NAND<T>(this T[] a, int startIndex, int length, T mask)
            where T : unmanaged
        {
            return a.PerformBitwiseOperation(startIndex, length, mask, BitwiseOperation.NAND);
        }
        #endregion
        #region NOR
        /// <summary>Performs the NOR operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the NOR operation on.</param>
        /// <param name="mask">The mask that will be applied in the NOR operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] NOR<T>(this T[] a, T mask)
            where T : unmanaged
        {
            return a.NOR(0, a.Length, mask);
        }
        /// <summary>Performs the NOR operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the NOR operation on.</param>
        /// <param name="startIndex">The index of the first element in the array to perform the NOR operation on.</param>
        /// <param name="length">The length of the resulting array, which is the number of elements to perform the NOR operation on.</param>
        /// <param name="mask">The mask that will be applied in the NOR operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] NOR<T>(this T[] a, int startIndex, int length, T mask)
            where T : unmanaged
        {
            return a.PerformBitwiseOperation(startIndex, length, mask, BitwiseOperation.NOR);
        }
        #endregion
        #region XNOR
        /// <summary>Performs the XNOR operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the XNOR operation on.</param>
        /// <param name="mask">The mask that will be applied in the XNOR operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] XNOR<T>(this T[] a, T mask)
            where T : unmanaged
        {
            return a.XNOR(0, a.Length, mask);
        }
        /// <summary>Performs the XNOR operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the XNOR operation on.</param>
        /// <param name="startIndex">The index of the first element in the array to perform the XNOR operation on.</param>
        /// <param name="length">The length of the resulting array, which is the number of elements to perform the XNOR operation on.</param>
        /// <param name="mask">The mask that will be applied in the XNOR operation.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] XNOR<T>(this T[] a, int startIndex, int length, T mask)
            where T : unmanaged
        {
            return a.PerformBitwiseOperation(startIndex, length, mask, BitwiseOperation.XNOR);
        }
        #endregion

        /// <summary>Performs the specified bitwise operation on all the elements of the <typeparamref name="T"/>[] and stores the resulting values into a new <typeparamref name="T"/>[].</summary>
        /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
        /// <param name="a">The <typeparamref name="T"/>[] whose elements to perform the operation on.</param>
        /// <param name="startIndex">The index of the first element in the array to perform the operation on.</param>
        /// <param name="length">The length of the resulting array, which is the number of elements to perform the operation on.</param>
        /// <param name="mask">The mask that will be applied in the operation, if supported.</param>
        /// <param name="operation">The bitwise operation to perform.</param>
        /// <returns>The resulting <typeparamref name="T"/>[], containing the manipulated elements.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T[] PerformBitwiseOperation<T>(this T[] a, int startIndex, int length, T mask, BitwiseOperation operation)
            where T : unmanaged
        {
            var result = new T[length];

            fixed (T* origin = a)
            fixed (T* target = result)
                PerformBitwiseOperation(origin + (uint)startIndex, target, mask, (uint)length, operation);

            return result;
        }

        private static unsafe void PerformBitwiseOperation<T>(T* origin, T* target, T mask, uint length, BitwiseOperation operation)
            where T : unmanaged
        {
            if (SIMDPointerBitwiseOperations.PerformBitwiseOperationVector256CustomType(origin, target, mask, length, operation))
                return;

            if (SIMDPointerBitwiseOperations.PerformBitwiseOperationVector128CustomType(origin, target, mask, length, operation))
                return;

            PointerBitwiseOperations.PerformBitwiseOperation(origin, target, mask, length, operation);
        }
    }
}
