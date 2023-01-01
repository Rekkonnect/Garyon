using Garyon.Extensions.ArrayExtensions;
using System;
using System.Text;

namespace Garyon.Extensions;

/// <summary>Provides extension functions for manipulating strings.</summary>
public static class StringManipulationExtensions
{
    private const string UTF7ObsoletionMessage = "The UTF-7 encoding is insecure and should not be used. Consider using UTF-8 instead.";
    private const string UTF7ObsoletionDiagnosticID = "SYSLIB0001";
    private const string UTF7ObsoletionURLFormat = "https://aka.ms/dotnet-warnings/{0}";

    #region NOT
    /// <summary>Performs the NOT operation on a string encoded in the UTF-7 format.</summary>
    /// <param name="s">The string to perform the NOT operation on.</param>
    /// <returns>The resulting string encoded in the UTF-7 format.</returns>
#if NET5_0_OR_GREATER
    [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
#else
    [Obsolete(UTF7ObsoletionMessage)]
#endif
    public static string NOTStringUTF7(this string s) => NOTString(s, Encoding.UTF7);
    /// <summary>Performs the NOT operation on a string encoded in the UTF-8 format.</summary>
    /// <param name="s">The string to perform the NOT operation on.</param>
    /// <returns>The resulting string encoded in the UTF-8 format.</returns>
    public static string NOTStringUTF8(this string s) => NOTString(s, Encoding.UTF8);
    /// <summary>Performs the NOT operation on a string encoded in the Unicode UTF-16 format.</summary>
    /// <param name="s">The string to perform the NOT operation on.</param>
    /// <returns>The resulting string encoded in the Unicode UTF-16 format.</returns>
    public static string NOTStringUnicode(this string s) => NOTString(s, Encoding.Unicode);
    /// <summary>Performs the NOT operation on a string encoded in the UTF-32 format.</summary>
    /// <param name="s">The string to perform the NOT operation on.</param>
    /// <returns>The resulting string encoded in the UTF-32 format.</returns>
    public static string NOTStringUTF32(this string s) => NOTString(s, Encoding.UTF32);
    /// <summary>Performs the NOT operation on a string encoded in the ASCII format.</summary>
    /// <param name="s">The string to perform the NOT operation on.</param>
    /// <returns>The resulting string encoded in the ASCII format.</returns>
    public static string NOTStringASCII(this string s) => NOTString(s, Encoding.ASCII);

    /// <summary>Performs the NOT operation on a string encoded in a provided custom encoding.</summary>
    /// <param name="s">The string to perform the NOT operation on.</param>
    /// <param name="encoding">The encoding of the string.</param>
    /// <returns>The resulting string encoded in the provided encoding.</returns>
    public static string NOTString(this string s, Encoding encoding)
    {
        var bytes = encoding.GetBytes(s);
        var newBytes = bytes.NOT();
        return encoding.GetString(newBytes);
    }
    #endregion
    #region AND
    /// <summary>Performs the AND operation on a string encoded in the UTF-7 format.</summary>
    /// <param name="s">The string to perform the AND operation on.</param>
    /// <param name="mask">The value to AND each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-7 format.</returns>
#if NET5_0_OR_GREATER
    [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
#else
    [Obsolete(UTF7ObsoletionMessage)]
#endif
    public static string ANDStringUTF7(this string s, int mask) => ANDString(s, mask, Encoding.UTF7);
    /// <summary>Performs the AND operation on a string encoded in the UTF-8 format.</summary>
    /// <param name="s">The string to perform the AND operation on.</param>
    /// <param name="mask">The value to AND each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-8 format.</returns>
    public static string ANDStringUTF8(this string s, int mask) => ANDString(s, mask, Encoding.UTF8);
    /// <summary>Performs the AND operation on a string encoded in the Unicode UTF-16 format.</summary>
    /// <param name="s">The string to perform the AND operation on.</param>
    /// <param name="mask">The value to AND each byte by.</param>
    /// <returns>The resulting string encoded in the Unicode UTF-16 format.</returns>
    public static string ANDStringUnicode(this string s, int mask) => ANDString(s, mask, Encoding.Unicode);
    /// <summary>Performs the AND operation on a string encoded in the UTF-32 format.</summary>
    /// <param name="s">The string to perform the AND operation on.</param>
    /// <param name="mask">The value to AND each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-32 format.</returns>
    public static string ANDStringUTF32(this string s, int mask) => ANDString(s, mask, Encoding.UTF32);
    /// <summary>Performs the AND operation on a string encoded in the ASCII format.</summary>
    /// <param name="s">The string to perform the AND operation on.</param>
    /// <param name="mask">The value to AND each byte by.</param>
    /// <returns>The resulting string encoded in the ASCII format.</returns>
    public static string ANDStringASCII(this string s, int mask) => ANDString(s, mask, Encoding.ASCII);

    /// <summary>Performs the AND operation on a string encoded in a provided custom encoding.</summary>
    /// <param name="s">The string to perform the AND operation on.</param>
    /// <param name="mask">The value to AND each byte by.</param>
    /// <param name="encoding">The encoding of the string.</param>
    /// <returns>The resulting string encoded in the provided encoding.</returns>
    public static string ANDString(this string s, int mask, Encoding encoding)
    {
        var bytes = encoding.GetBytes(s);
        var newBytes = bytes.AND((byte)mask);
        return encoding.GetString(newBytes);
    }
    #endregion
    #region OR
    /// <summary>Performs the OR operation on a string encoded in the UTF-7 format.</summary>
    /// <param name="s">The string to perform the OR operation on.</param>
    /// <param name="mask">The value to OR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-7 format.</returns>
#if NET5_0_OR_GREATER
    [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
#else
    [Obsolete(UTF7ObsoletionMessage)]
#endif
    public static string ORStringUTF7(this string s, int mask) => ORString(s, mask, Encoding.UTF7);
    /// <summary>Performs the OR operation on a string encoded in the UTF-8 format.</summary>
    /// <param name="s">The string to perform the OR operation on.</param>
    /// <param name="mask">The value to OR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-8 format.</returns>
    public static string ORStringUTF8(this string s, int mask) => ORString(s, mask, Encoding.UTF8);
    /// <summary>Performs the OR operation on a string encoded in the Unicode UTF-16 format.</summary>
    /// <param name="s">The string to perform the OR operation on.</param>
    /// <param name="mask">The value to OR each byte by.</param>
    /// <returns>The resulting string encoded in the Unicode UTF-16 format.</returns>
    public static string ORStringUnicode(this string s, int mask) => ORString(s, mask, Encoding.Unicode);
    /// <summary>Performs the OR operation on a string encoded in the UTF-32 format.</summary>
    /// <param name="s">The string to perform the OR operation on.</param>
    /// <param name="mask">The value to OR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-32 format.</returns>
    public static string ORStringUTF32(this string s, int mask) => ORString(s, mask, Encoding.UTF32);
    /// <summary>Performs the OR operation on a string encoded in the ASCII format.</summary>
    /// <param name="s">The string to perform the OR operation on.</param>
    /// <param name="mask">The value to OR each byte by.</param>
    /// <returns>The resulting string encoded in the ASCII format.</returns>
    public static string ORStringASCII(this string s, int mask) => ORString(s, mask, Encoding.ASCII);

    /// <summary>Performs the OR operation on a string encoded in a provided custom encoding.</summary>
    /// <param name="s">The string to perform the OR operation on.</param>
    /// <param name="mask">The value to OR each byte by.</param>
    /// <param name="encoding">The encoding of the string.</param>
    /// <returns>The resulting string encoded in the provided encoding.</returns>
    public static string ORString(this string s, int mask, Encoding encoding)
    {
        var bytes = encoding.GetBytes(s);
        var newBytes = bytes.OR((byte)mask);
        return encoding.GetString(newBytes);
    }
    #endregion
    #region XOR
    /// <summary>Performs the XOR operation on a string encoded in the UTF-7 format.</summary>
    /// <param name="s">The string to perform the XOR operation on.</param>
    /// <param name="mask">The value to XOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-7 format.</returns>
#if NET5_0_OR_GREATER
    [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
#else
    [Obsolete(UTF7ObsoletionMessage)]
#endif
    public static string XORStringUTF7(this string s, int mask) => XORString(s, mask, Encoding.UTF7);
    /// <summary>Performs the XOR operation on a string encoded in the UTF-8 format.</summary>
    /// <param name="s">The string to perform the XOR operation on.</param>
    /// <param name="mask">The value to XOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-8 format.</returns>
    public static string XORStringUTF8(this string s, int mask) => XORString(s, mask, Encoding.UTF8);
    /// <summary>Performs the XOR operation on a string encoded in the Unicode UTF-16 format.</summary>
    /// <param name="s">The string to perform the XOR operation on.</param>
    /// <param name="mask">The value to XOR each byte by.</param>
    /// <returns>The resulting string encoded in the Unicode UTF-16 format.</returns>
    public static string XORStringUnicode(this string s, int mask) => XORString(s, mask, Encoding.Unicode);
    /// <summary>Performs the XOR operation on a string encoded in the UTF-32 format.</summary>
    /// <param name="s">The string to perform the XOR operation on.</param>
    /// <param name="mask">The value to XOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-32 format.</returns>
    public static string XORStringUTF32(this string s, int mask) => XORString(s, mask, Encoding.UTF32);
    /// <summary>Performs the XOR operation on a string encoded in the ASCII format.</summary>
    /// <param name="s">The string to perform the XOR operation on.</param>
    /// <param name="mask">The value to XOR each byte by.</param>
    /// <returns>The resulting string encoded in the ASCII format.</returns>
    public static string XORStringASCII(this string s, int mask) => XORString(s, mask, Encoding.ASCII);

    /// <summary>Performs the XOR operation on a string encoded in a provided custom encoding.</summary>
    /// <param name="s">The string to perform the XOR operation on.</param>
    /// <param name="mask">The value to XOR each byte by.</param>
    /// <param name="encoding">The encoding of the string.</param>
    /// <returns>The resulting string encoded in the provided encoding.</returns>
    public static string XORString(this string s, int mask, Encoding encoding)
    {
        var bytes = encoding.GetBytes(s);
        var newBytes = bytes.XOR((byte)mask);
        return encoding.GetString(newBytes);
    }
    #endregion
    #region NAND
    /// <summary>Performs the NAND operation on a string encoded in the UTF-7 format.</summary>
    /// <param name="s">The string to perform the NAND operation on.</param>
    /// <param name="mask">The value to NAND each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-7 format.</returns>
#if NET5_0_OR_GREATER
    [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
#else
    [Obsolete(UTF7ObsoletionMessage)]
#endif
    public static string NANDStringUTF7(this string s, int mask) => NANDString(s, mask, Encoding.UTF7);
    /// <summary>Performs the NAND operation on a string encoded in the UTF-8 format.</summary>
    /// <param name="s">The string to perform the NAND operation on.</param>
    /// <param name="mask">The value to NAND each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-8 format.</returns>
    public static string NANDStringUTF8(this string s, int mask) => NANDString(s, mask, Encoding.UTF8);
    /// <summary>Performs the NAND operation on a string encoded in the Unicode UTF-16 format.</summary>
    /// <param name="s">The string to perform the NAND operation on.</param>
    /// <param name="mask">The value to NAND each byte by.</param>
    /// <returns>The resulting string encoded in the Unicode UTF-16 format.</returns>
    public static string NANDStringUnicode(this string s, int mask) => NANDString(s, mask, Encoding.Unicode);
    /// <summary>Performs the NAND operation on a string encoded in the UTF-32 format.</summary>
    /// <param name="s">The string to perform the NAND operation on.</param>
    /// <param name="mask">The value to NAND each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-32 format.</returns>
    public static string NANDStringUTF32(this string s, int mask) => NANDString(s, mask, Encoding.UTF32);
    /// <summary>Performs the NAND operation on a string encoded in the ASCII format.</summary>
    /// <param name="s">The string to perform the NAND operation on.</param>
    /// <param name="mask">The value to NAND each byte by.</param>
    /// <returns>The resulting string encoded in the ASCII format.</returns>
    public static string NANDStringASCII(this string s, int mask) => NANDString(s, mask, Encoding.ASCII);

    /// <summary>Performs the NAND operation on a string encoded in a provided custom encoding.</summary>
    /// <param name="s">The string to perform the NAND operation on.</param>
    /// <param name="mask">The value to NAND each byte by.</param>
    /// <param name="encoding">The encoding of the string.</param>
    /// <returns>The resulting string encoded in the provided encoding.</returns>
    public static string NANDString(this string s, int mask, Encoding encoding)
    {
        var bytes = encoding.GetBytes(s);
        var newBytes = bytes.NAND((byte)mask);
        return encoding.GetString(newBytes);
    }
    #endregion
    #region NOR
    /// <summary>Performs the NOR operation on a string encoded in the UTF-7 format.</summary>
    /// <param name="s">The string to perform the NOR operation on.</param>
    /// <param name="mask">The value to NOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-7 format.</returns>
#if NET5_0_OR_GREATER
    [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
#else
    [Obsolete(UTF7ObsoletionMessage)]
#endif
    public static string NORStringUTF7(this string s, int mask) => NORString(s, mask, Encoding.UTF7);
    /// <summary>Performs the NOR operation on a string encoded in the UTF-8 format.</summary>
    /// <param name="s">The string to perform the NOR operation on.</param>
    /// <param name="mask">The value to NOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-8 format.</returns>
    public static string NORStringUTF8(this string s, int mask) => NORString(s, mask, Encoding.UTF8);
    /// <summary>Performs the NOR operation on a string encoded in the Unicode UTF-16 format.</summary>
    /// <param name="s">The string to perform the NOR operation on.</param>
    /// <param name="mask">The value to NOR each byte by.</param>
    /// <returns>The resulting string encoded in the Unicode UTF-16 format.</returns>
    public static string NORStringUnicode(this string s, int mask) => NORString(s, mask, Encoding.Unicode);
    /// <summary>Performs the NOR operation on a string encoded in the UTF-32 format.</summary>
    /// <param name="s">The string to perform the NOR operation on.</param>
    /// <param name="mask">The value to NOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-32 format.</returns>
    public static string NORStringUTF32(this string s, int mask) => NORString(s, mask, Encoding.UTF32);
    /// <summary>Performs the NOR operation on a string encoded in the ASCII format.</summary>
    /// <param name="s">The string to perform the NOR operation on.</param>
    /// <param name="mask">The value to NOR each byte by.</param>
    /// <returns>The resulting string encoded in the ASCII format.</returns>
    public static string NORStringASCII(this string s, int mask) => NORString(s, mask, Encoding.ASCII);

    /// <summary>Performs the NOR operation on a string encoded in a provided custom encoding.</summary>
    /// <param name="s">The string to perform the NOR operation on.</param>
    /// <param name="mask">The value to NOR each byte by.</param>
    /// <param name="encoding">The encoding of the string.</param>
    /// <returns>The resulting string encoded in the provided encoding.</returns>
    public static string NORString(this string s, int mask, Encoding encoding)
    {
        var bytes = encoding.GetBytes(s);
        var newBytes = bytes.NOR((byte)mask);
        return encoding.GetString(newBytes);
    }
    #endregion
    #region XNOR
    /// <summary>Performs the XNOR operation on a string encoded in the UTF-7 format.</summary>
    /// <param name="s">The string to perform the XNOR operation on.</param>
    /// <param name="mask">The value to XNOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-7 format.</returns>
#if NET5_0_OR_GREATER
    [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
#else
    [Obsolete(UTF7ObsoletionMessage)]
#endif
    public static string XNORStringUTF7(this string s, int mask) => XNORString(s, mask, Encoding.UTF7);
    /// <summary>Performs the XNOR operation on a string encoded in the UTF-8 format.</summary>
    /// <param name="s">The string to perform the XNOR operation on.</param>
    /// <param name="mask">The value to XNOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-8 format.</returns>
    public static string XNORStringUTF8(this string s, int mask) => XNORString(s, mask, Encoding.UTF8);
    /// <summary>Performs the XNOR operation on a string encoded in the Unicode UTF-16 format.</summary>
    /// <param name="s">The string to perform the XNOR operation on.</param>
    /// <param name="mask">The value to XNOR each byte by.</param>
    /// <returns>The resulting string encoded in the Unicode UTF-16 format.</returns>
    public static string XNORStringUnicode(this string s, int mask) => XNORString(s, mask, Encoding.Unicode);
    /// <summary>Performs the XNOR operation on a string encoded in the UTF-32 format.</summary>
    /// <param name="s">The string to perform the XNOR operation on.</param>
    /// <param name="mask">The value to XNOR each byte by.</param>
    /// <returns>The resulting string encoded in the UTF-32 format.</returns>
    public static string XNORStringUTF32(this string s, int mask) => XNORString(s, mask, Encoding.UTF32);
    /// <summary>Performs the XNOR operation on a string encoded in the ASCII format.</summary>
    /// <param name="s">The string to perform the XNOR operation on.</param>
    /// <param name="mask">The value to XNOR each byte by.</param>
    /// <returns>The resulting string encoded in the ASCII format.</returns>
    public static string XNORStringASCII(this string s, int mask) => XNORString(s, mask, Encoding.ASCII);

    /// <summary>Performs the XNOR operation on a string encoded in a provided custom encoding.</summary>
    /// <param name="s">The string to perform the XNOR operation on.</param>
    /// <param name="mask">The value to XNOR each byte by.</param>
    /// <param name="encoding">The encoding of the string.</param>
    /// <returns>The resulting string encoded in the provided encoding.</returns>
    public static string XNORString(this string s, int mask, Encoding encoding)
    {
        var bytes = encoding.GetBytes(s);
        var newBytes = bytes.XNOR((byte)mask);
        return encoding.GetString(newBytes);
    }
    #endregion
}
