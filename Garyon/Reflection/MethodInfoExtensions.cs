using System;
using System.Reflection;

namespace Garyon.Reflection;

/// <summary>
/// Provides extensions for <see cref="MethodInfo"/>.
/// </summary>
public static class MethodInfoExtensions
{
    extension(MethodInfo method)
    {
        /// <inheritdoc cref="MethodInfo.CreateDelegate(Type)"/>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        public TDelegate CreateDelegate<TDelegate>()
            where TDelegate : Delegate
        {
#pragma warning disable CA2263 // Prefer generic overload when type is known
            return (TDelegate)method.CreateDelegate(typeof(TDelegate));
#pragma warning restore CA2263 // Prefer generic overload when type is known
        }

        /// <inheritdoc cref="MethodInfo.CreateDelegate(Type)"/>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        public TDelegate CreateDelegate<TDelegate>(object target)
            where TDelegate : Delegate
        {
#pragma warning disable CA2263 // Prefer generic overload when type is known
            return (TDelegate)method.CreateDelegate(typeof(TDelegate), target);
#pragma warning restore CA2263 // Prefer generic overload when type is known
        }

        public T? Invoke<T>(object? target, object?[]? arguments)
        {
            return (T?)method.Invoke(target, arguments);
        }
    }
}
