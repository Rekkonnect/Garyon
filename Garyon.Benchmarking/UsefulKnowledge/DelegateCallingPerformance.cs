using BenchmarkDotNet.Attributes;
using System;

namespace Garyon.Benchmarking
{
    public class DelegateCallingPerformance
    {
        private static Func<int, int, int, int, int> staticFunctionDelegate;
        private static Func<int, int, int, int, int> methodDelegate;
        private static Func<int, int, int, int, int> lambdaDelegate;

        public DelegateCallingPerformance()
        {
            staticFunctionDelegate = StaticFunction;
            methodDelegate = Method;
            lambdaDelegate = (a, b, c, d) => a + b + c + d;
        }

        [Benchmark]
        public void CallMethodDelegate()
        {
            methodDelegate(5, 12, 14, 56);
        }
        [Benchmark]
        public void CallStaticFunctionDelegate()
        {
            staticFunctionDelegate(5, 12, 14, 56);
        }
        [Benchmark]
        public void CallMethod()
        {
            Method(5, 12, 14, 56);
        }
        [Benchmark]
        public void CallStaticFunction()
        {
            StaticFunction(5, 12, 14, 56);
        }
        [Benchmark]
        public void CallLambda()
        {
            ((Func<int, int, int, int, int>)((a, b, c, d) => a + b + c + d))(5, 12, 14, 56);
        }
        [Benchmark]
        public void CallLambdaDelegate()
        {
            lambdaDelegate(5, 12, 14, 56);
        }

        private int Method(int a, int b, int c, int d)
        {
            return a + b + c + d;
        }
        private static int StaticFunction(int a, int b, int c, int d)
        {
            return a + b + c + d;
        }
    }
}
