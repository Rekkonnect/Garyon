using Garyon.Exceptions;
using Garyon.Functions;
using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>Contains extensions for the <seealso cref="IEnumerable{T}"/> interface.</summary>
public static partial class IEnumerableExtensions
{
    private static void VerifyNonEmptyCollection<T>(IEnumerable<T>? source)
    {
        if (source?.Any() is not true)
        {
            ThrowHelper.Throw<ArgumentException>("The collection must be non-null and contain at least one element.");
        }
    }
}
