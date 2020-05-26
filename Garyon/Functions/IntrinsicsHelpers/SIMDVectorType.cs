using System.Runtime.Intrinsics;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Contains the available types of SIMD vectors being available.</summary>
    public enum SIMDVectorType
    {
        /// <summary>Denotes the <seealso cref="Vector64{T}"/> type.</summary>
        Vector64 = 64,
        /// <summary>Denotes the <seealso cref="Vector128{T}"/> type.</summary>
        Vector128 = 128,
        /// <summary>Denotes the <seealso cref="Vector256{T}"/> type.</summary>
        Vector256 = 256,
        /// <summary>Denotes the <seealso cref="Vector512{T}"/> type. Reserved for future use.</summary>
        Vector512 = 512,
    }
}
