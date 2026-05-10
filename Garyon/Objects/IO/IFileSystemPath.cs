using System.IO;

namespace Garyon.Objects.IO;

public interface IFileSystemPath
{
    string Path { get; }
    FileSystemInfo FileSystemInfo { get; }
}
