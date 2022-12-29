using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Garyon.QualityControl.Extensions;

namespace Garyon.Benchmarking.Extensions;

//[HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses)]
public class ArrayCopyingHelpersUnoptimized : ArrayManipulationExtensionsQualityControlAsset
{
    #region byte[] -> T[]
    [Benchmark]
    [BenchmarkCategory("Byte > Byte", "Unoptimized")]
    public void ByteToByteArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetByteArray[i] = OriginalByteArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Byte > Int16", "Unoptimized")]
    public void ByteToInt16Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt16Array[i] = OriginalByteArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Byte > Int32", "Unoptimized")]
    public void ByteToInt32Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt32Array[i] = OriginalByteArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Byte > Int64", "Unoptimized")]
    public void ByteToInt64Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt64Array[i] = OriginalByteArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Byte > Single", "Unoptimized")]
    public void ByteToSingleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetSingleArray[i] = OriginalByteArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Byte > Double", "Unoptimized")]
    public void ByteToDoubleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetDoubleArray[i] = OriginalByteArray[i];
    }
    #endregion
    #region short[] -> T[]
    [Benchmark]
    [BenchmarkCategory("Int16 > Byte", "Unoptimized")]
    public void Int16ToByteArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetByteArray[i] = (byte)OriginalInt16Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int16 > Int16", "Unoptimized")]
    public void Int16ToInt16Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt16Array[i] = OriginalInt16Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int16 > Int32", "Unoptimized")]
    public void Int16ToInt32Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt32Array[i] = OriginalInt16Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int16 > Int64", "Unoptimized")]
    public void Int16ToInt64Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt64Array[i] = OriginalInt16Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int16 > Single", "Unoptimized")]
    public void Int16ToSingleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetSingleArray[i] = OriginalInt16Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int16 > Double", "Unoptimized")]
    public void Int16ToDoubleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetDoubleArray[i] = OriginalInt16Array[i];
    }
    #endregion
    #region int[] -> T[]
    [Benchmark]
    [BenchmarkCategory("Int32 > Byte", "Unoptimized")]
    public void Int32ToByteArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetByteArray[i] = (byte)OriginalInt32Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int32 > Int16", "Unoptimized")]
    public void Int32ToInt16Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt16Array[i] = (short)OriginalInt32Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int32 > Int32", "Unoptimized")]
    public void Int32ToInt32Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt32Array[i] = OriginalInt32Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int32 > Int64", "Unoptimized")]
    public void Int32ToInt64Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt64Array[i] = OriginalInt32Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int32 > Single", "Unoptimized")]
    public void Int32ToSingleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetSingleArray[i] = OriginalInt32Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int32 > Double", "Unoptimized")]
    public void Int32ToDoubleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetDoubleArray[i] = OriginalInt32Array[i];
    }
    #endregion
    #region long[] -> T[]
    [Benchmark]
    [BenchmarkCategory("Int64 > Byte", "Unoptimized")]
    public void Int64ToByteArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetByteArray[i] = (byte)OriginalInt64Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int64 > Int16", "Unoptimized")]
    public void Int64ToInt16Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt16Array[i] = (short)OriginalInt64Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int64 > Int32", "Unoptimized")]
    public void Int64ToInt32Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt32Array[i] = (int)OriginalInt64Array[i];
    }
    [Benchmark]
    [BenchmarkCategory("Int64 > Int64", "Unoptimized")]
    public void Int64ToInt64Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt64Array[i] = OriginalInt64Array[i];
    }
    #endregion
    #region float[] -> T[]
    [Benchmark]
    [BenchmarkCategory("Single > Int32", "Unoptimized")]
    public void SingleToInt32Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt32Array[i] = (int)OriginalSingleArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Single > Single", "Unoptimized")]
    public void SingleToSingleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetSingleArray[i] = OriginalSingleArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Single > Double", "Unoptimized")]
    public void SingleToDoubleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetDoubleArray[i] = OriginalSingleArray[i];
    }
    #endregion
    #region double[] -> T[]
    [Benchmark]
    [BenchmarkCategory("Double > Int32", "Unoptimized")]
    public void DoubleToInt32Array()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetInt32Array[i] = (int)OriginalDoubleArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Double > Single", "Unoptimized")]
    public void DoubleToSingleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetSingleArray[i] = (float)OriginalDoubleArray[i];
    }
    [Benchmark]
    [BenchmarkCategory("Double > Double", "Unoptimized")]
    public void DoubleToDoubleArray()
    {
        for (int i = 0; i < ArrayLength; i++)
            TargetDoubleArray[i] = OriginalSingleArray[i];
    }
    #endregion
}
