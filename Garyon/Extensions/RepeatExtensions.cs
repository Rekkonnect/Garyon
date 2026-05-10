namespace Garyon.Extensions;

public static class RepeatExtensions
{
    extension(char c)
    {
        public RepeatedChar Repeated(int count)
        {
            return new(c, count);
        }
    }

    extension(string s)
    {
        public RepeatedString Repeated(int count)
        {
            return new(s, count);
        }
    }
}
