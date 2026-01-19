namespace Garyon.Objects.Strings;

/*
 * DOMAIN: Indentation of strings
 * - Discover indentation of a string (usually a code block)
 * - Indent a string without checking for its lines
 * - Indent a string's or StringBuilder's lines
 * - Append an Indentation into a StringBuilder
 */

/// <summary>
/// Represents an indentation block, as a repetition of a character multiple
/// times.
/// </summary>
/// <param name="Character">
/// The (usually whitespace) character to be used for the indentation block. It
/// may be any character, whitespace or not.
/// </param>
/// <param name="Width">
/// The number of times to repeat the character.
/// </param>
/// <remarks>
/// This will be used in more utilities in the future, including string
/// indentation, discovering string indentation patterns, etc.
/// </remarks>
public record struct Indentation(char Character, int Width)
{
    public static readonly Indentation TwoSpaces = new(WhitespaceFacts.Space, 2);
    public static readonly Indentation FourSpaces = new(WhitespaceFacts.Space, 4);
    public static readonly Indentation EightSpaces = new(WhitespaceFacts.Space, 8);
    public static readonly Indentation SingleTab = new(WhitespaceFacts.Tab, 1);

    public override readonly string ToString()
    {
        return new(Character, Width);
    }
}
