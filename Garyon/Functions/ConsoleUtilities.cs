using Garyon.Objects;
using System;
using static System.Console;

namespace Garyon.Functions;

/// <summary>Contains utilities for the <seealso cref="Console"/> class.</summary>
public static class ConsoleUtilities
{
    #region Console Color
    /// <summary>Sets the console colors to specified values.</summary>
    /// <param name="foreground">The foreground color set to set.</param>
    /// <param name="background">The background color set to set.</param>
    public static void SetConsoleColor(ConsoleColor foreground, ConsoleColor background)
    {
        ForegroundColor = foreground;
        BackgroundColor = background;
    }
    /// <summary>Sets the console colors to a specified <seealso cref="ConsoleColorSet"/>'s colors.</summary>
    /// <param name="set">The console color set to set.</param>
    public static void SetConsoleColor(ConsoleColorSet set)
    {
        ForegroundColor = set.ForegroundColor;
        BackgroundColor = set.BackgroundColor;
    }
    /// <summary>Gets the current console color.</summary>
    /// <returns>A <seealso cref="ConsoleColorSet"/> containing the current console colors.</returns>
    public static ConsoleColorSet GetConsoleColor() => new(ForegroundColor, BackgroundColor);
    #endregion

    #region Output
    /// <summary>Writes exception information to the console.</summary>
    /// <param name="e">The exception whose information to write.</param>
    public static void WriteExceptionInfo(Exception e)
    {
        WriteLine($"Exception: ");
        WriteLineWithColor(e.Message, ConsoleColor.DarkRed, false);
        WriteLineWithColor($"Stack Trace:", ConsoleColor.Red, false);
        WriteLineWithColor(e.StackTrace, ConsoleColor.DarkRed);
        WriteLine();
    }

    /// <summary>Writes text to the console and optionally reset the console color after writing.</summary>
    /// <param name="text">The text to write to the console.</param>
    /// <param name="resetColor">Resets the console color after writing the text, if set to <see langword="true"/>.</param>
    public static void WriteAndOptionallyResetColor(string text, bool resetColor)
    {
        Write(text);
        OptionallyResetColor(resetColor);
    }
    /// <summary>Writes text to the console, appends a newline after the written text and optionally reset the console color after writing.</summary>
    /// <param name="text">The text to write to the console.</param>
    /// <param name="resetColor">Resets the console color after writing the text, if set to <see langword="true"/>.</param>
    public static void WriteLineAndOptionallyResetColor(string text, bool resetColor) => WriteAndOptionallyResetColor($"{text}\n", resetColor);
    /// <summary>Writes text to the console with a specified foreground color.</summary>
    /// <param name="text">The text to write to the console.</param>
    /// <param name="color">The foreground color to set before writing the text.</param>
    /// <param name="resetColor">Resets the console color after writing the text, if set to <see langword="true"/>.</param>
    public static void WriteWithColor(string text, ConsoleColor color, bool resetColor = true)
    {
        ForegroundColor = color;
        WriteAndOptionallyResetColor(text, resetColor);
    }
    /// <summary>Writes text to the console with a specified color.</summary>
    /// <param name="text">The text to write to the console.</param>
    /// <param name="color">The color to set before writing the text.</param>
    /// <param name="resetColor">Resets the console color after writing the text, if set to <see langword="true"/>.</param>
    public static void WriteWithColor(string text, ConsoleColorSet color, bool resetColor = true)
    {
        SetConsoleColor(color);
        WriteAndOptionallyResetColor(text, resetColor);
    }
    /// <summary>Writes text to the console with a specified foreground color and appends a newline after the specified text.</summary>
    /// <param name="text">The text to write to the console.</param>
    /// <param name="color">The foreground color to set before writing the text.</param>
    /// <param name="resetColor">Resets the console color after writing the text, if set to <see langword="true"/>.</param>
    public static void WriteLineWithColor(string text, ConsoleColor color, bool resetColor = true) => WriteWithColor($"{text}\n", color, resetColor);
    /// <summary>Writes text to the console with a specified color and appends a newline after the specified text.</summary>
    /// <param name="text">The text to write to the console.</param>
    /// <param name="color">The color to set before writing the text.</param>
    /// <param name="resetColor">Resets the console color after writing the text, if set to <see langword="true"/>.</param>
    public static void WriteLineWithColor(string text, ConsoleColorSet color, bool resetColor = true) => WriteWithColor($"{text}\n", color, resetColor);

