﻿using BenchmarkDotNet.Attributes;
using Garyon.Extensions;
using Garyon.Extensions.ArrayExtensions;

namespace Garyon.Benchmarking.Extensions
{
    [MinIterationTime(250)]
    public class ArrayTypeComparison
    {
        #region Autogenerated
        //[Benchmark]
        public void Array1D() => GenericArrayExtensions.IsArrayOfType<object[], object>();
        //[Benchmark]
        public void Array2D() => GenericArrayExtensions.IsArrayOfType<object[,], object>();
        //[Benchmark]
        public void Array3D() => GenericArrayExtensions.IsArrayOfType<object[,,], object>();
        //[Benchmark]
        public void Array4D() => GenericArrayExtensions.IsArrayOfType<object[,,,], object>();
        //[Benchmark]
        public void Array5D() => GenericArrayExtensions.IsArrayOfType<object[,,,,], object>();
        //[Benchmark]
        public void Array6D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,], object>();
        //[Benchmark]
        public void Array7D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,], object>();
        //[Benchmark]
        public void Array8D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,], object>();
        //[Benchmark]
        public void Array9D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,], object>();
        //[Benchmark]
        public void Array10D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,], object>();
        //[Benchmark]
        public void Array11D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array12D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array13D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array14D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array15D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array16D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array17D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array18D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array19D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array20D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array21D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array22D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array23D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array24D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array25D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array26D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array27D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array28D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array29D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array30D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array31D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>();
        //[Benchmark]
        public void Array32D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>();
        #endregion
        //[Benchmark]
        public void JaggedArray1x1D() => GenericArrayExtensions.IsArrayOfType<object[][], object>(2);
        //[Benchmark]
        public void JaggedArray1x1x1D() => GenericArrayExtensions.IsArrayOfType<object[][][], object>(3);
        //[Benchmark]
        public void JaggedArray1x1x1x1D() => GenericArrayExtensions.IsArrayOfType<object[][][][], object>(4);
        //[Benchmark]
        public void JaggedArray1x1x1x1x1D() => GenericArrayExtensions.IsArrayOfType<object[][][][][], object>(5);
        //[Benchmark]
        public void JaggedArray1x1x1x1x1x1D() => GenericArrayExtensions.IsArrayOfType<object[][][][][][], object>(6);
        //[Benchmark]
        public void JaggedArray1x2x3x4x5x6D() => GenericArrayExtensions.IsArrayOfType<object[][,][,,][,,,][,,,,][,,,,,], object>(6);
        //[Benchmark]
        public void JaggedArray6x5x4x3x2x1D() => GenericArrayExtensions.IsArrayOfType<object[,,,,,][,,,,][,,,][,,][,][], object>(6);
        [Benchmark]
        [Arguments(2)]
        //[Arguments(3)]
        //[Arguments(4)]
        //[Arguments(5)]
        //[Arguments(6)]
        //[Arguments(10)]
        //[Arguments(25)]
        //[Arguments(32)]
        public void JaggedArray32x32D(int maxJaggingLevel)
        {
            GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>(maxJaggingLevel);
        }
        [Benchmark]
        [Arguments(3)]
        //[Arguments(4)]
        //[Arguments(5)]
        //[Arguments(6)]
        //[Arguments(10)]
        //[Arguments(25)]
        //[Arguments(32)]
        public void JaggedArray32x32x32D(int maxJaggingLevel)
        {
            GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>(maxJaggingLevel);
        }
        [Benchmark]
        [Arguments(4)]
        //[Arguments(5)]
        //[Arguments(6)]
        //[Arguments(10)]
        //[Arguments(25)]
        //[Arguments(32)]
        public void JaggedArray32x32x32x32D(int maxJaggingLevel)
        {
            GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>(maxJaggingLevel);
        }
        [Arguments(5)]
        //[Arguments(6)]
        //[Arguments(10)]
        //[Arguments(25)]
        //[Arguments(32)]
        public void JaggedArray32x32x32x32x32D(int maxJaggingLevel)
        {
            GenericArrayExtensions.IsArrayOfType<object[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,][,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], object>(maxJaggingLevel);
        }
    }
}
