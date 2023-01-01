using System.Collections.Generic;

namespace Garyon.Functions;

/// <summary>Contains functions that can be used as selectors in functions that manipulate objects. Using these functions is advised over lambdas, as lambdas create a new delegate instance, effectively reducing performance.</summary>
public static class Selectors
{
    #region General
    /// <summary>Returns the provided object without performing any manipulations.</summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="value">The provided object.</param>
    /// <returns>The provided object without manipulating it.</returns>
    public static T SelfObjectReturner<T>(T value) => value;

    /// <summary>Returns <see langword="null"/> without caring about the provided object.</summary>
    /// <typeparam name="T">The type of the object. Must be a reference type.</typeparam>
    /// <param name="value">The provided object.</param>
    /// <returns><see langword="null"/>, ignoring the provided object.</returns>
    public static T NullReturner<T>(T value)
        where T : class
    {
        return null;
    }
    /// <summary>Returns <see langword="default"/> without caring about the provided object.</summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="value">The provided object.</param>
    /// <returns><see langword="default"/>, ignoring the provided object.</returns>
    public static T DefaultValueReturner<T>(T value)
    {
        return default;
    }
    #endregion

    #region Nullable
    /// <summary>Returns the value in a nullable struct object without checking whether it has a value.</summary>
    /// <typeparam name="T">The type of the struct.</typeparam>
    /// <param name="value">The provided nullable struct object.</param>
    /// <returns>The value stored in the nullable struct object.</returns>
    /// <remarks><see cref="ValueOrDefaultReturner{T}(T?)"/> checks for whether the nullable struct object has a value.</remarks>
    public static T ValueReturner<T>(T? value)
        where T : struct
    {
        return value.Value;
    }
    /// <summary>Returns the value in a nullable struct object, if it has any, otherwise <see langword="default"/>.</summary>
    /// <typeparam name="T">The type of the struct.</typeparam>
    /// <param name="value">The provided nullable struct object.</param>
    /// <returns>The value stored in the nullable struct object.</returns>
    public static T ValueOrDefaultReturner<T>(T? value)
        where T : struct
    {
        return value.GetValueOrDefault();
    }
    #endregion

    #region Key/Value Pair
    /// <summary>Returns the key in a <seealso cref="KeyValuePair{TKey, TValue}"/> object.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="kvp">The provided <seealso cref="KeyValuePair{TKey, TValue}"/> object.</param>
    /// <returns>The key stored in the <seealso cref="KeyValuePair{TKey, TValue}"/> object.</returns>
    public static TKey KeyReturner<TKey, TValue>(KeyValuePair<TKey, TValue> kvp)
    {
        return kvp.Key;
    }
    /// <summary>Returns the value in a <seealso cref="KeyValuePair{TKey, TValue}"/> object.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="kvp">The provided <seealso cref="KeyValuePair{TKey, TValue}"/> object.</param>
    /// <returns>The value stored in the <seealso cref="KeyValuePair{TKey, TValue}"/> object.</returns>
    public static TValue ValueReturner<TKey, TValue>(KeyValuePair<TKey, TValue> kvp)
    {
        return kvp.Value;
    }
    #endregion
}
