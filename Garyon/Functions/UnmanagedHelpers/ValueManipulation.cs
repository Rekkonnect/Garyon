using Garyon.Functions.PointerHelpers;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static Garyon.Functions.PointerHelpers.PointerBitwiseOperationsBase;

namespace Garyon.Functions.UnmanagedHelpers
{
    /// <summary>Provides functions for value manipulation.</summary>
    public abstract class ValueManipulation
    {
        /// <summary>Rescales a value into a different type. The returned value includes as many bytes as the target type allows from the original type, with the rest, if any, set to 0.</summary>
        /// <typeparam name="TOrigin">The type of the original value.</typeparam>
        /// <typeparam name="TTarget">The target type to rescale the value into.</typeparam>
        /// <param name="value">The value to rescale.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe TTarget Rescale<TOrigin, TTarget>(TOrigin value)
            where TOrigin : unmanaged
            where TTarget : unmanaged
        {
            if (sizeof(TOrigin) < sizeof(TTarget))
            {
                TTarget final = default;
                *(TOrigin*)&final = value;
                return final;
            }
            return *(TTarget*)&value;
        }

        /// <summary>Performs the NOT bitwise operation into a value of any type. All its bytes are negated.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe T NOT<T>(T value)
            where T : unmanaged
        {
            if (sizeof(T) > sizeof(Vector128<byte>))
                if (Avx.IsSupported)
                    return UseVector256(value);

            if (sizeof(T) > sizeof(ulong))
                if (Sse.IsSupported)
                    return UseVector128(value);

            return UseVector64(value);

            static T UseVector64(T value)
            {
                T result = default;
                PointerBitwiseOperations.NOTByteArray((byte*)&value, (byte*)&result, (uint)sizeof(T));
                return result;
            }
            static T UseVector128(T value)
            {
                T result = default;
                SIMDPointerBitwiseOperations.NOTArrayVector128((byte*)&value, (byte*)&result, (uint)sizeof(T));
                return result;
            }
            static T UseVector256(T value)
            {
                T result = default;
                SIMDPointerBitwiseOperations.NOTArrayVector256((byte*)&value, (byte*)&result, (uint)sizeof(T));
                return result;
            }
        }

        #region Perform Operations
        /// <summary>Performs the AND bitwise operation into a value of any type. All its bytes are ANDed.</summary>
        /// <typeparam name="T">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the AND operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe T AND<T>(T value, T mask)
            where T : unmanaged
        {
            return PerformOperation(value, mask, BitwiseOperation.AND);
        }
        /// <summary>Performs the OR bitwise operation into a value of any type. All its bytes are ORed.</summary>
        /// <typeparam name="T">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the OR operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe T OR<T>(T value, T mask)
            where T : unmanaged
        {
            return PerformOperation(value, mask, BitwiseOperation.OR);
        }
        /// <summary>Performs the XOR bitwise operation into a value of any type. All its bytes are XORed.</summary>
        /// <typeparam name="T">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the XOR operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe T XOR<T>(T value, T mask)
            where T : unmanaged
        {
            return PerformOperation(value, mask, BitwiseOperation.XOR);
        }
        /// <summary>Performs the NAND bitwise operation into a value of any type. All its bytes are NANDed.</summary>
        /// <typeparam name="T">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the NAND operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe T NAND<T>(T value, T mask)
            where T : unmanaged
        {
            return PerformOperation(value, mask, BitwiseOperation.NAND);
        }
        /// <summary>Performs the NOR bitwise operation into a value of any type. All its bytes are NORed.</summary>
        /// <typeparam name="T">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the NOR operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe T NOR<T>(T value, T mask)
            where T : unmanaged
        {
            return PerformOperation(value, mask, BitwiseOperation.NOR);
        }
        /// <summary>Performs the XNOR bitwise operation into a value of any type. All its bytes are XNORed.</summary>
        /// <typeparam name="T">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the XNOR operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe T XNOR<T>(T value, T mask)
            where T : unmanaged
        {
            return PerformOperation(value, mask, BitwiseOperation.XNOR);
        }
        #endregion

