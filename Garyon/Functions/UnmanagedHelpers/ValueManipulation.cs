namespace Garyon.Functions.UnmanagedHelpers;

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
}
