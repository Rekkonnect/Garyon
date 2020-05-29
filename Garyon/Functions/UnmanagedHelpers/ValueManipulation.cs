using Garyon.Exceptions;
using Garyon.Functions.IntrinsicsHelpers;
using Garyon.Functions.PointerHelpers;
using System;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.UnmanagedHelpers
{
    /// <summary>Provides functions for value manipulation.</summary>
    public abstract class ValueManipulation
    {
        /// <summary>Rescales a value into a different type. The returned value includes as many bytes as the target type allows from the original type, with the rest, if any, set to 0.</summary>
        /// <typeparam name="TOrigin">The type of the original value.</typeparam>
        /// <typeparam name="TTarget">The target type to rescale the value into.</typeparam>
        /// <param name="value">The value to rescale.</param>
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
        public static unsafe T NOT<T>(T value)
            where T : unmanaged
        {
            if (sizeof(T) > sizeof(Vector128<byte>))
                if (Avx.IsSupported)
                    return UseVector256(value);

            if (Sse.IsSupported)
                return UseVector128(value);

            ThrowHelper.Throw<PlatformNotSupportedException>("Get a new CPU");
            return default;

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
    }
}
