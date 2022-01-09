namespace Garyon.Functions
{
    /// <summary>Provides useful functions for initializing variables, fields or properties.</summary>
    public static class Initialization
    {
        /// <summary>Attempts to initialize a field of type <typeparamref name="TDerived"/>, with a suitable <typeparamref name="TDerived"/> value stored in a variable of <typeparamref name="TBase"/>.</summary>
        /// <typeparam name="TDerived">The type of the values stored in the field.</typeparam>
        /// <typeparam name="TBase">The base type, from which <typeparamref name="TDerived"/> derives.</typeparam>
        /// <param name="field">The field that will be initialized, if not already initialized. This means, if the field is not <see langword="null"/>, it will not be set.</param>
        /// <param name="value">The value to attempt to set the field to, if its runtime type is at least <typeparamref name="TDerived"/>.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="field"/> was <see langword="null"/> at the time of calling this method,
        /// and the runtime type of <paramref name="value"/> allows its value to be stored in <paramref name="field"/>, otherwise <see langword="false"/>.
        /// </returns>
        public static bool TryInitializeUpcast<TDerived, TBase>(ref TDerived field, TBase value)
            where TDerived : class, TBase
            where TBase : class
        {
            if (field is not null)
                return false;

            return TrySetUpcast(ref field, value);
        }
        /// <summary>Attempts to set a field of type <typeparamref name="TDerived"/> to a <typeparamref name="TDerived"/> value stored in a variable of <typeparamref name="TBase"/>.</summary>
        /// <typeparam name="TDerived">The type of the values stored in the field.</typeparam>
        /// <typeparam name="TBase">The base type, from which <typeparamref name="TDerived"/> derives.</typeparam>
        /// <param name="field">The field that may be set to the given value.</param>
        /// <param name="value">The value to attempt to set the field to, if its runtime type is at least <typeparamref name="TDerived"/>.</param>
        /// <returns>
        /// <see langword="true"/> if the runtime type of <paramref name="value"/> allows its value to be stored in <paramref name="field"/>, otherwise <see langword="false"/>.
        /// </returns>
        public static bool TrySetUpcast<TDerived, TBase>(ref TDerived field, TBase value)
            where TDerived : class, TBase
            where TBase : class
        {
            if (value is not TDerived derived)
                return false;

            field = derived;
            return true;
        }
    }
}