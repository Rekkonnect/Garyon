using BenchmarkDotNet.Running;
using Garyon.Benchmarking.Extensions;
using Garyon.Tests.Extensions;

namespace Garyon.Benchmarking
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<ArrayCopyingHelpersVector128>();
        }
    }
}
