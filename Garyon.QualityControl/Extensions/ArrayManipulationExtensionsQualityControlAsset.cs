using Garyon.Objects.Advanced;

namespace Garyon.QualityControl.Extensions
{
    public class ArrayManipulationExtensionsQualityControlAsset : QualityControlAsset
    {
        // The length is explicitly set as this to better test the remainders
        protected const int BaseArrayLength = 21 * 32;
        protected const int ArrayLength = BaseArrayLength + 31;
        // This needs to be adjusted so that for each different remainder the unsafe functions work as intended
        // Current idea: create a class that contains all these arrays and their initialization, and adjust the tests so that they test
        // the remainder storing to avoid copying too many elements before testing the actual thing, since the functions are separated anyway

        protected static byte[] OriginalByteArray;
        protected static short[] OriginalInt16Array;
        protected static int[] OriginalInt32Array;
        protected static long[] OriginalInt64Array;
        protected static sbyte[] OriginalSByteArray;
        protected static ushort[] OriginalUInt16Array;
        protected static uint[] OriginalUInt32Array;
        protected static ulong[] OriginalUInt64Array;
        protected static float[] OriginalSingleArray;
        protected static double[] OriginalDoubleArray;
        protected static decimal[] OriginalDecimalArray;
        protected static bool[] OriginalBoolArray;
        protected static char[] OriginalCharArray;

        protected static byte[] TargetByteArray;
        protected static short[] TargetInt16Array;
        protected static int[] TargetInt32Array;
        protected static long[] TargetInt64Array;
        protected static sbyte[] TargetSByteArray;
        protected static ushort[] TargetUInt16Array;
        protected static uint[] TargetUInt32Array;
        protected static ulong[] TargetUInt64Array;
        protected static float[] TargetSingleArray;
        protected static double[] TargetDoubleArray;
        protected static decimal[] TargetDecimalArray;
        protected static bool[] TargetBoolArray;
        protected static char[] TargetCharArray;

        static ArrayManipulationExtensionsQualityControlAsset()
        {
            Initialize(ref OriginalByteArray);
            Initialize(ref OriginalInt16Array);
            Initialize(ref OriginalInt32Array);
            Initialize(ref OriginalInt64Array);
            Initialize(ref OriginalSByteArray);
            Initialize(ref OriginalUInt16Array);
            Initialize(ref OriginalUInt32Array);
            Initialize(ref OriginalUInt64Array);
            Initialize(ref OriginalSingleArray);
            Initialize(ref OriginalDoubleArray);
            Initialize(ref OriginalDecimalArray);
            Initialize(ref OriginalBoolArray);
            Initialize(ref OriginalCharArray);

            Initialize(ref TargetByteArray);
            Initialize(ref TargetInt16Array);
            Initialize(ref TargetInt32Array);
            Initialize(ref TargetInt64Array);
            Initialize(ref TargetSByteArray);
            Initialize(ref TargetUInt16Array);
            Initialize(ref TargetUInt32Array);
            Initialize(ref TargetUInt64Array);
            Initialize(ref TargetSingleArray);
            Initialize(ref TargetDoubleArray);
            Initialize(ref TargetDecimalArray);
            Initialize(ref TargetBoolArray);
            Initialize(ref TargetCharArray);

            // Values are within [0, 255] for most arrays to allow easier conversion
            var r = new AdvancedRandom();
            for (int i = 0; i < ArrayLength; i++)
            {
                OriginalByteArray[i] = r.NextByte();
                OriginalInt16Array[i] = r.NextByte();
                OriginalInt32Array[i] = r.NextByte();
                OriginalInt64Array[i] = r.NextByte();
                OriginalSByteArray[i] = r.NextSByte();
                OriginalUInt16Array[i] = r.NextByte();
                OriginalUInt32Array[i] = r.NextByte();
                OriginalUInt64Array[i] = r.NextByte();
                OriginalSingleArray[i] = r.NextByte();
                OriginalDoubleArray[i] = r.NextByte();
                OriginalDecimalArray[i] = r.NextByte();
                OriginalBoolArray[i] = r.NextBoolean();
                OriginalCharArray[i] = (char)r.NextByte();
            }

            static void Initialize<T>(ref T[] array) => array = new T[ArrayLength];
        }
    }
}
