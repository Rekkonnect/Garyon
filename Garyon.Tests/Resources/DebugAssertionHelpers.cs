using System;
using System.Diagnostics;

namespace Garyon.Tests.Resources
{
    public static class DebugAssertionHelpers
    {
        /// <summary>Asserts whether the provided array is a valid array in the memory without causing issues.</summary>
        /// <param name="a">The array to test.</param>
        public static void TestArray(Array a)
        {
            Debug.Assert(a is object && a.Length != 0);
        }
        /// <summary>Asserts whether the provided arrays are valid arrays in the memory without causing issues.</summary>
        /// <param name="arrays">The arrays to test.</param>
        public static void TestArrays(params Array[] arrays)
        {
            foreach (Array a in arrays)
                TestArray(a);
        }
    }
}
