using BenchmarkDotNet.Running;
using Garyon.Benchmarking.Extensions;

namespace Garyon.Benchmarking
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunningFunctions.RunArrayCopyingHelpers();
        }
    }
}
