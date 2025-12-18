#if HAS_INUMBER
using System.Numerics;
#endif

namespace Garyon.Mathematics;

/// <summary>
/// Provides mathematical functions related to sequences of numbers.
/// </summary>
public static class SequencesMath
{
#if HAS_INUMBER
    /// <summary>
    /// Calculates the sum of all integers from 1 to <paramref name="max"/> (inclusive).
    /// </summary>
    public static T Sum<T>(T max)
        where T : IBinaryInteger<T>, IShiftOperators<T, int, T>
    {
        return (max * (max + T.One)).Halve();
    }

    /// <summary>
    /// Calculates the sum of all integers from
    /// <paramref name="start"/> to <paramref name="end"/> (inclusive).
    /// </summary>
    public static T Sum<T>(T start, T end)
        where T : IBinaryInteger<T>, IShiftOperators<T, int, T>
    {
        return (end * (end + T.One) - (start - T.One) * start).Halve();
    }
#else
    /// <summary>
    /// Calculates the sum of all integers from 1 to <paramref name="max"/> (inclusive).
    /// </summary>
    public static int Sum(int max)
    {
        return max * (max + 1) / 2;
    }
    
    /// <summary>
    /// Calculates the sum of all integers from
    /// <paramref name="start"/> to <paramref name="end"/> (inclusive).
    /// </summary>
    public static int Sum(int start, int end)
    {
        return end * (end + 1) - (start - 1) * start / 2;
    }
#endif
}
