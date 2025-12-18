namespace Garyon.Objects;

/// <summary>
/// Provides extensions for types implementing <see cref="ISharedInstance"/>.
/// </summary>
public static class SharedInstanceExtensions
{
    extension<T>(T)
        where T : class, ISharedInstance, new()
    {
        /// <summary>
        /// Gets the shared instance of the type.
        /// </summary>
        public static T Shared => Singleton<T>.Instance;
    }
}
