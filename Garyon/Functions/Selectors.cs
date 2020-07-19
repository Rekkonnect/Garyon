namespace Garyon.Functions
{
    /// <summary>Contains functions that can be used as selectors in functions that manipulate objects. Using these functions is advised over lambdas, as lambdas reduce performance.</summary>
    public static class Selectors
    {
        /// <summary>Returns the provided object without performing any manipulations.</summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="o">The provided object.</param>
        /// <returns>The provided object without manipulating it.</returns>
        public static T SelfObjectReturner<T>(T o) => o;

        /// <summary>Returns <see langword="null"/> without caring about the provided object.</summary>
        /// <typeparam name="T">The type of the object. Must be a reference type.</typeparam>
        /// <param name="o">The provided object.</param>
        /// <returns><see langword="null"/>, ignoring the provided object.</returns>
        public static T NullReturner<T>(T o)
            where T : class
        {
            return null;
        }
        /// <summary>Returns <see langword="default"/> without caring about the provided object.</summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="o">The provided object.</param>
        /// <returns><see langword="default"/>, ignoring the provided object.</returns>
        public static T DefaultValueReturner<T>(T o)
        {
            return default;
        }
    }
}
