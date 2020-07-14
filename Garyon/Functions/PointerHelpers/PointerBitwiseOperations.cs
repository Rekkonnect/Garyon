using System.Runtime.CompilerServices;

namespace Garyon.Functions.PointerHelpers
{
    /// <summary>Provides functions that convert elements of given sequences as pointers.</summary>
    public abstract unsafe class PointerBitwiseOperations : PointerBitwiseOperationsBase
    {
        // All functions return bool to match ArrayBitwiseOperation<T>'s return type

        #region NOT
        /// <summary>Performs the NOT operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.</summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the NOT operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="length">The length of both sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NOT<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            return sizeof(T) switch
            {
                sizeof(byte) => NOTByteArray((byte*)origin, (byte*)target, length),
                sizeof(ushort) => NOTUInt16Array((ushort*)origin, (ushort*)target, length),
                sizeof(uint) => NOTUInt32Array((uint*)origin, (uint*)target, length),
                sizeof(ulong) => NOTUInt64Array((ulong*)origin, (ulong*)target, length),
                _ => NOTCustomStructSizeArray(origin, target, length),
            };
        }

        /// <summary>
        /// Performs the NOT operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.<br/>
        /// The provided type is treated as an unknown non-primitive struct type. Use <seealso cref="NOT{T}(T*, T*, uint)"/> for struct types with size equal to that of any known primitive struct type (1, 2, 4 or 8).
        /// </summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the NOT operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NOTCustomStructSizeArray<T>(T* origin, T* target, uint length)
            where T : unmanaged
        {
            return PerformOperationCustomStructSizeArray(origin, target, default, length, BitwiseOperation.NOT);
        }
        /// <summary>Performs the NOT operation on a number of elements from a sequence given as a <seealso cref="byte"/>* and stores the results in a given sequence as a <seealso cref="byte"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="byte"/>* whose elements to perform the NOT operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="byte"/>* that will contain the manipulated elements.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NOTByteArray(byte* origin, byte* target, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, default, length, BitwiseOperation.NOT);
        }
        /// <summary>Performs the NOT operation on a number of elements from a sequence given as a <seealso cref="ushort"/>* and stores the results in a given sequence as a <seealso cref="ushort"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ushort"/>* whose elements to perform the NOT operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ushort"/>* that will contain the manipulated elements.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NOTUInt16Array(ushort* origin, ushort* target, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, default, length, BitwiseOperation.NOT);
        }
        /// <summary>Performs the NOT operation on a number of elements from a sequence given as a <seealso cref="uint"/>* and stores the results in a given sequence as a <seealso cref="uint"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="uint"/>* whose elements to perform the NOT operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="uint"/>* that will contain the manipulated elements.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NOTUInt32Array(uint* origin, uint* target, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, default, length, BitwiseOperation.NOT);
        }
        /// <summary>Performs the NOT operation on a number of elements from a sequence given as a <seealso cref="ulong"/>* and stores the results in a given sequence as a <seealso cref="ulong"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ulong"/>* whose elements to perform the NOT operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ulong"/>* that will contain the manipulated elements.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NOTUInt64Array(ulong* origin, ulong* target, uint length)
        {
            for (uint i = 0; i < length; i++)
                target[i] = ~origin[i];
            return true;
        }

        // This function only exists to be considered an ArrayBitwiseOperation<T>
        private static bool NOTUInt64Array(ulong* origin, ulong* target, ulong mask, uint length)
        {
            return NOTUInt64Array(origin, target, default, length);
        }
        #endregion
        #region AND
        /// <summary>Performs the AND operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.</summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the AND operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the AND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AND<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return sizeof(T) switch
            {
                sizeof(byte) => ANDByteArray((byte*)origin, (byte*)target, *(byte*)&mask, length),
                sizeof(ushort) => ANDUInt16Array((ushort*)origin, (ushort*)target, *(ushort*)&mask, length),
                sizeof(uint) => ANDUInt32Array((uint*)origin, (uint*)target, *(uint*)&mask, length),
                sizeof(ulong) => ANDUInt64Array((ulong*)origin, (ulong*)target, *(ulong*)&mask, length),
                _ => ANDCustomStructSizeArray(origin, target, mask, length),
            };
        }

        /// <summary>
        /// Performs the AND operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.<br/>
        /// The provided type is treated as an unknown non-primitive struct type. Use <seealso cref="AND{T}(T*, T*, T, uint)"/> for struct types with size equal to that of any known primitive struct type (1, 2, 4 or 8).
        /// </summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the AND operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the AND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ANDCustomStructSizeArray<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformOperationCustomStructSizeArray(origin, target, mask, length, BitwiseOperation.AND);
        }
        /// <summary>Performs the AND operation on a number of elements from a sequence given as a <seealso cref="byte"/>* and stores the results in a given sequence as a <seealso cref="byte"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="byte"/>* whose elements to perform the AND operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="byte"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the AND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ANDByteArray(byte* origin, byte* target, byte mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.AND);
        }
        /// <summary>Performs the AND operation on a number of elements from a sequence given as a <seealso cref="ushort"/>* and stores the results in a given sequence as a <seealso cref="ushort"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ushort"/>* whose elements to perform the AND operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ushort"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the AND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ANDUInt16Array(ushort* origin, ushort* target, ushort mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.AND);
        }
        /// <summary>Performs the AND operation on a number of elements from a sequence given as a <seealso cref="uint"/>* and stores the results in a given sequence as a <seealso cref="uint"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="uint"/>* whose elements to perform the AND operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="uint"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the AND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ANDUInt32Array(uint* origin, uint* target, uint mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.AND);
        }
        /// <summary>Performs the AND operation on a number of elements from a sequence given as a <seealso cref="ulong"/>* and stores the results in a given sequence as a <seealso cref="ulong"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ulong"/>* whose elements to perform the AND operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ulong"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the AND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ANDUInt64Array(ulong* origin, ulong* target, ulong mask, uint length)
        {
            for (uint i = 0; i < length; i++)
                target[i] = origin[i] & mask;
            return true;
        }
        #endregion
        #region OR
        /// <summary>Performs the OR operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.</summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the OR operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the OR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OR<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return sizeof(T) switch
            {
                sizeof(byte) => ORByteArray((byte*)origin, (byte*)target, *(byte*)&mask, length),
                sizeof(ushort) => ORUInt16Array((ushort*)origin, (ushort*)target, *(ushort*)&mask, length),
                sizeof(uint) => ORUInt32Array((uint*)origin, (uint*)target, *(uint*)&mask, length),
                sizeof(ulong) => ORUInt64Array((ulong*)origin, (ulong*)target, *(ulong*)&mask, length),
                _ => ORCustomStructSizeArray(origin, target, mask, length),
            };
        }

        /// <summary>
        /// Performs the OR operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.<br/>
        /// The provided type is treated as an unknown non-primitive struct type. Use <seealso cref="OR{T}(T*, T*, T, uint)"/> for struct types with size equal to that of any known primitive struct type (1, 2, 4 or 8).
        /// </summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the OR operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the OR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ORCustomStructSizeArray<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformOperationCustomStructSizeArray(origin, target, mask, length, BitwiseOperation.OR);
        }
        /// <summary>Performs the OR operation on a number of elements from a sequence given as a <seealso cref="byte"/>* and stores the results in a given sequence as a <seealso cref="byte"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="byte"/>* whose elements to perform the OR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="byte"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the OR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ORByteArray(byte* origin, byte* target, byte mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.OR);
        }
        /// <summary>Performs the OR operation on a number of elements from a sequence given as a <seealso cref="ushort"/>* and stores the results in a given sequence as a <seealso cref="ushort"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ushort"/>* whose elements to perform the OR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ushort"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the OR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ORUInt16Array(ushort* origin, ushort* target, ushort mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.OR);
        }
        /// <summary>Performs the OR operation on a number of elements from a sequence given as a <seealso cref="uint"/>* and stores the results in a given sequence as a <seealso cref="uint"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="uint"/>* whose elements to perform the OR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="uint"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the OR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ORUInt32Array(uint* origin, uint* target, uint mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.OR);
        }
        /// <summary>Performs the OR operation on a number of elements from a sequence given as a <seealso cref="ulong"/>* and stores the results in a given sequence as a <seealso cref="ulong"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ulong"/>* whose elements to perform the OR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ulong"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the OR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool ORUInt64Array(ulong* origin, ulong* target, ulong mask, uint length)
        {
            for (uint i = 0; i < length; i++)
                target[i] = origin[i] | mask;
            return true;
        }
        #endregion
        #region XOR
        /// <summary>Performs the XOR operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.</summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the XOR operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XOR<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return sizeof(T) switch
            {
                sizeof(byte) => XORByteArray((byte*)origin, (byte*)target, *(byte*)&mask, length),
                sizeof(ushort) => XORUInt16Array((ushort*)origin, (ushort*)target, *(ushort*)&mask, length),
                sizeof(uint) => XORUInt32Array((uint*)origin, (uint*)target, *(uint*)&mask, length),
                sizeof(ulong) => XORUInt64Array((ulong*)origin, (ulong*)target, *(ulong*)&mask, length),
                _ => XORCustomStructSizeArray(origin, target, mask, length),
            };
        }

        /// <summary>
        /// Performs the XOR operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.<br/>
        /// The provided type is treated as an unknown non-primitive struct type. Use <seealso cref="XOR{T}(T*, T*, T, uint)"/> for struct types with size equal to that of any known primitive struct type (1, 2, 4 or 8).
        /// </summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the XOR operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XORCustomStructSizeArray<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformOperationCustomStructSizeArray(origin, target, mask, length, BitwiseOperation.XOR);
        }
        /// <summary>Performs the XOR operation on a number of elements from a sequence given as a <seealso cref="byte"/>* and stores the results in a given sequence as a <seealso cref="byte"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="byte"/>* whose elements to perform the XOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="byte"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XORByteArray(byte* origin, byte* target, byte mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.XOR);
        }
        /// <summary>Performs the XOR operation on a number of elements from a sequence given as a <seealso cref="ushort"/>* and stores the results in a given sequence as a <seealso cref="ushort"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ushort"/>* whose elements to perform the XOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ushort"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XORUInt16Array(ushort* origin, ushort* target, ushort mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.XOR);
        }
        /// <summary>Performs the XOR operation on a number of elements from a sequence given as a <seealso cref="uint"/>* and stores the results in a given sequence as a <seealso cref="uint"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="uint"/>* whose elements to perform the XOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="uint"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XORUInt32Array(uint* origin, uint* target, uint mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.XOR);
        }
        /// <summary>Performs the XOR operation on a number of elements from a sequence given as a <seealso cref="ulong"/>* and stores the results in a given sequence as a <seealso cref="ulong"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ulong"/>* whose elements to perform the XOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ulong"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XORUInt64Array(ulong* origin, ulong* target, ulong mask, uint length)
        {
            for (uint i = 0; i < length; i++)
                target[i] = origin[i] ^ mask;
            return true;
        }
        #endregion
        #region NAND
        /// <summary>Performs the NAND operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.</summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the NAND operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NAND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NAND<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return sizeof(T) switch
            {
                sizeof(byte) => NANDByteArray((byte*)origin, (byte*)target, *(byte*)&mask, length),
                sizeof(ushort) => NANDUInt16Array((ushort*)origin, (ushort*)target, *(ushort*)&mask, length),
                sizeof(uint) => NANDUInt32Array((uint*)origin, (uint*)target, *(uint*)&mask, length),
                sizeof(ulong) => NANDUInt64Array((ulong*)origin, (ulong*)target, *(ulong*)&mask, length),
                _ => NANDCustomStructSizeArray(origin, target, mask, length),
            };
        }

        /// <summary>
        /// Performs the NAND operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.<br/>
        /// The provided type is treated as an unknown non-primitive struct type. Use <seealso cref="NAND{T}(T*, T*, T, uint)"/> for struct types with size equal to that of any known primitive struct type (1, 2, 4 or 8).
        /// </summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the NAND operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NAND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NANDCustomStructSizeArray<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformOperationCustomStructSizeArray(origin, target, mask, length, BitwiseOperation.NAND);
        }
        /// <summary>Performs the NAND operation on a number of elements from a sequence given as a <seealso cref="byte"/>* and stores the results in a given sequence as a <seealso cref="byte"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="byte"/>* whose elements to perform the NAND operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="byte"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NAND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NANDByteArray(byte* origin, byte* target, byte mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.NAND);
        }
        /// <summary>Performs the NAND operation on a number of elements from a sequence given as a <seealso cref="ushort"/>* and stores the results in a given sequence as a <seealso cref="ushort"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ushort"/>* whose elements to perform the NAND operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ushort"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NAND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NANDUInt16Array(ushort* origin, ushort* target, ushort mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.NAND);
        }
        /// <summary>Performs the NAND operation on a number of elements from a sequence given as a <seealso cref="uint"/>* and stores the results in a given sequence as a <seealso cref="uint"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="uint"/>* whose elements to perform the NAND operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="uint"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NAND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NANDUInt32Array(uint* origin, uint* target, uint mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.NAND);
        }
        /// <summary>Performs the NAND operation on a number of elements from a sequence given as a <seealso cref="ulong"/>* and stores the results in a given sequence as a <seealso cref="ulong"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ulong"/>* whose elements to perform the NAND operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ulong"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NAND operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NANDUInt64Array(ulong* origin, ulong* target, ulong mask, uint length)
        {
            for (uint i = 0; i < length; i++)
                target[i] = ~(origin[i] & mask);
            return true;
        }
        #endregion
        #region NOR
        /// <summary>Performs the NOR operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.</summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the NOR operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NOR<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return sizeof(T) switch
            {
                sizeof(byte) => NORByteArray((byte*)origin, (byte*)target, *(byte*)&mask, length),
                sizeof(ushort) => NORUInt16Array((ushort*)origin, (ushort*)target, *(ushort*)&mask, length),
                sizeof(uint) => NORUInt32Array((uint*)origin, (uint*)target, *(uint*)&mask, length),
                sizeof(ulong) => NORUInt64Array((ulong*)origin, (ulong*)target, *(ulong*)&mask, length),
                _ => NORCustomStructSizeArray(origin, target, mask, length),
            };
        }

        /// <summary>
        /// Performs the NOR operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.<br/>
        /// The provided type is treated as an unknown non-primitive struct type. Use <seealso cref="NOR{T}(T*, T*, T, uint)"/> for struct types with size equal to that of any known primitive struct type (1, 2, 4 or 8).
        /// </summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the NOR operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NORCustomStructSizeArray<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformOperationCustomStructSizeArray(origin, target, mask, length, BitwiseOperation.NOR);
        }
        /// <summary>Performs the NOR operation on a number of elements from a sequence given as a <seealso cref="byte"/>* and stores the results in a given sequence as a <seealso cref="byte"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="byte"/>* whose elements to perform the NOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="byte"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NORByteArray(byte* origin, byte* target, byte mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.NOR);
        }
        /// <summary>Performs the NOR operation on a number of elements from a sequence given as a <seealso cref="ushort"/>* and stores the results in a given sequence as a <seealso cref="ushort"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ushort"/>* whose elements to perform the NOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ushort"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NORUInt16Array(ushort* origin, ushort* target, ushort mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.NOR);
        }
        /// <summary>Performs the NOR operation on a number of elements from a sequence given as a <seealso cref="uint"/>* and stores the results in a given sequence as a <seealso cref="uint"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="uint"/>* whose elements to perform the NOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="uint"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NORUInt32Array(uint* origin, uint* target, uint mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.NOR);
        }
        /// <summary>Performs the NOR operation on a number of elements from a sequence given as a <seealso cref="ulong"/>* and stores the results in a given sequence as a <seealso cref="ulong"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ulong"/>* whose elements to perform the NOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ulong"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the NOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool NORUInt64Array(ulong* origin, ulong* target, ulong mask, uint length)
        {
            for (uint i = 0; i < length; i++)
                target[i] = ~(origin[i] | mask);
            return true;
        }
        #endregion
        #region XNOR
        /// <summary>Performs the XNOR operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.</summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the XNOR operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XNOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool XNOR<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return sizeof(T) switch
            {
                sizeof(byte) => XNORByteArray((byte*)origin, (byte*)target, *(byte*)&mask, length),
                sizeof(ushort) => XNORUInt16Array((ushort*)origin, (ushort*)target, *(ushort*)&mask, length),
                sizeof(uint) => XNORUInt32Array((uint*)origin, (uint*)target, *(uint*)&mask, length),
                sizeof(ulong) => XNORUInt64Array((ulong*)origin, (ulong*)target, *(ulong*)&mask, length),
                _ => XNORCustomStructSizeArray(origin, target, mask, length),
            };
        }

        /// <summary>
        /// Performs the XNOR operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.<br/>
        /// The provided type is treated as an unknown non-primitive struct type. Use <seealso cref="XNOR{T}(T*, T*, T, uint)"/> for struct types with size equal to that of any known primitive struct type (1, 2, 4 or 8).
        /// </summary>
        /// <typeparam name="T">The type of the elements stored in the sequence.</typeparam>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the XNOR operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XNOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XNORCustomStructSizeArray<T>(T* origin, T* target, T mask, uint length)
            where T : unmanaged
        {
            return PerformOperationCustomStructSizeArray(origin, target, mask, length, BitwiseOperation.XNOR);
        }
        /// <summary>Performs the XNOR operation on a number of elements from a sequence given as a <seealso cref="byte"/>* and stores the results in a given sequence as a <seealso cref="byte"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="byte"/>* whose elements to perform the XNOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="byte"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XNOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XNORByteArray(byte* origin, byte* target, byte mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.XNOR);
        }
        /// <summary>Performs the XNOR operation on a number of elements from a sequence given as a <seealso cref="ushort"/>* and stores the results in a given sequence as a <seealso cref="ushort"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ushort"/>* whose elements to perform the XNOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ushort"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XNOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XNORUInt16Array(ushort* origin, ushort* target, ushort mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.XNOR);
        }
        /// <summary>Performs the XNOR operation on a number of elements from a sequence given as a <seealso cref="uint"/>* and stores the results in a given sequence as a <seealso cref="uint"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="uint"/>* whose elements to perform the XNOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="uint"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XNOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XNORUInt32Array(uint* origin, uint* target, uint mask, uint length)
        {
            return PerformOperationPrimitiveStructArray(origin, target, mask, length, BitwiseOperation.XNOR);
        }
        /// <summary>Performs the XNOR operation on a number of elements from a sequence given as a <seealso cref="ulong"/>* and stores the results in a given sequence as a <seealso cref="ulong"/>*.</summary>
        /// <param name="origin">The origin sequence as a <seealso cref="ulong"/>* whose elements to perform the XNOR operation on.</param>
        /// <param name="target">The target sequence as a <seealso cref="ulong"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the XNOR operation.</param>
        /// <param name="length">The length of both sequences.</param>
        public static bool XNORUInt64Array(ulong* origin, ulong* target, ulong mask, uint length)
        {
            for (uint i = 0; i < length; i++)
                target[i] = ~(origin[i] ^ mask);
            return true;
        }
        #endregion

        /// <summary>Performs the specified bitwise operation operation on a number of elements from a sequence given as a <typeparamref name="T"/>* and stores the results in a given sequence as a <typeparamref name="T"/>*.</summary>
        /// <param name="origin">The origin sequence as a <typeparamref name="T"/>* whose elements to perform the operation on.</param>
        /// <param name="target">The target sequence as a <typeparamref name="T"/>* that will contain the manipulated elements.</param>
        /// <param name="mask">The mask that will be applied in the operation, if supported.</param>
        /// <param name="length">The length of both sequences.</param>
        /// <param name="operation">The bitwise operation to perform</param>
        public static bool PerformBitwiseOperation<T>(T* origin, T* target, T mask, uint length, BitwiseOperation operation)
            where T : unmanaged
        {
            return operation switch
            {
                BitwiseOperation.NOT => NOT(origin, target, length),
                BitwiseOperation.AND => AND(origin, target, mask, length),
                BitwiseOperation.OR => OR(origin, target, mask, length),
                BitwiseOperation.XOR => XOR(origin, target, mask, length),
                BitwiseOperation.NAND => NAND(origin, target, mask, length),
                BitwiseOperation.NOR => NOR(origin, target, mask, length),
                BitwiseOperation.XNOR => XNOR(origin, target, mask, length),
            };
        }

        private static ArrayBitwiseOperation<ulong> GetUInt64BitwiseOperationPerformer(BitwiseOperation operation)
        {
            return operation switch
            {
                BitwiseOperation.NOT => NOTUInt64Array,
                BitwiseOperation.AND => ANDUInt64Array,
                BitwiseOperation.OR => ORUInt64Array,
                BitwiseOperation.XOR => XORUInt64Array,
                BitwiseOperation.NAND => NANDUInt64Array,
                BitwiseOperation.NOR => NORUInt64Array,
                BitwiseOperation.XNOR => XNORUInt64Array,
            };
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool PerformOperationPrimitiveStructArray<T>(T* origin, T* target, T mask, uint length, BitwiseOperation operation)
            where T : unmanaged
        {
            ulong extendedMask = ExtendMaskTo8Bytes(mask);
            uint byteLength = length * (uint)sizeof(T);
            uint newLength = byteLength / sizeof(ulong);
            uint remainder = byteLength % sizeof(ulong);
            GetUInt64BitwiseOperationPerformer(operation)((ulong*)origin, (ulong*)target, extendedMask, newLength);
            return PerformOperationCustomStructSizeArray(origin + newLength * 8, target + newLength * 8, (byte*)&extendedMask, 1, remainder, operation);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool PerformOperationCustomStructSizeArray<T>(T* origin, T* target, T mask, uint length, BitwiseOperation operation)
            where T : unmanaged
        {
            return PerformOperationCustomStructSizeArray(origin, target, (byte*)&mask, length, (uint)sizeof(T), operation);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool PerformOperationCustomStructSizeArray<T>(T* origin, T* target, byte* mask, uint length, uint maskLength, BitwiseOperation operation)
            where T : unmanaged
        {
            for (uint i = 0; i < length; i++)
            {
                uint performedIterations = 0;

                uint iterations = maskLength / sizeof(ulong);
                uint remainder = maskLength % sizeof(ulong);

                ulong* o = (ulong*)&origin[i];
                ulong* t = (ulong*)&target[i];
                ulong* m = (ulong*)mask;

                while (performedIterations < iterations)
                {
                    *t = PerformBitwiseOperation(*o, *m, operation);
                    performedIterations++;
                    PointerArithmetic.Increment(ref o, ref m);
                }
                if (remainder > 0)
                {
                    ulong originMask = ulong.MaxValue << (int)(remainder * 8);
                    ulong targetMask = ~originMask;

                    var originResult = *o;
                    var result = PerformBitwiseOperation(*o, *m, operation);
                    *t = (originResult & originMask) | (result & targetMask);
                }
            }
            return true;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong PerformBitwiseOperation(ulong a, ulong b, BitwiseOperation operation)
        {
            return operation switch
            {
                BitwiseOperation.NOT => ~a,
                BitwiseOperation.AND => a & b,
                BitwiseOperation.OR => a | b,
                BitwiseOperation.XOR => a ^ b,
                BitwiseOperation.NAND => ~(a & b),
                BitwiseOperation.NOR => ~(a | b),
                BitwiseOperation.XNOR => ~(a ^ b),
            };
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong ExtendMaskTo8Bytes<T>(T mask)
            where T : unmanaged
        {
            ulong extended = 0;
            T* elementArray = (T*)&extended;
            int size = sizeof(ulong) / sizeof(T);
            for (int i = 0; i < size; i++)
                elementArray[i] = mask;
            return extended;
        }
    }
}