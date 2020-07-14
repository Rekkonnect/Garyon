using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Garyon.RegularExpressions
{
    /// <summary>Provides a collection of utilities to create common <seealso cref="Regex"/> instances with better readability.</summary>
    public static class RegexFactory
    {
        /// <summary>Gets the any latin letter pattern "[a-z]".</summary>
        public static Regex AnyLatinLetterPattern => new Regex(@"[a-z]");
        /// <summary>Gets the any consecutive latin letter pattern "([a-z])\1".</summary>
        public static Regex AnyConsecutiveLatinLetterPattern => new Regex(@"([a-z])\1");

        /// <summary>Constructs a <seealso cref="Regex"/> that matches a specified character.</summary>
        /// <param name="c">The character the constructed <seealso cref="Regex"/> will match.</param>
        /// <returns>The constructed <seealso cref="Regex"/> that matches the specified character.</returns>
        public static Regex CharacterPattern(char c) => new Regex(c.ToString());

        // TODO: Documentation
        public static Regex ConsecutivePatternPart(int min, int max) => new Regex($"{{{min},{max}}}");
        public static Regex ExactConsecutivePatternPart(int occurrences) => new Regex($"{{{occurrences}}}");
        public static Regex AtLeastConsecutivePatternPart(int min) => new Regex($"{{{min},}}");
        public static Regex AtMostConsecutivePatternPart(int max) => new Regex($"{{,{max}}}");

        // TODO: Add more pattern constructors

        /// <summary>Merges the provided <seealso cref="Regex"/> instances.</summary>
        /// <param name="regexes">The <seealso cref="Regex"/> instances to merge.</param>
        /// <returns>The merged <seealso cref="Regex"/>.</returns>
        public static Regex MergeRegex(params Regex[] regexes) => regexes.MergeRegex();

        /// <summary>Merges the provided <seealso cref="Regex"/> instances.</summary>
        /// <param name="regexes">The <seealso cref="Regex"/> instances to merge.</param>
        /// <returns>The merged <seealso cref="Regex"/>.</returns>
        public static Regex MergeRegex(this IEnumerable<Regex> regexes) => new Regex(regexes.Select(s => s.ToString()).Combine());
    }
}
