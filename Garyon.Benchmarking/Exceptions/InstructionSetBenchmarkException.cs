using System;

namespace Garyon.Benchmarking.Exceptions
{
    public class InstructionSetBenchmarkException : BenchmarkException
    {
        public InstructionSetBenchmarkException() : base("The system does not support the required instruction set to test this function.") { }
        public InstructionSetBenchmarkException(string message) : base(message) { }
        public InstructionSetBenchmarkException(string message, Exception innerException) : base(message, innerException) { }
    }
}
