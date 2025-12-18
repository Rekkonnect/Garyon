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
            return (TDelegate)method.CreateDelegate(typeof(TDelegate));
        }

        /// <inheritdoc cref="MethodInfo.CreateDelegate(Type)"/>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        public TDelegate CreateDelegate<TDelegate>(object target)
            where TDelegate : Delegate
        {
            return (TDelegate)method.CreateDelegate(typeof(TDelegate), target);
        }
    }
}
