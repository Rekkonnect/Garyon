using System;
using System.Text;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="SpanLineEnumerator"/>.
/// </summary>
/// <remarks>
/// Most extensions operate on the enumerator by reference, thus
/// propagating mutations to the original instance.
/// </remarks>
public static class SpanLineEnumeratorExtensions
{
    extension(ref SpanLineEnumerator enumerator)
    {
        /// <summary>
        /// Advances the enumerator until a non-empty line is found.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if a non-empty line was found;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool SkipEmpty()
        {
            while (true)
            {
                if (enumerator.Current is not "")
                {
                    break;
                }

                var hasNext = enumerator.MoveNext();
                if (!hasNext)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Consumes the next line in the enumerator, by advancing it
        /// and returning the line.
        /// </summary>
        /// <param name="line">The line that was fetched.</param>
        /// <returns>
        /// <see langword="true"/> if a line was fetched, otherwise
        /// <see langword="false"/> if the enumerator has concluded.
        /// </returns>
        public bool ConsumeNext(out ReadOnlySpan<char> line)
        {
            var hasNext = enumerator.MoveNext();
            line = default;
            if (hasNext)
            {
                line = enumerator.Current;
            }
            return hasNext;
        }
    }
}
