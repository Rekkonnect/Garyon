using Garyon.Functions.Arrays;
using Garyon.Functions.PointerHelpers;
using System;
using System.CodeDom.Compiler;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Garyon.Extensions.ArrayExtensions.ArrayConverting
{
    public static class ArrayConvertingExtensions
    {
        /// <summary>Copies all the numerical elements of the <typeparamref name="TFrom"/>[] to a new <typeparamref name="TTo"/>[] and returns the <typeparamref name="TTo"/>[].</summary>
        /// <typeparam name="TFrom">The numerical type of the elements in the original array.</typeparam>
        /// <typeparam name="TTo">The numerical type of the elements in the new array.</typeparam>
        /// <param name="a">The <typeparamref name="TFrom"/>[] whose elements to copy.</param>
        /// <returns>The resulting array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe TTo[] CopyToNumericalArray<TFrom, TTo>(this TFrom[] a)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            uint length = (uint)a.Length;
            var result = new TTo[length];

            fixed (TFrom* origin = a)
            fixed (TTo* target = result)
            {
                if (CopyToSIMD(origin, target, length))
                    return result;

                PointerConversion.ConvertTo(origin, target, (int)length);
            }

            return result;
        }

        private static unsafe bool CopyToSIMD<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (SIMDPointerConversion.CopyToArrayVector256(origin, target, length))
                return true;

            if (SIMDPointerConversion.CopyToArrayVector128(origin, target, length))
                return true;

            return false;
        }

        public static void CopyTo<TArrayFrom, TArrayTo>(this TArrayFrom original, TArrayTo target)
        {
            var a = original as Array;
            var b = target as Array;

            unsafe
            {
                if (ArrayIdentification.IsArrayOfByte<TArrayFrom>())
                {
                    if (ArrayIdentification.IsArrayOfByte<TArrayTo>())
                    {
                        
                    }
                }
            }
        }
    }
}
