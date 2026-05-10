using Garyon.Objects.IO;
using System.IO;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class PathItemsTests
{
    [Test]
    public async Task FilePathExposesPathOnlyHelpersTest()
    {
        string directory = Path.Combine(Path.GetTempPath(), "garyon-path-tests");
        string path = Path.Combine(directory, "file.txt");

        FilePath file = new(path);

        await Assert.That(file.Path).IsEqualTo(path);
        await Assert.That(file.Directory.Path).IsEqualTo(directory);
        await Assert.That(file.FileName).IsEqualTo("file.txt");
        await Assert.That(file.ExtensionlessFileName).IsEqualTo("file");
        await Assert.That(file.Extension).IsEqualTo(".txt");

        await Assert.That(file.WithExtension(".json").Path).IsEqualTo(Path.ChangeExtension(path, ".json"));
        await Assert.That(file.WithFileName("x.bin").Path).IsEqualTo(Path.Combine(directory, "x.bin"));

        // Wrapper objects should be constructible even if the path does not exist.
        await Assert.That(file.FileInfo.FullName.EndsWith(Path.Combine("garyon-path-tests", "file.txt"))).IsTrue();
    }

    [Test]
    public async Task DirectoryPathCombinesAndFindsParentTest()
    {
        string parent = Path.Combine(Path.GetTempPath(), "garyon-path-tests-parent");
        string directory = Path.Combine(parent, "child");

        DirectoryPath dir = new(directory);

        await Assert.That(dir.Path).IsEqualTo(directory);
        await Assert.That(dir.File("file.txt").Path).IsEqualTo(Path.Combine(directory, "file.txt"));
        await Assert.That(dir.Parent.Path).IsEqualTo(new DirectoryInfo(directory).Parent!.FullName);

        // Wrapper objects should be constructible even if the path does not exist.
        await Assert.That(dir.DirectoryInfo.FullName.EndsWith(Path.Combine("garyon-path-tests-parent", "child"))).IsTrue();
    }
}
