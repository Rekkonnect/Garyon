using System;
using System.Collections.Generic;

namespace Garyon.Extensions;

public static class CollectionToStringExtensions
{
    extension<T>(IEnumerable<T> collection)
    {
        public string ToListString(string delimiter = ", ")
        {
            return string.Join(delimiter, collection);
        }
    }
}
