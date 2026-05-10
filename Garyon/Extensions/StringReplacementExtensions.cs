using Polyfills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garyon.Extensions;

public static class StringReplacementExtensions
{
    extension(string source)
    {
        public string Replace(IEnumerable<StringReplacement> replacements)
        {
            var sortedReplacements = replacements.OrderBy(s => s.Start).ToList();
            var result = new StringBuilder();

            var lastWrittenPosition = 0;
            foreach (var replacement in sortedReplacements)
            {
                ConsumeSource(replacement.Start);
                result.Append(replacement.NewText);
                lastWrittenPosition = replacement.SourceEnd;
            }

            ConsumeSource(source.Length);

            return result.ToString();

            void ConsumeSource(int end)
            {
                var writtenSpan = source.AsSpan()[lastWrittenPosition..end];
                result.Append(writtenSpan);
                lastWrittenPosition = end;
            }
        }
    }
}
