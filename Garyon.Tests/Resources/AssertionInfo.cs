using NUnit.Framework;

namespace Garyon.Tests.Resources
{
    /// <summary>Contains information about an assertion process.</summary>
    /// <typeparam name="T">The type of the values that will be used in the assertion.</typeparam>
    public class AssertionInfo<T>
    {
        /// <summary>The expected value.</summary>
        public T Expected { get; set; }
        /// <summary>The actual value.</summary>
        public T Actual { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="AssertionInfo{T}"/> class.</summary>
        public AssertionInfo() { }
        /// <summary>Initializes a new instance of the <seealso cref="AssertionInfo{T}"/> class.</summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public AssertionInfo(T expected, T actual)
        {
            Expected = expected;
            Actual = actual;
        }

        /// <summary>Asserts the equality of the expected and the actual values.</summary>
        public void AssertEquality()
        {
            Assert.AreEqual(Expected, Actual);
        }
    }
}
