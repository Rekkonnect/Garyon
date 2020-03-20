using Garyon.Extensions;
using System;

namespace Garyon.Benchmarking
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            GenericArrayExtensions.IsArrayOfType<object[], object>();
        }
    }
}
