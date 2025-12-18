using System.Text.RegularExpressions;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="Regex"/>.
/// </summary>
public static class RegexExtensions
{
    extension(Regex regex)
    {
        /// <summary>
        /// Runs the regex expression against an empty string for
        /// cold start purposes. Helpful if the regex is using the
        /// <see cref="RegexOptions.Compiled"/> option.
        /// </summary>
        public void ColdStart()
        {
            regex.Match(string.Empty);
        }
    }
}
