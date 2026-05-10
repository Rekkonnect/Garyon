namespace Garyon.Extensions;

public static class StringOperatorExtensions
{
    extension(string s)
    {
        public static string operator *(int count, string @string)
        {
            return @string.Repeat(count);
        }

        public static string operator *(string @string, int count)
        {
            return @string.Repeat(count);
        }
    }
}
