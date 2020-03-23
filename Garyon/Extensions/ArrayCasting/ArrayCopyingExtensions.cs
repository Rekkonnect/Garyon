using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;

namespace Garyon.Extensions.ArrayCasting
{
    public static class ArrayCopyingExtensions
    {
        /// <summary>Copies all the numerical elements of the <typeparamref name="TFrom"/>[] to a new <typeparamref name="TTo"/>[] and returns the <typeparamref name="TTo"/>[].</summary>
        /// <typeparam name="TFrom">The numerical type of the elements in the original array.</typeparam>
        /// <typeparam name="TTo">The numerical type of the elemnets in the new array.</typeparam>
        /// <param name="a">The <typeparamref name="TFrom"/>[] whose elements to copy.</param>
        /// <returns>The resulting array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TTo[] CopyToNumericalArray<TFrom, TTo>(this TFrom[] a)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            unsafe
            {
                uint length = (uint)a.Length;
                var result = new TTo[length];

                var origin = a.GetPointer();
                var target = result.GetPointer();

                if (CopyToSIMD(origin, target, length))
                    return result;

                CopyTo(origin, target, length);
                return result;
            }
        }

        private static unsafe bool CopyToSIMD<TFrom, TTo>(TFrom* origin, TTo* target, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {
            if (UnsafeArrayCopyingHelpers.CopyToArrayVector256(origin, target, length))
                return true;

            if (UnsafeArrayCopyingHelpers.CopyToArrayVector128(origin, target, length))
                return true;

            return false;
        }
        private static unsafe void CopyTo<TFrom, TTo>(TFrom* origin, TTo* result, uint length)
            where TFrom : unmanaged
            where TTo : unmanaged
        {

        }

        public static void CopyTo<TArrayFrom, TArrayTo>(this TArrayFrom original, TArrayTo target)
        {
            var a = original as Array;
            var b = target as Array;

            if (GenericArrayExtensions.IsArrayOfByte<TArrayFrom>())
            {
                if (GenericArrayExtensions.IsArrayOfByte<TArrayTo>())
                {

                }
            }
        }

        public static unsafe void CopyTo(byte* origin, short* result, uint length)
        {
            for (uint i = 0; i < length; i++)
                result[i] = (short)origin[i];
        }
    }
}
