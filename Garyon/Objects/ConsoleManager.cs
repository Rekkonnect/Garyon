using Garyon.Functions;
using System;

namespace Garyon.Objects;

/// <summary>Provides utilities for reading or writing to the console using specific colors.</summary>
public class ConsoleManager
{
    /// <summary>The output color set that is being applied to the console on each writing operation.</summary>
    public ConsoleColorSet OutputColorSet;
    /// <summary>The input color set that is being applied to the console on each reading operation.</summary>
    public ConsoleColorSet InputColorSet;

    /// <summary>Determines whether the console color will be reset after each operation.</summary>
    public bool ResetColorAfterOperation { get; set; }

    /// <summary>Gets or sets the global color set, representing both the output and the input color sets.</summary>
    public ConsoleColorSet GlobalColorSet
    {
        get
        {
            if (OutputColorSet != InputColorSet)
                throw new Exception("The current color sets are not equal.");
            return OutputColorSet;
        }
        set => (OutputColorSet, InputColorSet) = (value, value);
    }

    /// <summary>Initializes a new instance of the <seealso cref="ConsoleManager"/> class with <seealso cref="ConsoleColorSet.Default"/> being used for both input and output.</summary>
    public ConsoleManager() : this(ConsoleColorSet.Default) { }

    /// <summary>Initializes a new instance of the <seealso cref="ConsoleManager"/> class with <seealso cref="ConsoleColorSet.Default"/> being used for input and a custom set for output.</summary>
    /// <param name="outputForeground">The <seealso cref="ConsoleColor"/> that will be used for the foreground color during output.</param>
    /// <param name="outputBackground">The <seealso cref="ConsoleColor"/> that will be used for the background color during output.</param>
    /// <param name="resetColorAfterOperation">Determines whether the console color will be reset after each operation.</param>
    public ConsoleManager(ConsoleColor outputForeground, ConsoleColor outputBackground = ConsoleColor.Black, bool resetColorAfterOperation = false)
        : this(new ConsoleColorSet(outputForeground, outputBackground), resetColorAfterOperation) { }

    /// <summary>Initializes a new instance of the <seealso cref="ConsoleManager"/> class with <seealso cref="ConsoleColorSet.Default"/> being used for input and a <seealso cref="ConsoleColorSet"/> for output.</summary>
    /// <param name="outputColorSet">The <seealso cref="ConsoleColorSet"/> that will be used for the console color during output.</param>
    /// <param name="resetColorAfterOperation">Determines whether the console color will be reset after each operation.</param>
    public ConsoleManager(ConsoleColorSet outputColorSet, bool resetColorAfterOperation = false) : this(outputColorSet, ConsoleColorSet.Default, resetColorAfterOperation) { }

    /// <summary>Initializes a new instance of the <seealso cref="ConsoleManager"/> class with custom <seealso cref="ConsoleColorSet"/>s being used for input and output.</summary>
    /// <param name="outputColorSet">The <seealso cref="ConsoleColorSet"/> that will be used for the console color during output.</param>
    /// <param name="inputColorSet">The <seealso cref="ConsoleColorSet"/> that will be used for the console color during input.</param>
    /// <param name="resetColorAfterOperation">Determines whether the console color will be reset after each operation.</param>
    public ConsoleManager(ConsoleColorSet outputColorSet, ConsoleColorSet inputColorSet, bool resetColorAfterOperation = false)
    {
        OutputColorSet = outputColorSet;
        InputColorSet = inputColorSet;
        ResetColorAfterOperation = resetColorAfterOperation;
    }

    /// <summary>Writes the specified text using the current <seealso cref="ConsoleColorSet"/>.</summary>
    /// <param name="text">The text to write.</param>
    public void Write(string text)
    {
        ConsoleUtilities.WriteWithColor(text, OutputColorSet, ResetColorAfterOperation);
    }
    /// <summary>Writes the specified text using the current <seealso cref="ConsoleColorSet"/> and appends a line.</summary>
    /// <param name="text">The text to write.</param>
    public void WriteLine(string text)
    {
        ConsoleUtilities.WriteLineWithColor(text, OutputColorSet, ResetColorAfterOperation);
    }
    /// <summary>Writes the specified text using the current <seealso cref="ConsoleColorSet"/> and optionally appends a line.</summary>
    /// <param name="text">The text to write.</param>
    /// <param name="newlineAfterText">Determines whether a newline will be appended after the text.</param>
    public void WriteWithOptionalNewline(string text, bool newlineAfterText)
    {
        ConsoleUtilities.WriteWithColorAndOptionalNewline(text, OutputColorSet, ResetColorAfterOperation, newlineAfterText);
    }

    /// <summary>Reads a line from the console with the current <seealso cref="ConsoleColorSet"/>.</summary>
    /// <returns>The input string that was given.</returns>
    public string ReadLine()
    {
        return ConsoleUtilities.ReadLineWithColor(OutputColorSet, ResetColorAfterOperation);
    }
    /// <summary>Requests input from the console, providing a request message before doing so, with with the current <seealso cref="ConsoleColorSet"/>s for both input and output.</summary>
    /// <param name="requestMessage">The message to write before requesting input.</param>
    /// <param name="newlineAfterMessage">Determines whether a newline should be added after the message.</param>
    /// <returns>The input string that was given.</returns>
    public string RequestInputLine(string requestMessage, bool newlineAfterMessage = true)
    {
        return ConsoleUtilities.RequestInputLine(requestMessage, OutputColorSet, InputColorSet, ResetColorAfterOperation, newlineAfterMessage);
    }
}
