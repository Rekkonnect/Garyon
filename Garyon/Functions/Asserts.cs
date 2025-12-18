using Garyon.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Garyon.Functions;

public static class Asserts
{
    // TODO: Because this is getting out of hand, backport all nullable analysis
    // attributes to those that do not contain them so they are always available
    // and can be picked up by the nullable analysis
    public static void NotNull<T>(
        [NotNull]
        T? value)
    {
        if (value is null)
        {
            throw new NullAssertionException();
        }
    }
}
