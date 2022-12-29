using BenchmarkDotNet.Attributes;
using Garyon.QualityControl.SizedStructs;

namespace Garyon.Benchmarking.UsefulKnowledge;

public class StructSizeInitializations
{
    [Benchmark]
    public void Size3()
    {
        new Struct3();
    }
    [Benchmark]
    public void Size4()
    {
        new int();
    }
    [Benchmark]
    public void Size7()
    {
        new Struct7();
    }
    [Benchmark]
    public void Size12()
    {
        new Struct12();
    }
    [Benchmark]
    public void Size15()
    {
        new Struct15();
    }
    [Benchmark]
    public void Size23()
    {
        new Struct23();
    }
    [Benchmark]
    public void Size31()
    {
        new Struct31();
    }
}
