namespace Garyon.Extensions;

public readonly record struct RepeatedString(string String, int Count)
{
    public int Length => String.Length * Count;

    public override string ToString()
    {
        return String.Repeat(Count);
    }
}
