using System.Collections.Generic;
using System.Text;
using static System.IO.File;

namespace Garyon.Functions;

/// <summary>Provides functions related to reading or writing files.</summary>
public static class FileHandling
{
    /// <summary>Writes all lines without the final newline at the end of the file.</summary>
    /// <param name="path">The path of the file to write the lines to.</param>
    /// <param name="lines">The lines to write to the file.</param>
    public static void WriteAllLinesWithoutFinalNewLine(string path, IEnumerable<string> lines)
    {
        var result = new StringBuilder();
        foreach (var l in lines)
            result.AppendLine(l);
        result.Remove(result.Length - 1, 1);
        WriteAllText(path, result.ToString());
    }
    /// <summary>Writes all lines without the final newline at the end of the file.</summary>
    /// <param name="path">The path of the file to write the lines to.</param>
    /// <param name="lines">The lines to write to the file.</param>
    public static void WriteAllLinesWithoutFinalNewLine(string path, IEnumerable<StringBuilder> lines)
    {
        var result = new StringBuilder();
        foreach (var l in lines)
            result.AppendLine(l.ToString());
        result.Remove(result.Length - 1, 1);
        WriteAllText(path, result.ToString());
    }
}