using Garyon.Objects;
using System;
using System.Collections.Generic;

namespace Garyon.Extensions;

public static partial class IEnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>Performs an action on each of the elements contained in the collection.</summary>
        /// <param name="action">The action to perform on each of the elements.</param>
        public void ForEach(Action<T> action)
        {
            foreach (T e in source)
                action(e);
        }

        /// <summary>Performs an action on each of the elements contained in the collection.</summary>
        /// <param name="action">The action to perform on each of the elements.</param>
        public void ForEach(IndexedEnumeratedElementAction<T> action)
        {
            foreach (var (index, e) in source.WithIndex())
                action(index, e);
        }
    }
}
