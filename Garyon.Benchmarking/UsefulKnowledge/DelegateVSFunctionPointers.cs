using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.CompilerServices;

namespace Garyon.Benchmarking;

public unsafe struct FunctionPointer
{
    private readonly delegate*<void> pointer;

    public FunctionPointer(delegate*<void> pointer) => this.pointer = pointer;

    public void Invoke() => pointer();
}

public unsafe class DelegateVSFunctionPointers
{
    private readonly Action action = Function;
    private readonly delegate*<void> functionPointer = &Function;
    private readonly FunctionPointer functionPointerWrapper = new(&Function);

    [Benchmark]
    public void DirectCall()
    {
        Function();
    }

    [Benchmark]
    public void Delegate()
    {
        action();
    }

    [Benchmark]
    public void FunctionPointer()
    {
        functionPointer();
    }

    [Benchmark]
    public void FunctionPointerWrapper()
    {
        functionPointerWrapper.Invoke();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Function() { }
}
