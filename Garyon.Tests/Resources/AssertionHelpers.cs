using NUnit.Framework;
using System;

namespace Garyon.Tests.Resources;

public static class AssertionHelpers
{
    /// <summary>Ignores the current test, throwing the appropriate exception and displays a message related to unsupported instruction set.</summary>
    public static void UnsupportedInstructionSet()
    {
        Assert.Ignore("The system does not support the required instruction set to test this function.");
    }

    /// <summary>Asserts whether the provided array is a valid array in the memory without causing issues.</summary>
    /// <param name="a">The array to test.</param>
    public static void TestArray(Array a)
    {
        Assert.IsTrue(a is object && a.Length != 0);
    }
    /// <summary>Asserts whether the provided arrays are valid arrays in the memory without causing issues.</summary>
    /// <param name="arrays">The arrays to test.</param>
    public static void TestArrays(params Array[] arrays)
    {
        foreach (Array a in arrays)
            TestArray(a);
    }
}
