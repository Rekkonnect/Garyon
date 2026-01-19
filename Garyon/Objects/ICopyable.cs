namespace Garyon.Objects;

/// <summary>
/// Denotes that an object is copyable into a target instance, usually of
/// another type.
/// </summary>
/// <typeparam name="TTarget">
/// The type of the object to copy onto.
/// </typeparam>
public interface ICopyable<in TTarget>
{
    /// <summary>
    /// Copies the object into a target object. This method is intended to copy
    /// data that the target object supports and requires.
    /// </summary>
    /// <param name="target">
    /// The target object that will have its state overwritten with this
    /// object's data.
    /// </param>
    /// <remarks>
    /// This can be useful for mapping objects like DTOs into entities, or other
    /// likewise correlated objects.
    /// </remarks>
    public void CopyTo(TTarget target);
}
