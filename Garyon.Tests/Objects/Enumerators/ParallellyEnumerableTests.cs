using Garyon.Objects.Enumerators;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects.Enumerators;

public class ParallellyEnumerableTests
{
    [Test]
    public async Task Enumerate2Test()
    {
        int[] ints = [2, 4, 7, 8, 12];
        string[] strings = ["a", "c", "f"];

        (int, string)[] expectedIntString =
        [
            (2, "a"),
            (4, "c"),
            (7, "f"),
            (8, default),
            (12, default),
        ];
        (string, int)[] expectedStringInt = expectedIntString.Select(a => (a.Item2, a.Item1)).ToArray();

        await Assert.That((ints, strings).AsParallellyEnumerable()).IsEquivalentTo(expectedIntString);
        await Assert.That((strings, ints).AsParallellyEnumerable()).IsEquivalentTo(expectedStringInt);
    }

    [Test]
    public async Task Enumerate3Test()
    {
        int[] ints = [2, 4, 7, 8, 12];
        string[] strings = ["a", "c", "f"];
        double[] doubles = [4.3, 3.1, -0.3, 7];

        (int, string, double)[] expectedIntStringDouble =
        [
            (2, "a", 4.3),
            (4, "c", 3.1),
            (7, "f", -0.3),
            (8, default, 7),
            (12, default, default),
        ];
        (int, double, string)[] expectedIntDoubleString = expectedIntStringDouble.Select(a => (a.Item1, a.Item3, a.Item2)).ToArray();
        (string, int, double)[] expectedStringIntDouble = expectedIntStringDouble.Select(a => (a.Item2, a.Item1, a.Item3)).ToArray();
        (string, double, int)[] expectedStringDoubleInt = expectedIntStringDouble.Select(a => (a.Item2, a.Item3, a.Item1)).ToArray();
        (double, int, string)[] expectedDoubleIntString = expectedIntStringDouble.Select(a => (a.Item3, a.Item1, a.Item2)).ToArray();
        (double, string, int)[] expectedDoubleStringInt = expectedIntStringDouble.Select(a => (a.Item3, a.Item2, a.Item1)).ToArray();

        await Assert.That((ints, strings, doubles).AsParallellyEnumerable()).IsEquivalentTo(expectedIntStringDouble);
        await Assert.That((ints, doubles, strings).AsParallellyEnumerable()).IsEquivalentTo(expectedIntDoubleString);
        await Assert.That((strings, ints, doubles).AsParallellyEnumerable()).IsEquivalentTo(expectedStringIntDouble);
        await Assert.That((strings, doubles, ints).AsParallellyEnumerable()).IsEquivalentTo(expectedStringDoubleInt);
        await Assert.That((doubles, ints, strings).AsParallellyEnumerable()).IsEquivalentTo(expectedDoubleIntString);
        await Assert.That((doubles, strings, ints).AsParallellyEnumerable()).IsEquivalentTo(expectedDoubleStringInt);
    }
    [Test]
    public async Task Enumerate4Test()
    {
        int[] ints = [2, 4, 7, 8, 12];
        string[] strings = ["a", "c", "f"];
        double[] doubles = [4.3, 3.1, -0.3, 7];
        bool[] bools = [true, true, false];

        (int, string, double, bool)[] expectedIntStringDoubleBool =
        [
            (2, "a", 4.3, true),
            (4, "c", 3.1, true),
            (7, "f", -0.3, false),
            (8, default, 7, default),
            (12, default, default, default),
        ];

        await Assert.That((ints, strings, doubles, bools).AsParallellyEnumerable()).IsEquivalentTo(expectedIntStringDoubleBool);
    }
}