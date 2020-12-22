using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions
{
    /// <summary>Provides generic extension methods for lists.</summary>
    public static class GenericListExtensions
    {
        #region Cloning
        /// <summary>Clones a list.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list to clone.</param>
        public static List<T> Clone<T>(this List<T> l) => new List<T>(l);
        /// <summary>Clones a list of lists.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list of lists to clone.</param>
        public static List<List<T>> Clone<T>(this List<List<T>> l)
        {
            var result = new List<List<T>>();
            for (int i = 0; i < l.Count; i++)
                result.Add(new List<T>(l[i]));
            return result;
        }
        #endregion

        #region Operations
        /// <summary>Removes a collection of elements from a list and returns a new list that does not contain the removed elements.</summary>
        /// <typeparam name="T">The type of the elements that are stored in the list.</typeparam>
        /// <param name="l">The list whose elements to remove.</param>
        /// <param name="elements">The elements to remove.</param>
        /// <returns>A new copy of the list that does not contain the removed elements.</returns>
        public static List<T> RemoveRange<T>(this List<T> l, IEnumerable<T> elements)
        {
            return l.RemoveRange(elements, out _);
        }
        /// <summary>Removes a collection of elements from a list and returns a new list that does not contain the removed elements.</summary>
        /// <typeparam name="T">The type of the elements that are stored in the list.</typeparam>
        /// <param name="l">The list whose elements to remove.</param>
        /// <param name="elements">The elements to remove.</param>
        /// <param name="removedElements">The number of elements that were removed from the list.</param>
        /// <returns>A new copy of the list that does not contain the removed elements.</returns>
        public static List<T> RemoveRange<T>(this List<T> l, IEnumerable<T> elements, out int removedElements)
        {
            var result = new List<T>(l);
            removedElements = result.RemoveAll(e => elements.Contains(e));
            return result;
        }

        /// <summary>Inserts an element at the start of the <seealso cref="List{T}"/>.</summary>
        /// <typeparam name="T">The type of the elements contained in the <seealso cref="List{T}"/>.</typeparam>
        /// <param name="l">The <seealso cref="List{T}"/> at whose start to insert the element.</param>
        /// <param name="element">The element to add.</param>
        /// <returns>The instance of the <seealso cref="List{T}"/> in which the new element was inserted.</returns>
        public static List<T> InsertAtStart<T>(this List<T> l, T element)
        {
            if (l == null)
                return new List<T> { element };
            l.Insert(0, element);
            return l;
        }
        /// <summary>Moves an element in the <seealso cref="List{T}"/> to a different position.</summary>
        /// <typeparam name="T">The type of the elements contained in the <seealso cref="List{T}"/>.</typeparam>
        /// <param name="l">The <seealso cref="List{T}"/> within which to move an element.</param>
        /// <param name="from">The index of the element to move.</param>
        /// <param name="to">The new index of the element.</param>
        /// <returns>The instance of the <seealso cref="List{T}"/> in which the element was moved.</returns>
        public static List<T> MoveElement<T>(this List<T> l, int from, int to)
        {
            l.Insert(to, l[from]);
            l.RemoveAt(from + (from > to ? 1 : 0));
            return l;
        }
        /// <summary>Moves an element to the end of the <seealso cref="List{T}"/>.</summary>
        /// <typeparam name="T">The type of the elements contained in the <seealso cref="List{T}"/>.</typeparam>
        /// <param name="l">The <seealso cref="List{T}"/> within which to move an element.</param>
        /// <param name="from">The index of the element to move.</param>
        /// <returns>The instance of the <seealso cref="List{T}"/> in which the element was moved.</returns>
        public static List<T> MoveElementToEnd<T>(this List<T> l, int from)
        {
            l.Insert(l.Count, l[from]);
            l.RemoveAt(from);
            return l;
        }
        /// <summary>Moves an element to the start of the <seealso cref="List{T}"/>.</summary>
        /// <typeparam name="T">The type of the elements contained in the <seealso cref="List{T}"/>.</typeparam>
        /// <param name="l">The <seealso cref="List{T}"/> within which to move an element.</param>
        /// <param name="from">The index of the element to move.</param>
        /// <returns>The instance of the <seealso cref="List{T}"/> in which the element was moved.</returns>
        public static List<T> MoveElementToStart<T>(this List<T> l, int from)
        {
            l.Insert(0, l[from]);
            l.RemoveAt(from + 1);
            return l;
        }
        #endregion

        #region Contain Checks
        /// <summary>Determines whether the list contains all the elements of an other list.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="list">The list whose elements have to be contained on the other list.</param>
        /// <param name="containedList">The list other list to check.</param>
        public static bool ContainsAll<T>(this List<T> list, List<T> containedList)
        {
            if (containedList.Count != list.Count)
                return false;
            var tempList = list.Clone();
            var tempContained = containedList.Clone();
            for (int i = 0; i < tempContained.Count; i++)
                if (!tempList.Remove(tempContained[i]))
                    return false;
            return true;
        }
        /// <summary>Determines whether the list contains all the elements of an other list in the specific order.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="list">The list whose elements have to be contained on the other list.</param>
        /// <param name="containedList">The list other list to check.</param>
        public static bool ContainsOrdered<T>(this List<T> list, List<T> containedList)
        {
            int originalIndex = 0;

            for (int i = 0; i < containedList.Count; i++)
            {
                while (originalIndex < list.Count - i)
                {
                    if (list[originalIndex].Equals(containedList[i]))
                        break;
                    originalIndex++;
                }

                originalIndex++;
            }

            return true;
        }
        /// <summary>Determines whether the list contains all the elements of an other list in any order.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="list">The list whose elements have to be contained on the other list.</param>
        /// <param name="containedList">The list other list to check.</param>
        public static bool ContainsUnordered<T>(this List<T> list, List<T> containedList) => new HashSet<T>(list).IsSupersetOf(containedList);
        #endregion

        #region Intradimensional
        /// <summary>Gets the lengths of the list of arrays.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="l">The list of arrays to get the lengths of.</param>
        public static int[] GetLengths<T>(this List<T[]> l)
        {
            int[] lengths = new int[l.Count];
            for (int i = 0; i < l.Count; i++)
                lengths[i] = l[i].Length;
            return lengths;
        }
        /// <summary>Converts the list of arrays to a two-dimensional array.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="l">The list of arrays to convert.</param>
        public static T[,] ToTwoDimensionalArray<T>(this List<T[]> l)
        {
            var ar = new T[l.Count, l.GetLengths().Max()];
            for (int i = 0; i < l.Count; i++)
                for (int j = 0; j < l[i].Length; j++)
                    ar[i, j] = l[i][j];
            return ar;
        }
        #endregion
    }
}
