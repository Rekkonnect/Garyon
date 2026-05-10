using System;
using System.Collections.Generic;

namespace Garyon.Extensions;

public static class ReadOnlySpanExtensions
{
    extension<T>(ReadOnlySpan<T> span)
    {
        public IReadOnlyList<T> AsReadOnlyList()
        {
            // Relying on the collection expression syntax for the compiler to
            // be free to optimize this conversion from ROS to IROL, though
            // it has not been benchmarked to prove its effectiveness
            return [.. span];
        }
    }
}
