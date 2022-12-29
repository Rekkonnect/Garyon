using BenchmarkDotNet.Attributes;
using System.Linq;
using static Garyon.Mathematics.GeneralMath;

namespace Garyon.Benchmarking.Mathematics.General;

public class MinMaxFunctions
{
    #region Byte
    [Benchmark]
    public void MinByteCustom()
    {
        byte min = Min((byte)1, (byte)4, (byte)12, (byte)0, (byte)5, (byte)15, (byte)3, (byte)19, (byte)255, (byte)123, (byte)131);
    }
    [Benchmark]
    public void MinByteLINQ()
    {
        byte[] bytes = { 1, 4, 12, 0, 5, 15, 3, 19, 255, 123, 131 };
        byte min = bytes.Min();
    }
    #endregion

    #region Int16
    [Benchmark]
    public void MinInt16Custom()
    {
        short min = Min((short)1, (short)4, (short)12, (short)0, (short)5, (short)15, (short)3, (short)19, (short)255, (short)123, (short)131);
    }
    [Benchmark]
    public void MinInt16LINQ()
    {
        short[] shorts = { 1, 4, 12, 0, 5, 15, 3, 19, 255, 123, 131 };
        short min = shorts.Min();
    }
    #endregion

    #region Int32
    [Benchmark]
    public void MinInt32Custom()
    {
        int min = Min(1, 4, 12, 0, 5, 15, 3, 19, 255, 123, 131);
    }
    [Benchmark]
    public void MinInt32LINQ()
    {
        int[] ints = { 1, 4, 12, 0, 5, 15, 3, 19, 255, 123, 131 };
        int min = ints.Min();
    }
    #endregion

    #region Int64
    [Benchmark]
    public void MinInt64Custom()
    {
        long min = Min(1, 4, 12, 0, 5, 15, 3, 19, 255, 123, 131);
    }
    [Benchmark]
    public void MinInt64LINQ()
    {
        long[] longs = { 1, 4, 12, 0, 5, 15, 3, 19, 255, 123, 131 };
        long min = longs.Min();
    }
    #endregion
}
