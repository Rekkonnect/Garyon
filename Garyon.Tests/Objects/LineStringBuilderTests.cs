using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class LineStringBuilderTests
{
    [Test]
    public async Task AddModifyBuildAndClearTest()
    {
        var builder = new LineStringBuilder();
        IEnumerable<string> extraLines = ["delta", "epsilon"];

        builder.AddLine("alpha");
        builder.AddRepeatedLine("beta", 2);
        builder.AddLines("gamma");
        builder.AddLines(extraLines);
        builder[1, 0] = 'B';

        int lineCount = builder.LineCount;
        string text = builder.ToString();
        builder.Clear();

        await Assert.That(lineCount).IsEqualTo(6);
        await Assert.That(text).IsEqualTo(string.Join(Environment.NewLine, ["alpha", "Beta", "beta", "gamma", "delta", "epsilon"]));
        await Assert.That(builder.LineCount).IsEqualTo(0);
    }
}
