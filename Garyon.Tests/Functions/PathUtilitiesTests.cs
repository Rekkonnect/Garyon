using Garyon.Functions;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static System.IO.Path;

namespace Garyon.Tests.Functions
{
    public class PathUtilitiesTests
    {
        [Test]
        public void GetCommonDirectoryTest()
        {
            AreEqual(PathUtilities.ConcatenateDirectoryPath("C:", "users", "user"), PathUtilities.GetCommonDirectory(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users", "user")));
            AreEqual(PathUtilities.ConcatenateDirectoryPath("C:", "users", "user"), PathUtilities.GetCommonDirectory(Combine("C:", "users", "user"), Combine("C:", "users", "user", "Desktop")));
            AreEqual(PathUtilities.ConcatenateDirectoryPath("C:", "users"), PathUtilities.GetCommonDirectory(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users", "Rekkon")));
            AreEqual(PathUtilities.ConcatenateDirectoryPath("C:", "users"), PathUtilities.GetCommonDirectory(Combine("C:", "users", "user"), Combine("C:", "users", "user0", "Desktop")));
        }

        [Test]
        public void DeterminePathItemTypeTest()
        {
            AreEqual(PathItemType.Directory, PathUtilities.DeterminePathItemType($"{Combine("C:", "users", "user", "Desktop")}{DirectorySeparatorChar}"));
            AreEqual(PathItemType.Volume, PathUtilities.DeterminePathItemType($"C{VolumeSeparatorChar}"));
            AreEqual(PathItemType.File, PathUtilities.DeterminePathItemType(Combine("C:", "users", "user", "Desktop", "Some file.txt")));
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
            AreEqual("user", PathUtilities.GetIndividualItemName(@"C:\users/user/"));
            AreEqual("file.txt", PathUtilities.GetIndividualItemName(@"C:/users/user/file.txt"));
            AreEqual("C:", PathUtilities.GetIndividualItemName("C:"));
        }
    }
}
