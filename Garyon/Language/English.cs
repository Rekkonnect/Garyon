using Garyon.RegularExpressions;
using System.Text.RegularExpressions;

namespace Garyon.Language
{
    public abstract class English : BaseLanguage
    {
        public static bool HasExactConsecutiveSameLetters(string word, int exact)
        {
            return HasConsecutiveLetters(word, RegexFactory.AnyConsecutiveLatinLetterPattern, RegexFactory.AtLeastConsecutivePatternPart(exact), RegexOptions.IgnoreCase);
        }
        public static bool HasAtLeastConsecutiveSameLetters(string word, int min)
        {
            return HasConsecutiveLetters(word, RegexFactory.AnyConsecutiveLatinLetterPattern, RegexFactory.AtLeastConsecutivePatternPart(min), RegexOptions.IgnoreCase);
        }
        public static bool HasAtMostConsecutiveSameLetters(string word, int max)
        {
            return HasConsecutiveLetters(word, RegexFactory.AnyConsecutiveLatinLetterPattern, RegexFactory.AtMostConsecutivePatternPart(max), RegexOptions.IgnoreCase);
        }
        public static bool HasConsecutiveSameLetters(string word, int min, int max)
        {
            return HasConsecutiveLetters(word, RegexFactory.AnyConsecutiveLatinLetterPattern, RegexFactory.ConsecutivePatternPart(min, max), RegexOptions.IgnoreCase);
        }

        private static bool HasConsecutiveLetters(string word, Regex letterRegex, Regex consecutive, RegexOptions options)
        {
            return Regex.IsMatch(word, $@"{letterRegex}{consecutive}", options);
        }

        public static bool IsPotentialWord(string word)
        {
            // No word will ever contain 3 or more consecutive same letters
            if (HasAtLeastConsecutiveSameLetters(word, 3))
                return false;

            return true;
        }
    }
}
