using System;

namespace Garyon.Benchmarking.Exceptions
{
    public class BenchmarkException : Exception
    {
        public BenchmarkException()
            : base("An exception has occurred during a benchmark.") { }
        public BenchmarkException(string message)
            : base(message) { }
        public BenchmarkException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
