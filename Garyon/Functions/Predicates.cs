using System;

namespace Garyon.Functions
{
    /// <summary>Contains functions that can be used as predicates in functions that filter collections. Using these functions is advised over lambdas, as lambdas create a new delegate instance, effectively reducing performance.</summary>
    public static class Predicates
    {
        #region General
        /// <summary>Determines whether the provided element is not <see langword="null"/>.</summary>
        /// <typeparam name="T">The type of the element.</typeparam>
        /// <param name="element">The element.</param>
        /// <returns><see langword="true"/> if the element is not <see langword="null"/>, otherwise <see langword="false"/>.</returns>
        /// <remarks>For nullable structs, consider using <see cref="HasValue{T}(T?)"/>.</remarks>
        public static bool NotNull<T>(T element) => element != null;
        /// <summary>Determines whether the provided element is <see langword="null"/>.</summary>
        /// <typeparam name="T">The type of the element.</typeparam>
        /// <param name="element">The element.</param>
        /// <returns><see langword="true"/> if the element is <see langword="null"/>, otherwise <see langword="false"/>.</returns>
        /// <remarks>For nullable structs, consider using <see cref="DoesNotHaveValue{T}(T?)"/>.</remarks>
        public static bool Null<T>(T element) => element == null;
        #endregion

        #region Nullable
        /// <summary>Determines whether the provided nullable struct object has a value.</summary>
        /// <typeparam name="T">The type of the struct.</typeparam>
        /// <param name="element">The nullable struct object.</param>
        /// <returns>The value equal to <seealso cref="Nullable{T}.HasValue"/> for the provided object.</returns>
        public static bool HasValue<T>(T? element)
            where T : struct
        {
            return element.HasValue;
        }
        /// <summary>Determines whether the provided nullable struct object does not have a value.</summary>
        /// <typeparam name="T">The type of the struct.</typeparam>
        /// <param name="element">The nullable struct object.</param>
        /// <returns>The value opposite to <seealso cref="Nullable{T}.HasValue"/> for the provided object.</returns>
        public static bool DoesNotHaveValue<T>(T? element)
            where T : struct
        {
            return !element.HasValue;
        }
        #endregion
    }
}
