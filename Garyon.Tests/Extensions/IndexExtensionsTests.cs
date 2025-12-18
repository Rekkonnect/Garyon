using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class IndexExtensionsTests
{
    [Test]
    public async Task InvertTest()
    {
        var ar = new[] { 0, 1, 2, 3, 4, 3, 2, 1, 0 };
        for (int i = 0; i < ar.Length; i++)
        {
            await Assert.That(ar[((Index)i).Invert()]).IsEqualTo(ar[i]);
        }
    }
}