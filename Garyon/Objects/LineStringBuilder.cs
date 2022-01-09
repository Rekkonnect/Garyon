using Garyon.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Garyon.Objects
{
    /// <summary>Provides mechanisms and functions to building a multiline string.</summary>
    /// <remarks>Added strings are not checked for whether they're multiline or not, and are treated as a single line.</remarks>
    public class LineStringBuilder
    {
        private readonly List<StringBuilder> lines = new();

        /// <summary>Returns the line count of the string.</summary>
        public int LineCount => lines.Count;

        /// <summary>Adds a line to the string.</summary>
        /// <param name="line">The line to add to the string.</param>
        public void AddLine(string line)
        {
            lines.Add(new(line));
        }
        /// <summary>Adds the same line to the builder a specified number of times.</summary>
        /// <param name="line">The line to add to the builder.</param>
        /// <param name="times">The numer of times to add the line to the builder.</param>
        public void AddRepeatedLine(string line, int times)
        {
            for (int i = 0; i < times; i++)
                AddLine(line);
        }
        /// <summary>Adds a number of lines to the builder.</summary>
        /// <param name="lines">The lines to add to the builder.</param>
        public void AddLines(IEnumerable<string> lines)
        {
            foreach (var line in lines)
                AddLine(line);
        }
        /// <summary>Adds a number of lines to the builder.</summary>
        /// <param name="lines">The lines to add to the builder.</param>
        public void AddLines(params string[] lines)
        {
            foreach (var line in lines)
                AddLine(line);
        }

        /// <summary>Clears all the lines.</summary>
        public void Clear()
        {
            lines.Clear();
        }

        /// <summary>Builds the resulting string by concatenating all the lines and delimiting them with newline characters.</summary>
        /// <returns>The resulting string.</returns>
        public override string ToString()
        {
            return new StringBuilder().AppendLines(lines).RemoveLast().ToString();
        }

        /// <summary>Gets or sets the character at the specified line and column.</summary>
        /// <param name="line">The index of the line.</param>
        /// <param name="column">The index of the column.</param>
        /// <returns>The character at the respective position.</returns>
        public char this[int line, int column]
        {
            get => lines[line][column];
            set => lines[line][column] = value;
        }
    }
}