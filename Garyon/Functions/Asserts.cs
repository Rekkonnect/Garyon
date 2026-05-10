using Garyon.Exceptions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Garyon.Functions;

/// <summary>
/// Provides assertion helpers for validating conditions and values.
/// </summary>
public static class Asserts
{
    /// <summary>
    /// Asserts that the specified condition is <see langword="true" />.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="expression">The expression text of the asserted condition.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="condition" /> is <see langword="false" />.</exception>
    public static void True(
        bool condition,
        [CallerArgumentExpression(nameof(condition))]
        string expression = "")
    {
        if (!condition)
        {
            throw new AssertionException(
                $"Expected the expression '{expression}' to be true, but was false.");
        }
    }

    /// <summary>
    /// Asserts that the specified condition is <see langword="false" />.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="expression">The expression text of the asserted condition.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="condition" /> is <see langword="true" />.</exception>
    public static void False(
        bool condition,
        [CallerArgumentExpression(nameof(condition))]
        string expression = "")
    {
        if (condition)
        {
            throw new AssertionException(
                $"Expected the expression '{expression}' to be false, but was true.");
        }
    }

    /// <summary>
    /// Asserts that the specified value is of the specified type.
    /// </summary>
    /// <typeparam name="T">The expected type.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="expression">The expression text of the asserted value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="value" /> is not of type <typeparamref name="T"/>.</exception>
    public static T OfType<T>(
        object? value,
        [CallerArgumentExpression(nameof(value))]
        string expression = "")
    {
        if (value is not T t)
        {
            throw new AssertionException(
                $"Expected '{expression}' to be of type {typeof(T)}, but was {FormatValue(value)}.");
        }
        return t;
    }

    /// <summary>
    /// Asserts that the specified value is <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="expression">The expression text of the asserted value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="value" /> is not <see langword="null" />.</exception>
    public static void Null<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))]
        string expression = "")
    {
        if (value is not null)
        {
            throw new AssertionException(
                $"Expected '{expression}' to be null, but was {FormatValue(value)}.");
        }
    }

    /// <summary>
    /// Asserts that the specified value is not <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="expression">The expression text of the asserted value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="value" /> is <see langword="null" />.</exception>
    public static void NotNull<T>(
        [NotNull]
        T? value,
        [CallerArgumentExpression(nameof(value))]
        string expression = "")
    {
        if (value is null)
        {
            throw new AssertionException(
                $"Expected '{expression}' to not be null, but was null.");
        }
    }

    /// <summary>
    /// Asserts that the specified value is equal to its default value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="expression">The expression text of the asserted value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="value" /> is not the default value.</exception>
    public static void Default<T>(
        T value,
        [CallerArgumentExpression(nameof(value))]
        string expression = "")
    {
        if (!IsDefault(value))
        {
            throw new AssertionException(
                $"Expected '{expression}' to be the default value, but was {FormatValue(value)}.");
        }
    }

    /// <summary>
    /// Asserts that the specified value is not equal to its default value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="expression">The expression text of the asserted value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="value" /> is the default value.</exception>
    public static void NotDefault<T>(
        T value,
        [CallerArgumentExpression(nameof(value))]
        string expression = "")
    {
        if (IsDefault(value))
        {
            throw new AssertionException(
                $"Expected '{expression}' to not be the default value, but it was.");
        }
    }

    /// <summary>
    /// Asserts that two values are equal.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="actual">The actual value.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="actualExpression">The expression text of the actual value.</param>
    /// <param name="expectedExpression">The expression text of the expected value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="actual" /> and <paramref name="expected" /> are not equal.</exception>
    public static void Equal<T>(
        T actual,
        T expected,
        [CallerArgumentExpression(nameof(actual))]
        string actualExpression = "",
        [CallerArgumentExpression(nameof(expected))]
        string expectedExpression = "")
    {
        if (!EqualityComparer<T>.Default.Equals(actual, expected))
        {
            throw new AssertionException(
                $"Expected '{actualExpression}' to be equal to '{expectedExpression}', but found {FormatValue(actual)} and {FormatValue(expected)} respectively.");
        }
    }

    /// <summary>
    /// Asserts that two values are not equal.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="actual">The actual value.</param>
    /// <param name="expected">The value that should not match the actual value.</param>
    /// <param name="actualExpression">The expression text of the actual value.</param>
    /// <param name="expectedExpression">The expression text of the expected value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="actual" /> and <paramref name="expected" /> are equal.</exception>
    public static void NotEqual<T>(
        T actual,
        T expected,
        [CallerArgumentExpression(nameof(actual))]
        string actualExpression = "",
        [CallerArgumentExpression(nameof(expected))]
        string expectedExpression = "")
    {
        if (EqualityComparer<T>.Default.Equals(actual, expected))
        {
            throw new AssertionException(
                $"Expected '{actualExpression}' to not be equal to '{expectedExpression}', but both were {FormatValue(actual)}.");
        }
    }

