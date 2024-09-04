using Garyon.Functions;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static System.IO.Path;

namespace Garyon.Tests.Functions;

public class PathUtilitiesTests
{
    [Test]
    public void GetCommonDirectoryTest()
    {
        AssertBidirectional(
            ["C:", "users", "user"],
            ["C:", "users", "user", "Desktop"],
            ["C:", "users", "user"]
            );

        AssertBidirectional(
            ["C:", "users"],
            ["C:", "users", "Rekkon"],
            ["C:", "users", "user", "Desktop"]
            );

        AssertBidirectional(
            ["C:", "users"],
            ["C:", "users", "user"],
            ["C:", "users", "user0", "Desktop"]
            );

        AssertBidirectional(
            ["C:", "users"],
            ["C:", "users", "user", "Desktop"],
            ["C:", "users", "user0", "Desktop"]
            );

        AssertBidirectional(
            ["C:", "users"],
            ["C:", "users", "A", "B", "C"],
            ["C:", "users", "B", "B", "C"]
            );

        static void AssertBidirectional(string[] expected, string[] left, string[] right)
        {
            Assert(expected, left, right);
            Assert(expected, right, left);
        }

        static void Assert(string[] expected, string[] left, string[] right)
        {
            var expectedConcatenated = PathUtilities.ConcatenateDirectoryPath(expected);
            var actualConcatenated = PathUtilities.GetCommonDirectory(Combine(left), Combine(right));
            AreEqual(expectedConcatenated, actualConcatenated);
        }
    }

    [Test]
    public void DeterminePathItemTypeTest()
    {
        Assert(
            PathItemType.Directory,
            $"{Combine("C:", "users", "user", "Desktop")}{DirectorySeparatorChar}");

        Assert(
            PathItemType.Volume,
            $"C{VolumeSeparatorChar}");

        AssertCombined(
            PathItemType.File,
            ["C:", "users", "user", "Desktop", "Some file.txt"]);

        static void Assert(PathItemType expected, string path)
        {
            AreEqual(expected, PathUtilities.DeterminePathItemType(path));
        }
        static void AssertCombined(PathItemType expected, string[] path)
        {
            var pathString = Combine(path);
            Assert(expected, pathString);
        }
    }

    [Test]
    public void ConcatenateDirectoryPathTest()
    {
        AreEqual($"{Combine("C:", "users", "user")}{DirectorySeparatorChar}", PathUtilities.ConcatenateDirectoryPath("C:", "users", "user"));
    }

    [Test]
    public void GetPreviousPathDirectoryInNewPathTest()
    {
        AreEqual("Desktop", PathUtilities.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users", "user")));
        AreEqual("user", PathUtilities.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users")));
        AreEqual(null, PathUtilities.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user"), Combine("C:", "users", "user", "Desktop")));
    }

    [Test]
    public void NormalizeDirectoryPathTest()
    {
        AreEqual(PathUtilities.ConcatenateDirectoryPath("C:", "users", "user"), PathUtilities.NormalizeDirectoryPath(@"C:\users/user/"));
        AreEqual(PathUtilities.ConcatenateDirectoryPath("C:", "users", "user"), PathUtilities.NormalizeDirectoryPath(@"C:\users/user"));
        AreEqual("", PathUtilities.NormalizeDirectoryPath(""));
    }

    [Test]
    public void GetIndividualItemNameTest()
    {
        Assert("user", @"C:\users/user/");
        Assert("file.txt", @"C:/users/user/file.txt");
        Assert("C:", "C:");

        static void Assert(string expected, string path)
        {
            AreEqual(expected, PathUtilities.GetIndividualItemName(path));
        }
    }
}