    /// <summary>Writes a text with the specified foreground color, optionally adds a newline after text and optionally resets the color after writing.</summary>
    /// <param name="text">The text to write.</param>
    /// <param name="color">The foreground color to set before writing the text.</param>
    /// <param name="resetColor">Resets the console color after writing the text, if set to <see langword="true"/>.</param>
    /// <param name="newlineAfterText">Adds a newline after the written text, if set to <see langword="true"/>.</param>
    public static void WriteWithColorAndOptionalNewline(string text, ConsoleColor color, bool resetColor = true, bool newlineAfterText = false)
    {
        if (newlineAfterText)
            WriteLineWithColor(text, color, resetColor);
        else
            WriteWithColor(text, color, resetColor);
    }
    /// <summary>Writes a text with the specified color, optionally adds a newline after text and optionally resets the color after writing.</summary>
    /// <param name="text">The text to write.</param>
    /// <param name="color">The color to set before writing the text.</param>
    /// <param name="resetColor">Resets the console color after writing the text, if set to <see langword="true"/>.</param>
    /// <param name="newlineAfterText">Adds a newline after the written text, if set to <see langword="true"/>.</param>
    public static void WriteWithColorAndOptionalNewline(string text, ConsoleColorSet color, bool resetColor = true, bool newlineAfterText = false)
    {
        if (newlineAfterText)
            WriteLineWithColor(text, color, resetColor);
        else
            WriteWithColor(text, color, resetColor);
    }
    #endregion

    #region Input
    /// <summary>Reads a line from the console and optionally reset the console color after reading.</summary>
    /// <param name="resetColor">Resets the console color after reading the line, if set to <see langword="true"/>.</param>
    /// <returns>The input string that was given.</returns>
    public static string ReadLineAndOptionallyResetColor(bool resetColor)
    {
        string result = ReadLine();
        OptionallyResetColor(resetColor);
        return result;
    }

    /// <summary>Reads a line from the console with a specified foreground color.</summary>
    /// <param name="color">The foreground color to set before reading the line.</param>
    /// <param name="resetColor">Resets the console color after reading the line, if set to <see langword="true"/>.</param>
    /// <returns>The input string that was given.</returns>
    public static string ReadLineWithColor(ConsoleColor color, bool resetColor = true)
    {
        ForegroundColor = color;
        return ReadLineAndOptionallyResetColor(resetColor);
    }
    /// <summary>Reads a line from the console with a specified color.</summary>
    /// <param name="color">The color to set before reading the line.</param>
    /// <param name="resetColor">Resets the console color after reading the line, if set to <see langword="true"/>.</param>
    /// <returns>The input string that was given.</returns>
    public static string ReadLineWithColor(ConsoleColorSet color, bool resetColor = true)
    {
        SetConsoleColor(color);
        return ReadLineAndOptionallyResetColor(resetColor);
    }
    /// <summary>Requests input from the console, providing a request message before doing so, with a specified foreground color for both input and output, separately provided.</summary>
    /// <param name="requestMessage">The message to write before requesting input.</param>
    /// <param name="outputColor">The foreground color of the message.</param>
    /// <param name="inputColor">The foreground color of the input.</param>
    /// <param name="resetColor">Determines whether the color will be reset after performing the operation.</param>
    /// <param name="newlineAfterMessage">Determines whether a newline should be added after the message.</param>
    /// <returns>The input string that was given.</returns>
    public static string RequestInputLine(string requestMessage, ConsoleColor outputColor, ConsoleColor inputColor, bool resetColor = true, bool newlineAfterMessage = true)
    {
        WriteWithColorAndOptionalNewline(requestMessage, outputColor, false, newlineAfterMessage);
        return ReadLineWithColor(inputColor, resetColor);
    }
    /// <summary>Requests input from the console, providing a request message before doing so, with a specified color for both input and output, separately provided.</summary>
    /// <param name="requestMessage">The message to write before requesting input.</param>
    /// <param name="outputColor">The color of the message.</param>
    /// <param name="inputColor">The color of the input.</param>
    /// <param name="resetColor">Determines whether the color will be reset after performing the operation.</param>
    /// <param name="newlineAfterMessage">Determines whether a newline should be added after the message.</param>
    /// <returns>The input string that was given.</returns>
    public static string RequestInputLine(string requestMessage, ConsoleColorSet outputColor, ConsoleColorSet inputColor, bool resetColor = true, bool newlineAfterMessage = true)
    {
        WriteWithColorAndOptionalNewline(requestMessage, outputColor, false, newlineAfterMessage);
        return ReadLineWithColor(inputColor, resetColor);
    }
    #endregion

    private static void OptionallyResetColor(bool resetColor)
    {
        if (resetColor)
            ResetColor();
    }
}