#if HAS_INUMBER
    /// <summary>
    /// Asserts that the actual value is less than the expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="actual">The actual value.</param>
    /// <param name="expected">The value that the actual value must be less than.</param>
    /// <param name="actualExpression">The expression text of the actual value.</param>
    /// <param name="expectedExpression">The expression text of the expected value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="actual" /> is not less than <paramref name="expected" />.</exception>
    public static void LessThan<T>(
        T actual,
        T expected,
        [CallerArgumentExpression(nameof(actual))]
        string actualExpression = "",
        [CallerArgumentExpression(nameof(expected))]
        string expectedExpression = "")
        where T : IComparisonOperators<T, T, bool>
    {
        if (!(actual < expected))
        {
            throw new AssertionException(
                $"Expected '{actualExpression}' to be less than '{expectedExpression}', but found {FormatValue(actual)} and {FormatValue(expected)} respectively.");
        }
    }

    /// <summary>
    /// Asserts that the actual value is less than or equal to the expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="actual">The actual value.</param>
    /// <param name="expected">The value that the actual value must be less than or equal to.</param>
    /// <param name="actualExpression">The expression text of the actual value.</param>
    /// <param name="expectedExpression">The expression text of the expected value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="actual" /> is greater than <paramref name="expected" />.</exception>
    public static void LessThanOrEqual<T>(
        T actual,
        T expected,
        [CallerArgumentExpression(nameof(actual))]
        string actualExpression = "",
        [CallerArgumentExpression(nameof(expected))]
        string expectedExpression = "")
        where T : IComparisonOperators<T, T, bool>
    {
        if (actual > expected)
        {
            throw new AssertionException(
                $"Expected '{actualExpression}' to be less than or equal to '{expectedExpression}', but found {FormatValue(actual)} and {FormatValue(expected)} respectively.");
        }
    }

    /// <summary>
    /// Asserts that the actual value is greater than the expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="actual">The actual value.</param>
    /// <param name="expected">The value that the actual value must be greater than.</param>
    /// <param name="actualExpression">The expression text of the actual value.</param>
    /// <param name="expectedExpression">The expression text of the expected value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="actual" /> is not greater than <paramref name="expected" />.</exception>
    public static void GreaterThan<T>(
        T actual,
        T expected,
        [CallerArgumentExpression(nameof(actual))]
        string actualExpression = "",
        [CallerArgumentExpression(nameof(expected))]
        string expectedExpression = "")
        where T : IComparisonOperators<T, T, bool>
    {
        if (!(actual > expected))
        {
            throw new AssertionException(
                $"Expected '{actualExpression}' to be greater than '{expectedExpression}', but found {FormatValue(actual)} and {FormatValue(expected)} respectively.");
        }
    }

    /// <summary>
    /// Asserts that the actual value is greater than or equal to the expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="actual">The actual value.</param>
    /// <param name="expected">The value that the actual value must be greater than or equal to.</param>
    /// <param name="actualExpression">The expression text of the actual value.</param>
    /// <param name="expectedExpression">The expression text of the expected value.</param>
    /// <exception cref="AssertionException">Thrown when <paramref name="actual" /> is less than <paramref name="expected" />.</exception>
    public static void GreaterThanOrEqual<T>(
        T actual,
        T expected,
        [CallerArgumentExpression(nameof(actual))]
        string actualExpression = "",
        [CallerArgumentExpression(nameof(expected))]
        string expectedExpression = "")
        where T : IComparisonOperators<T, T, bool>
    {
        if (actual < expected)
        {
            throw new AssertionException(
                $"Expected '{actualExpression}' to be greater than or equal to '{expectedExpression}', but found {FormatValue(actual)} and {FormatValue(expected)} respectively.");
        }
    }
#endif

    private static bool IsDefault<T>(T value)
    {
        return EqualityComparer<T>.Default.Equals(value, default!);
    }

    private static string FormatValue<T>(T value)
    {
        return value is null
            ? "null"
            : value.ToString() ?? string.Empty;
    }
}
