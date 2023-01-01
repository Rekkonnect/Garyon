#if HAS_INTRINSICS

using System.Runtime.Intrinsics;

namespace Garyon.Functions.IntrinsicsHelpers;

/// <summary>Contains the available types of SIMD vectors being available.</summary>
public enum SIMDVectorType
{
    /// <summary>Denotes the <seealso cref="Vector64{T}"/> type.</summary>
    Vector64 = 64,
    /// <summary>Denotes the <seealso cref="Vector128{T}"/> type.</summary>
    Vector128 = 128,
    /// <summary>Denotes the <seealso cref="Vector256{T}"/> type.</summary>
    Vector256 = 256,

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
    /// <summary>Denotes the <seealso cref="Vector512{T}"/> type. Reserved for future use.</summary>
    Vector512 = 512,
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
}

#endif
