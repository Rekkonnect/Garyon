#if HAS_SLICES

using System;

namespace Garyon.Extensions;

/// <summary>Provides extensions for the <seealso cref="Range"/> struct.</summary>
public static class RangeExtensions
{
    /// <summary>Gets the start and the end indices of the range, given the specified length.</summary>
    /// <param name="range">The range.</param>
    /// <param name="length">The length of the collection.</param>
    /// <param name="start">The start of the range for the given length.</param>
    /// <param name="end">The end of the range for the given length.</param>
    public static void GetStartAndEnd(this Range range, int length, out int start, out int end)
    {
        start = range.Start.GetOffset(length);
        end = range.End.GetOffset(length);
    }
}

#endif
