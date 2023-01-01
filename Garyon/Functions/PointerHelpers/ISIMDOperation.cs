namespace Garyon.Functions.PointerHelpers;

/// <summary>Represents a SIMD operation.</summary>
public interface ISIMDOperation
{
    /// <summary>Determines whether the SIMD operation is supported in the currently executing system.</summary>
    public abstract bool IsSupported { get; }
}