        #region Perform Operations + Rescale Mask
        /// <summary>Performs the AND bitwise operation into a value of any type. All its bytes are ANDed.</summary>
        /// <typeparam name="TValue">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <typeparam name="TMask">The type of the mask.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the AND operation. It will be rescaled to fit the size of the value.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe TValue ANDRescaleMask<TValue, TMask>(TValue value, TMask mask)
            where TValue : unmanaged
            where TMask : unmanaged
        {
            return PerformOperationRescaleMask(value, mask, BitwiseOperation.AND);
        }
        /// <summary>Performs the OR bitwise operation into a value of any type. All its bytes are ORed.</summary>
        /// <typeparam name="TValue">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <typeparam name="TMask">The type of the mask.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the OR operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe TValue ORRescaleMask<TValue, TMask>(TValue value, TMask mask)
            where TValue : unmanaged
            where TMask : unmanaged
        {
            return PerformOperationRescaleMask(value, mask, BitwiseOperation.OR);
        }
        /// <summary>Performs the XOR bitwise operation into a value of any type. All its bytes are XORed.</summary>
        /// <typeparam name="TValue">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <typeparam name="TMask">The type of the mask.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the XOR operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe TValue XORRescaleMask<TValue, TMask>(TValue value, TMask mask)
            where TValue : unmanaged
            where TMask : unmanaged
        {
            return PerformOperationRescaleMask(value, mask, BitwiseOperation.XOR);
        }
        /// <summary>Performs the NAND bitwise operation into a value of any type. All its bytes are NANDed.</summary>
        /// <typeparam name="TValue">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <typeparam name="TMask">The type of the mask.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the NAND operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe TValue NANDRescaleMask<TValue, TMask>(TValue value, TMask mask)
            where TValue : unmanaged
            where TMask : unmanaged
        {
            return PerformOperationRescaleMask(value, mask, BitwiseOperation.NAND);
        }
        /// <summary>Performs the NOR bitwise operation into a value of any type. All its bytes are NORed.</summary>
        /// <typeparam name="TValue">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <typeparam name="TMask">The type of the mask.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the NOR operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe TValue NORRescaleMask<TValue, TMask>(TValue value, TMask mask)
            where TValue : unmanaged
            where TMask : unmanaged
        {
            return PerformOperationRescaleMask(value, mask, BitwiseOperation.NOR);
        }
        /// <summary>Performs the XNOR bitwise operation into a value of any type. All its bytes are XNORed.</summary>
        /// <typeparam name="TValue">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <typeparam name="TMask">The type of the mask.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the XNOR operation.</param>
        /// <returns>The resulting value.</returns>
        public static unsafe TValue XNORRescaleMask<TValue, TMask>(TValue value, TMask mask)
            where TValue : unmanaged
            where TMask : unmanaged
        {
            return PerformOperationRescaleMask(value, mask, BitwiseOperation.XNOR);
        }
        #endregion

        /// <summary>Performs a bitwise operation into a value of any type.</summary>
        /// <typeparam name="TValue">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <typeparam name="TMask">The type of the mask.</typeparam>
        /// <param name="value">The value to negate.</param>
        /// <param name="mask">The mask to apply during the bitwise operation.</param>
        /// <param name="operation">The bitwise operation to perform to the value.</param>
        public static unsafe TValue PerformOperationRescaleMask<TValue, TMask>(TValue value, TMask mask, BitwiseOperation operation)
            where TValue : unmanaged
            where TMask : unmanaged
        {
            return PerformOperation(value, Rescale<TMask, TValue>(mask), operation);
        }
        /// <summary>Performs a bitwise operation into a value of any type. All its bytes are negated.</summary>
        /// <typeparam name="T">The type of the value. Its size must to be 1, 2, 4 or 8 bytes.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="mask">The mask to apply during the bitwise operation.</param>
        /// <param name="operation">The bitwise operation to perform to the value.</param>
        public static unsafe T PerformOperation<T>(T value, T mask, BitwiseOperation operation)
            where T : unmanaged
        {
            if (sizeof(T) > sizeof(Vector128<byte>))
                if (Avx.IsSupported)
                    return UseVector256(value, mask, operation);

            if (sizeof(T) > sizeof(ulong))
                if (Sse.IsSupported)
                    return UseVector128(value, mask, operation);

            return UseVector64(value, mask, operation);

            static T UseVector64(T value, T mask, BitwiseOperation operation)
            {
                T result = default;
                PointerBitwiseOperations.PerformBitwiseOperation(&value, &result, mask, 1, operation);
                return result;
            }
            static T UseVector128(T value, T mask, BitwiseOperation operation)
            {
                T result = default;
                SIMDPointerBitwiseOperations.PerformBitwiseOperationVector128CustomType(&value, &result, mask, 1, operation);
                return result;
            }
            static T UseVector256(T value, T mask, BitwiseOperation operation)
            {
                T result = default;
                SIMDPointerBitwiseOperations.PerformBitwiseOperationVector256CustomType(&value, &result, mask, 1, operation);
                return result;
            }
        }
    }
}
