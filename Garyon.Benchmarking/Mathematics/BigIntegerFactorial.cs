using BenchmarkDotNet.Attributes;
using Garyon.Objects;
using System.Numerics;

namespace Garyon.Benchmarking.Mathematics
{
    public class BigIntegerFactorial
    {
        [Params(1, 2, 3, 4, 5, 6, 7, 9, 12, 13, 15, 22, 27, 39, 46, 61, 87, 113, 165, 279, 367, 483, 518, 699, 834, 1135, 1389)]
        public long Number;

        [Benchmark(Baseline = true)]
        public void NonCacheableLinear()
        {
            NonCacheableLinear(Number);
        }
        [Benchmark]
        public void NonCacheableOptimized()
        {
            NonCacheableOptimized(Number);
        }
        [Benchmark]
        public void CacheableLinear()
        {
            CacheableLinear(Number);
        }
        [Benchmark]
        public void CacheableOptimized()
        {
            CacheableOptimized(Number);
        }

        private static BigInteger NonCacheableLinear(long n)
        {
            BigInteger result = 1;
            for (long i = 2; i <= n; i++)
                result *= i;
            return result;
        }
        private static BigInteger NonCacheableOptimized(long n)
        {
            BigInteger result = 1;

            long currentFactor = n;
            long currentIncrement = currentFactor - 2;

            long bound = n / 2;

            for (long i = 1; i <= bound; i++)
            {
                result *= currentFactor;

                currentFactor += currentIncrement;
                currentIncrement -= 2;
            }

            if (n % 2 == 1)
                result *= n / 2 + 1;

            return result;
        }
        private static BigInteger CacheableLinear(long n)
        {
            CacheableBigInteger result = 1;
            for (long i = 2; i <= n; i++)
                result.Multiply(i);
            return result;
        }
        private static BigInteger CacheableOptimized(long n)
        {
            CacheableBigInteger result = 1;

            long currentFactor = n;
            long currentIncrement = currentFactor - 2;

            long bound = n / 2;

            for (long i = 1; i <= bound; i++)
            {
                result.Multiply(currentFactor);

                currentFactor += currentIncrement;
                currentIncrement -= 2;
            }

            if (n % 2 == 1)
                result.Multiply(n / 2 + 1);

            return result;
        }
    }
}
