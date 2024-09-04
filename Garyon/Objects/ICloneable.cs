using System;

namespace Garyon.Objects;

/// <summary>
/// Denotes that an object is cloneable into a target type, usually itself.
/// </summary>
/// <typeparam name="T">
/// The type of the cloned object. Usually it's the type itself that implements this interface.
/// </typeparam>
public interface ICloneable<T> : ICloneable
    where T : notnull
{
#if HAS_INTERFACE_DIMS
    object ICloneable.Clone() => Clone();
#endif

    /// <summary>
    /// Clones this instance into a new object and returns the cloned object.
    /// </summary>
    /// <returns>The cloned object.</returns>
    public new T Clone();
}
