using System;

namespace Garyon.Functions
{
    /// <summary>Contains functions regarding checking values.</summary>
    public static class Checks
    {
        /// <summary>Determines whether two objects are equal using the <seealso cref="object.Equals(object?)"/> method without triggering a <seealso cref="NullReferenceException"/>.</summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="left">The left object to compare.</param>
        /// <param name="right">The right object to compare.</param>
        /// <returns><see langword="true"/> if both objects are equal, or both are <see langword="null"/>; otherwise <see langword="false"/>.</returns>
        /// <remarks>This implementation avoids using the <seealso cref="object.Equals(object?, object?)"/> function in order to avoid the type checking overhead.</remarks>
        public static bool SafeEquals<T>(T left, T right)
            where T : class?
        {
            return left?.Equals(right) ?? (right is null);
        }
        /// <summary>Determines whether two objects are equal using the <seealso cref="object.Equals(object?)"/> method without triggering a <seealso cref="NullReferenceException"/>.</summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="left">The left object to compare.</param>
        /// <param name="right">The right object to compare.</param>
        /// <returns><see langword="true"/> if both objects are equal, or both are <see langword="null"/>; otherwise <see langword="false"/>.</returns>
        /// <remarks>This implementation utilizes the <seealso cref="IEquatable{T}.Equals(T?)"/> function in order to avoid the type checking overhead.</remarks>
        public static bool SafeEqualsEquatable<T>(T left, T right)
            where T : class?, IEquatable<T>?
        {
            return left?.Equals(right) ?? (right is null);
        }
    }
}
