namespace Garyon.Extensions;

public readonly record struct RepeatedChar(char Character, int Count)
{
    public override string ToString()
    {
        if (Count is 0)
        {
            return string.Empty;
        }

        return new string(Character, Count);
    }
}

