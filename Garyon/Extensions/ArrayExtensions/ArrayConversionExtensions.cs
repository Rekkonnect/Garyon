using System;
using System.Text;

namespace Garyon.Extensions.ArrayExtensions;

public static class ArrayConversionExtensions
{
    #region string[] -> T[]
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="byte"/>[]. NOTE: This parses the numerical value of the strings, and stores the resulting values in <seealso cref="byte"/> variables. Not to be confused with <seealso cref="Encoding.GetBytes(string)"/>.</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="byte"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static byte[] ToNumericalByteArray(this string[] s)
    {
        byte[] ar = new byte[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToByte(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="short"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="short"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static short[] ToInt16Array(this string[] s)
    {
        short[] ar = new short[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToInt16(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="int"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="int"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static int[] ToInt32Array(this string[] s)
    {
        int[] ar = new int[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToInt32(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="long"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="long"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static long[] ToInt64Array(this string[] s)
    {
        long[] ar = new long[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToInt64(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="sbyte"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="sbyte"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static sbyte[] ToSByteArray(this string[] s)
    {
        sbyte[] ar = new sbyte[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToSByte(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="ushort"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="ushort"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static ushort[] ToUInt16Array(this string[] s)
    {
        ushort[] ar = new ushort[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToUInt16(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="uint"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="uint"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static uint[] ToUInt32Array(this string[] s)
    {
        uint[] ar = new uint[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToUInt32(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="ulong"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="ulong"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static ulong[] ToUInt64Array(this string[] s)
    {
        ulong[] ar = new ulong[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToUInt64(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="float"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="float"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static float[] ToSingleArray(this string[] s)
    {
        float[] ar = new float[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToSingle(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="double"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="double"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static double[] ToDoubleArray(this string[] s)
    {
        double[] ar = new double[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToDouble(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="decimal"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="decimal"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static decimal[] ToDecimalArray(this string[] s)
    {
        decimal[] ar = new decimal[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToDecimal(s[i]);
        return ar;
    }
    /// <summary>Converts this <seealso cref="string"/>[] into a <seealso cref="bool"/>[].</summary>
    /// <param name="s">The <seealso cref="string"/>[] to parse.</param>
    /// <returns>A <seealso cref="bool"/>[] that contains the numerical values of the strings at their respective positions.</returns>
    public static bool[] ToBooleanArray(this string[] s)
    {
        bool[] ar = new bool[s.Length];
        for (int i = 0; i < s.Length; i++)
            ar[i] = Convert.ToBoolean(s[i]);
        return ar;
    }
    #endregion

    #region string[,] -> T[,]
    /// <summary>Converts this <seealso cref="string"/>[,] into a <seealso cref="int"/>[,].</summary>
    /// <param name="s">The <seealso cref="string"/>[,] to parse.</param>
    /// <returns>A <seealso cref="int"/>[,] that contains the numerical values of the strings at their respective positions.</returns>
    public static int[,] ToInt32Array(this string[,] s)
    {
        int a = s.GetLength(0);
        int b = s.GetLength(1);
        int[,] ar = new int[a, b];
        for (int i = 0; i < a; i++)
            for (int j = 0; j < b; j++)
                ar[a, b] = Convert.ToInt32(s[a, b]);
        return ar;
    }
    public static double[,] ToDoubleArray(this string[,] s)
    {
        int a = s.GetLength(0);
        int b = s.GetLength(1);
        double[,] ar = new double[a, b];
        for (int i = 0; i < a; i++)
            for (int j = 0; j < b; j++)
                ar[i, j] = Convert.ToDouble(s[i, j]);
        return ar;
    }
    public static decimal[,] ToDecimalArray(this string[,] s)
    {
        int a = s.GetLength(0);
        int b = s.GetLength(1);
        decimal[,] ar = new decimal[a, b];
        for (int i = 0; i < a; i++)
            for (int j = 0; j < b; j++)
                ar[i, j] = Convert.ToDecimal(s[i, j]);
        return ar;
    }
    #endregion

    public static int[] ToInt32Array(this bool[] a)
    {
        int[] ar = new int[a.Length];
        for (int i = 0; i < a.Length; i++)
            ar[i] = Convert.ToInt32(a[i]);
        return ar;
    }
    public static bool[] ToBooleanArray(this int[] a)
    {
        bool[] ar = new bool[a.Length];
        for (int i = 0; i < a.Length; i++)
            ar[i] = Convert.ToBoolean(a[i]);
        return ar;
    }
    public static string[] ToStringArray<T>(this T[] a)
    {
        string[] result = new string[a.Length];
        for (int i = 0; i < a.Length; i++)
            result[i] = a[i].ToString();
        return result;
    }
}
