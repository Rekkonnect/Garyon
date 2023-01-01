#if HAS_SLICES

using System;
using Garyon.Extensions.ArrayExtensions;

namespace Garyon.Objects.Enumerators.ArrayEnumerators;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[Obsolete("This is a highly inefficient way to enumerate an array of an arbitrary rank. Will be removed in a future version.")]
public class ArrayEnumerator<T>
{
    private readonly int[] current;

    protected readonly int[] Lengths;

    protected int Rank => Lengths.Length;

    public int TotalElements
    {
        get
        {
            int total = 1;
            for (int i = 0; i < Rank; i++)
                total *= Lengths[i];
            return total;
        }
    }
    public int EnumeratedElements
    {
        get
        {
            int total = 0;

            int multiplier = 1;
            for (int i = 1; i <= Rank; i++)
            {
                total += current[^i] * multiplier;
                multiplier *= Lengths[^i];
            }

            return total;
        }
    }
    public int RemainingElements => TotalElements - EnumeratedElements;

    public ArrayEnumerator(int[] lengths)
    {
        if (lengths.Length != Rank)
            throw new ArgumentException("The lengths array must contain exactly as many elements as the enumerated array's rank.");
        Lengths = lengths;
        current = new int[Rank];
    }
    public ArrayEnumerator(Array a) : this(a.GetDimensionLengths()) { }

    public T GetCurrent(Array a)
    {
        if (a.Rank != Rank)
            throw new ArgumentException("The provided array must have the same rank as the enumerator's rank.");
        return (T)a.GetValue(current);
    }
    public void SetCurrent(Array a, T value)
    {
        if (a.Rank != Rank)
            throw new ArgumentException("The provided array must have the same rank as the enumerator's rank.");
        a.SetValue(value, current);
    }

    public void AssignCurrent(Array from, Array to)
    {
        SetCurrent(to, GetCurrent(from));
    }

    public int[] Advance()
    {
        bool alive = true;
        for (int i = 1; i <= Rank && alive; i++)
            if (alive = ++current[^i] >= Lengths[^i])
                current[^i] = 0;

        return current;
    }
}

#endif
