using Garyon.Functions;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using static System.IO.Path;

namespace Garyon.Tests.Functions;

public class PathUtilitiesTests
{
    [Test]
    public async Task GetCommonDirectoryTest()
    {
        await AssertBidirectional(
            ["C:", "users", "user"],
            ["C:", "users", "user", "Desktop"],
            ["C:", "users", "user"]
            );

        await AssertBidirectional(
            ["C:", "users"],
            ["C:", "users", "Rekkon"],
            ["C:", "users", "user", "Desktop"]
            );

        await AssertBidirectional(
            ["C:", "users"],
            ["C:", "users", "user"],
            ["C:", "users", "user0", "Desktop"]
            );

        await AssertBidirectional(
            ["C:", "users"],
            ["C:", "users", "user", "Desktop"],
            ["C:", "users", "user0", "Desktop"]
            );

        await AssertBidirectional(
            ["C:", "users"],
            ["C:", "users", "A", "B", "C"],
            ["C:", "users", "B", "B", "C"]
            );

        static async Task AssertBidirectional(string[] expected, string[] left, string[] right)
        {
            await AssertPaths(expected, left, right);
            await AssertPaths(expected, right, left);
        }

        static async Task AssertPaths(string[] expected, string[] left, string[] right)
        {
            var expectedConcatenated = PathUtilities.ConcatenateDirectoryPath(expected);
            var actualConcatenated = PathUtilities.GetCommonDirectory(Combine(left), Combine(right));
            await Assert.That(actualConcatenated).IsEqualTo(expectedConcatenated);
        }
    }

    [Test]
    public async Task DeterminePathItemTypeTest()
    {
        await AssertPaths(
            PathItemType.Directory,
            $"{Combine("C:", "users", "user", "Desktop")}{DirectorySeparatorChar}");

        await AssertPaths(
            PathItemType.Volume,
            $"C{VolumeSeparatorChar}");

        await AssertCombined(
            PathItemType.File,
            ["C:", "users", "user", "Desktop", "Some file.txt"]);

        static async Task AssertPaths(PathItemType expected, string path)
        {
            await Assert.That(PathUtilities.DeterminePathItemType(path)).IsEqualTo(expected);
        }
        static async Task AssertCombined(PathItemType expected, string[] path)
        {
            var pathString = Combine(path);
            await AssertPaths(expected, pathString);
        }
    }

    [Test]
    public async Task ConcatenateDirectoryPathTest()
    {
        await Assert
            .That(PathUtilities.ConcatenateDirectoryPath("C:", "users", "user"))
            .IsEqualTo($"{Combine("C:", "users", "user")}{DirectorySeparatorChar}");
    }

    [Test]
    public async Task GetPreviousPathDirectoryInNewPathTest()
    {
        await Assert.That(PathUtilities.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users", "user"))).IsEqualTo("Desktop");
        await Assert.That(PathUtilities.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users"))).IsEqualTo("user");
        await Assert.That(PathUtilities.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user"), Combine("C:", "users", "user", "Desktop"))).IsNull();
    }

    [Test]
    public async Task NormalizeDirectoryPathTest()
    {
        await Assert.That(PathUtilities.NormalizeDirectoryPath(@"C:\users/user/")).IsEqualTo(PathUtilities.ConcatenateDirectoryPath("C:", "users", "user"));
        await Assert.That(PathUtilities.NormalizeDirectoryPath(@"C:\users/user")).IsEqualTo(PathUtilities.ConcatenateDirectoryPath("C:", "users", "user"));
        await Assert.That(PathUtilities.NormalizeDirectoryPath("")).IsEmpty();
    }

    [Test]
    public async Task GetIndividualItemNameTest()
    {
        await AssertPaths("user", @"C:\users/user/");
        await AssertPaths("file.txt", @"C:/users/user/file.txt");
        await AssertPaths("C:", "C:");

        static async Task AssertPaths(string expected, string path)
        {
            await Assert.That(PathUtilities.GetIndividualItemName(path)).IsEqualTo(expected);
        }
    }
}