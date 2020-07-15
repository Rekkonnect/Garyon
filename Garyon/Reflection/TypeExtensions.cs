using System;

namespace Garyon.Reflection
{
    /// <summary>Provides extension methods for the <seealso cref="Type"/> class.</summary>
    public static class TypeExtensions
    {
        /// <summary>Determines whether a type inherits another type.</summary>
        /// <typeparam name="T">The type to check whether it is being inherited by the provided <paramref name="type"/>.</typeparam>
        /// <param name="type">The type to determine whether it inherits the type <typeparamref name="T"/>.</param>
        /// <returns>A value determining whether <paramref name="type"/> inherits <typeparamref name="T"/>.</returns>
        public static bool Inherits<T>(this Type type) => type.Inherits(typeof(T));
        /// <summary>Determines whether a type inherits another type.</summary>
        /// <param name="type">The type to determine whether it inherits the type <param name="inherited">.</param>
        /// <param name="inherited">The type to check whether it is being inherited by the provided <paramref name="type"/>.</typeparam>
        /// <returns>A value determining whether <paramref name="type"/> inherits <param name="inherited">.</returns>
        public static bool Inherits(this Type type, Type inherited) => inherited.IsAssignableFrom(type);
    }
}
