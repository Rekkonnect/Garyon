using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Garyon.Extensions
{
    /// <summary>Provides generic extension methods for arrays.</summary>
    public static class GenericArrayExtensions
    {
        /// <summary>Appends an element to the original array without altering it. Returns a new array which contains the appended element at its end.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array to which the element will be appended.</param>
        /// <param name="item">The item to append.</param>
        public static T[] Append<T>(this T[] a, T item) => a.InsertAt(a.Length, item);
        /// <summary>Appends the specified elements to the original array without altering it. Returns a new array which contains the appended element at its end.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array to which the elements will be appended.</param>
        /// <param name="items">The items to append.</param>
        public static T[] AppendRange<T>(this T[] a, T[] items) => a.InsertRangeAt(a.Length, items);
        /// <summary>Copies the original array and returns a new array which contains the same elements at the same indices.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array to copy.</param>
        public static T[] CopyArray<T>(this T[] a)
        {
            T[] result = new T[a.Length];
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i];
            return result;
        }
        /// <summary>Removes a number of elements from the original array at the start without affecting the original array. Returns a new array which only contains the elements that were not removed.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array whose elements to remove.</param>
        /// <param name="excludedItemsCount">The number of elements to remove from the start of the array.</param>
        public static T[] ExcludeFirst<T>(this T[] a, int excludedItemsCount)
        {
            if (excludedItemsCount <= a.Length)
            {
                T[] ar = new T[a.Length - excludedItemsCount];
                for (int i = excludedItemsCount; i < a.Length; i++)
                    ar[i - excludedItemsCount] = a[i];
                return ar;
            }
            else
                throw new ArgumentException("The excluded items must be less or equal to the size of the array.", "excludedItemsCount");
        }
        /// <summary>Removes a number of elements from the original array at the end without affecting the original array. Returns a new array which only contains the elements that were not removed.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array whose elements to remove.</param>
        /// <param name="excludedItemsCount">The number of elements to remove from the end of the array.</param>
        public static T[] ExcludeLast<T>(this T[] a, int excludedItemsCount)
        {
            if (excludedItemsCount <= a.Length)
            {
                T[] ar = new T[a.Length - excludedItemsCount];
                for (int i = 0; i < ar.Length; i++)
                    ar[i] = a[i];
                return ar;
            }
            else
                throw new ArgumentException("The excluded items must be less or equal to the size of the array.", "excludedItemsCount");
        }
        /// <summary>Inserts an element at the array at a specified index without affecting the original array. Returns a new array which contains the elements of the original array and the inserted one.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array to which the element will be inserted.</param>
        /// <param name="item">The item to insert to the array.</param>
        /// <param name="index">The index of the inserted item in the array.</param>
        public static T[] InsertAt<T>(this T[] a, int index, T item)
        {
            if (a != null)
            {
                T[] result = new T[a.Length + 1];
                result[index] = item;
                for (int i = 0; i < index; i++)
                    result[i] = a[i];
                for (int i = index; i < a.Length; i++)
                    result[i + 1] = a[i];
                return result;
            }
            else
                return new T[] { item };
        }
        /// <summary>Inserts the specified elements at the array at a specified index without affecting the original array. Returns a new array which contains the elements of the original array and the inserted one.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array to which the elements will be inserted.</param>
        /// <param name="items">The items to insert to the array.</param>
        /// <param name="index">The index of the first inserted item in the array.</param>
        public static T[] InsertRangeAt<T>(this T[] a, int index, T[] items)
        {
            if (a != null)
            {
                T[] result = new T[a.Length + items.Length];
                for (int i = 0; i < items.Length; i++)
                    result[index + i] = items[i];
                for (int i = 0; i < index; i++)
                    result[i] = a[i];
                for (int i = index; i < a.Length; i++)
                    result[i + items.Length] = a[i];
                return result;
            }
            else
                return items.CopyArray();
        }
        /// <summary>Moves an array element at a specified index to another. This affects the original array and returns its instance.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array whose element to move.</param>
        /// <param name="from">The old index of the element to move.</param>
        /// <param name="to">The new index of the element to move.</param>
        public static T[] MoveElement<T>(this T[] a, int from, int to)
        {
            T moved = a[from];
            for (int i = from; i < to; i++)
                a[i] = a[i + 1];
            for (int i = from; i > to; i--)
                a[i] = a[i - 1];
            a[to] = moved;
            return a;
        }
        /// <summary>Moves an array element at a specified index to the end of the array. This affects the original array and returns its instance.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array whose element to move.</param>
        /// <param name="from">The old index of the element to move.</param>
        public static T[] MoveElementToEnd<T>(this T[] a, int from) => a.MoveElement(from, a.Length - 1);
        /// <summary>Moves an array element at a specified index to the start of the array. This affects the original array and returns its instance.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array whose element to move.</param>
        /// <param name="from">The old index of the element to move.</param>
        public static T[] MoveElementToStart<T>(this T[] a, int from) => a.MoveElement(from, 0);
        /// <summary>Prepends an element to the original array without altering it. Returns a new array which contains the prepended element at its end.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array to which the element will be prepended.</param>
        /// <param name="item">The item to prepend.</param>
        public static T[] Prepend<T>(this T[] a, T item) => a.InsertAt(0, item);
        /// <summary>Removes an element of the array at a specified index without affecting it. Returns a new array without the removed element.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array whose element will be removed.</param>
        /// <param name="index">The index of the element to remove.</param>
        public static T[] RemoveAt<T>(this T[] a, int index)
        {
            T[] result = new T[a.Length - 1];
            for (int i = 0; i < index; i++)
                result[i] = a[i];
            for (int i = index + 1; i < a.Length; i++)
                result[i - 1] = a[i];
            return result;
        }
        /// <summary>Removes the duplicate elements of the array without affecting it. Returns a new array without the duplicate elements.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="a">The original array whose duplicate elements will be removed.</param>
        public static T[] RemoveDuplicates<T>(this T[] a)
        {
            var result = new List<T>();
            for (int i = 0; i < a.Length; i++)
                if (!result.Contains(a[i]))
                    result.Add(a[i]);
            return result.ToArray();
        }
        /// <summary>Reverses the elements of the array. Returns the instance of the original array.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="ar">The original array to reverse.</param>
        public static T[] Reverse<T>(this T[] a)
        {
            for (int i = 0; i < a.Length / 2; i++)
                a.Swap(i, a.Length - 1 - i);
            return a;
        }
        /// <summary>Sorts the array without affecting the original array. Returns a new instance containing the elements of the original array sorted.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="ar">The original array to sort.</param>
        public static T[] Sort<T>(this T[] ar)
        {
            var sorted = ar.ToList();
            sorted.Sort();
            return sorted.ToArray();
        }
        /// <summary>Swaps two elements in the array. Returns the instance of the original array.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="ar">The array whose elements to swap.</param>
        /// <param name="a">The index of the first element to swap.</param>
        /// <param name="a">The index of the second element to swap.</param>
        public static T[] Swap<T>(this T[] ar, int a, int b)
        {
            T t = ar[a];
            ar[a] = ar[b];
            ar[b] = t;
            return ar;
        }

        public static bool IsArrayOfByte<TArray>() => IsArrayOfType<TArray, byte>();
        public static bool IsArrayOfInt16<TArray>() => IsArrayOfType<TArray, short>();
        public static bool IsArrayOfInt32<TArray>() => IsArrayOfType<TArray, int>();
        public static bool IsArrayOfInt64<TArray>() => IsArrayOfType<TArray, long>();
        public static bool IsArrayOfSByte<TArray>() => IsArrayOfType<TArray, sbyte>();
        public static bool IsArrayOfUInt16<TArray>() => IsArrayOfType<TArray, ushort>();
        public static bool IsArrayOfUInt32<TArray>() => IsArrayOfType<TArray, uint>();
        public static bool IsArrayOfUInt64<TArray>() => IsArrayOfType<TArray, ulong>();
        public static bool IsArrayOfSingle<TArray>() => IsArrayOfType<TArray, float>();
        public static bool IsArrayOfDouble<TArray>() => IsArrayOfType<TArray, double>();
        public static bool IsArrayOfDecimal<TArray>() => IsArrayOfType<TArray, decimal>();

        #region Awful Autogenerated Code
        /// <summary>Awfully written function that determines whether an array type contains elements of the provided type. It only checks for multidimensional arrays (of the form [(,)*]). Jagged arrays are not taken into consideration in this implementation.</summary>
        /// <typeparam name="TArray">The type of the array.</typeparam>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <returns>Whether the given array type stores elements of the <typeparamref name="TElement"/> type.</returns>
        public static bool IsArrayOfType<TArray, TElement>()
        {
            if (typeof(TArray) == typeof(TElement[]))
                return true;
            if (typeof(TArray) == typeof(TElement[,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                return true;
            return false;
        }

        #region Extensions using GetPointer
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[] a)
            where TElement : unmanaged
            => GetPointer<TElement[], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        /// <summary>Converts an array to a pointer to its first element.</summary>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe TElement* GetPointer<TElement>(this TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,] a)
            where TElement : unmanaged
            => GetPointer<TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,], TElement>(a);
        #endregion

        /// <summary>Awfully written function that converts an array to a pointer to its first element. It only operates for multidimensional arrays (of the form [(,)*]). Jagged arrays cannot be converted to single pointers.</summary>
        /// <typeparam name="TArray">The type of the array. It must store elements of type <typeparamref name="TElement"/>.</typeparam>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="a">The array whose pointer to get.</param>
        /// <returns>A pointer to the first element of the array.</returns>
        public static unsafe TElement* GetPointer<TArray, TElement>(this TArray a)
            where TElement : unmanaged
        {
            if (typeof(TArray) == typeof(TElement[]))
                fixed (TElement* e = (TElement[])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,]))
                fixed (TElement* e = (TElement[,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,]))
                fixed (TElement* e = (TElement[,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,]))
                fixed (TElement* e = (TElement[,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,]))
                fixed (TElement* e = (TElement[,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,]))
                fixed (TElement* e = (TElement[,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            if (typeof(TArray) == typeof(TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]))
                fixed (TElement* e = (TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,])(object)a)
                    return e;
            return null;
        }
        /// <summary>Awfully written function that determines whether an array type contains elements of the provided type. It also checks for jagged arrays up to a maximum jagging level.</summary>
        /// <typeparam name="TArray">The type of the array.</typeparam>
        /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
        /// <param name="maxJaggingLevel">The maximum jagging level of the array (1 means up to [], 2 means up to [][], etc.).</param>
        /// <returns>Whether the given array type stores elements of the <typeparamref name="TElement"/> type.</returns>
        public static bool IsArrayOfType<TArray, TElement>(int maxJaggingLevel) => IsArrayOfType<TArray, TElement>(maxJaggingLevel);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static bool IsArrayOfType<TArray, TElement>(int maxJaggingLevel, int jaggingLevel = 1)
        {
            if (jaggingLevel > maxJaggingLevel)
                return false;

            if (TestTargetArrayType<TArray, TElement[]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            if (TestTargetArrayType<TArray, TElement[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,]>(maxJaggingLevel, jaggingLevel))
                return true;
            return false;
        }

        private static bool TestTargetArrayType<TArray, TTargetArray>(int maxJaggingLevel, int jaggingLevel = 1)
        {
            if (typeof(TArray) == typeof(TTargetArray))
                return true;
            if (IsArrayOfType<TArray, TTargetArray>(maxJaggingLevel, jaggingLevel + 1))
                return true;

            return false;
        }
        #endregion

        #region Array
        /// <summary>Gets the lengths of the array's dimensions.</summary>
        /// <param name="ar">The array whose dimension lengths to get.</param>
        /// <returns>An <seealso cref="int"/>[] containing the dimension lengths in order.</returns>
        public static int[] GetDimensionLengths(this Array ar)
        {
            var lengths = new int[ar.Rank];
            for (int i = 0; i < ar.Rank; i++)
                lengths[i] = ar.GetLength(i);
            return lengths;
        }
        #endregion
    }
}
