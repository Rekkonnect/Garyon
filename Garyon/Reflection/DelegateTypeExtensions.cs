using Garyon.Exceptions;
using System;
using System.Linq;

namespace Garyon.Reflection
{
    /// <summary>Provides extensions for delegate <seealso cref="Type"/> objects.</summary>
    public static class DelegateTypeExtensions
    {
        /// <summary>Gets the types of the parameters of the delegate.</summary>
        /// <typeparam name="T">The type of the delegate whose parameter types to get.</typeparam>
        /// <returns>An array containing the types of the parameters of the delegate.</returns>
        public static Type[] GetDelegateParameterTypes<T>()
            where T : Delegate
        {
            return typeof(T).GetDelegateParameterTypes();
        }

        /// <summary>Gets the types of the parameters of the delegate.</summary>
        /// <param name="delegateType">The type of the delegate whose parameter types to get.</param>
        /// <returns>An array containing the types of the parameters of the delegate.</returns>
        public static Type[] GetDelegateParameterTypes(this Type delegateType)
        {
            if (!delegateType.IsDelegate())
                ThrowHelper.Throw<ArgumentException>("The provided type must be a delegate type.");

            return delegateType.GetMethod("Invoke").GetParameters().Select(p => p.ParameterType).ToArray();
        }
    }
}
