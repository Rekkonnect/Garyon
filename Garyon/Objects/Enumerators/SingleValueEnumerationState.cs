namespace Garyon.Objects.Enumerators;

/// <summary>
/// Represents the state of the <seealso cref="SingleValueEnumerator{T}"/>.
/// </summary>
public enum SingleValueEnumerationState
{
    /// <summary>
    /// The state before yielding the single value.
    /// </summary>
    Before = -1,
    /// <summary>
    /// The state where the single value is currently yielded.
    /// </summary>
    Value = 0,
    /// <summary>
    /// The state where the enumerator has yielded the value and has
    /// reached the end.
    /// </summary>
    After = 1,
}
