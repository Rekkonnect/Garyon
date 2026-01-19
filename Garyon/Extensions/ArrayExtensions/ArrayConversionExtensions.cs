using System;
using System.Text;

namespace Garyon.Extensions.ArrayExtensions;

public static class ArrayConversionExtensions
{
    #region string[,] -> T[,]
    /// <summary>
    /// Converts this <seealso cref="string"/>[,] into a
    /// <seealso cref="int"/>[,].
    /// </summary>
    /// <param name="s">
    /// The <seealso cref="string"/>[,] to parse.
    /// </param>
    /// <returns>
    /// A <seealso cref="int"/>[,] that contains the numerical values of the
    /// strings at their respective positions.
    /// </returns>
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
}
