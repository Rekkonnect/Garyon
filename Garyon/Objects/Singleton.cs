namespace Garyon.Objects;

/// <summary>
/// Provides a single instance of a type.
/// </summary>
/// <typeparam name="T">The type whose single instance to hold.</typeparam>
public sealed class Singleton<T>
    where T : new()
{
    /// <summary>
    /// The single instance of the type.
    /// </summary>
    /// <remarks>
    /// It is not required that the type is not initialized elsewhere.
    /// This can also be used as a shared instance.
    /// </remarks>
    public static readonly T Instance = new();
}
