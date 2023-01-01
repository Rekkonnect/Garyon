using Garyon.DataStructures;
using System;

namespace Garyon.Objects;

/// <summary>Represents a <seealso cref="ValueCounterDictionary{TKey}"/> storing keys of type <seealso cref="Type"/>.</summary>
public class TypeValueCounterDictionary : ValueCounterDictionary<Type>
{
    /// <summary>Gets the value associated with the type <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The type whose value to retrieve.</typeparam>
    /// <returns>The value associated with the type <typeparamref name="T"/>.</returns>
    public int GetValue<T>() => this[typeof(T)];
    /// <summary>Sets the value associated with the type <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The type whose value to set.</typeparam>
    /// <param name="value">The value to associate the type <typeparamref name="T"/> with.</param>
    public void SetValue<T>(int value) => this[typeof(T)] = value;
}
