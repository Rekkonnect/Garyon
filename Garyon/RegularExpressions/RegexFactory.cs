using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Garyon.RegularExpressions;

/// <summary>Provides a collection of utilities to create common <seealso cref="Regex"/> instances with better readability.</summary>
public static class RegexFactory
{
    /// <summary>Gets the any latin letter pattern "[a-z]".</summary>
    public static Regex AnyLatinLetterPattern => new(@"[a-z]");
    /// <summary>Gets the any consecutive latin letter pattern "([a-z])\1".</summary>
    public static Regex AnyConsecutiveLatinLetterPattern => new(@"([a-z])\1");

    /// <summary>Constructs a <seealso cref="Regex"/> that matches a specified character.</summary>
    /// <param name="c">The character the constructed <seealso cref="Regex"/> will match.</param>
    /// <returns>The constructed <seealso cref="Regex"/> that matches the specified character.</returns>
    public static Regex CharacterPattern(char c) => new(c.ToString());

    /// <summary>Gets the consecutive character pattern part "{<paramref name="min"/>,<paramref name="max"/>}".</summary>
    /// <param name="min">The minimum number of occurrences that the pattern will match.</param>
    /// <param name="max">The maximum number of occurrences that the pattern will match.</param>
    /// <returns>The pattern part that indicates matching consecutive characters.</returns>
    public static Regex ConsecutivePatternPart(int min, int max) => new($"{{{min},{max}}}");
    /// <summary>Gets the consecutive character pattern part "{<paramref name="occurrences"/>}".</summary>
    /// <param name="occurrences">The exact number of occurrences that the pattern will match.</param>
    /// <returns>The pattern part that indicates matching consecutive characters.</returns>
    public static Regex ExactConsecutivePatternPart(int occurrences) => new($"{{{occurrences}}}");
    /// <summary>Gets the consecutive character pattern part "{<paramref name="min"/>,}".</summary>
    /// <param name="min">The minimum number of occurrences that the pattern will match.</param>
    public static Regex AtLeastConsecutivePatternPart(int min) => new($"{{{min},}}");
    /// <summary>Gets the consecutive character pattern part "{,<paramref name="max"/>}".</summary>
    /// <param name="max">The maximum number of occurrences that the pattern will match.</param>
    /// <returns>The pattern part that indicates matching consecutive characters.</returns>
    public static Regex AtMostConsecutivePatternPart(int max) => new($"{{,{max}}}");

    // TODO: Add more pattern constructors

    /// <summary>Merges the provided <seealso cref="Regex"/> instances.</summary>
    /// <param name="regexes">The <seealso cref="Regex"/> instances to merge.</param>
    /// <returns>The merged <seealso cref="Regex"/>.</returns>
    public static Regex MergeRegex(params Regex[] regexes) => regexes.MergeRegex();

    /// <summary>Merges the provided <seealso cref="Regex"/> instances.</summary>
    /// <param name="regexes">The <seealso cref="Regex"/> instances to merge.</param>
    /// <returns>The merged <seealso cref="Regex"/>.</returns>
    public static Regex MergeRegex(this IEnumerable<Regex> regexes) => new(regexes.Select(s => s.ToString()).Combine());
}
