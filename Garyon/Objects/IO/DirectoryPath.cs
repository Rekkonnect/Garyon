using System.IO;
using SystemPath = System.IO.Path;

namespace Garyon.Objects.IO;

/// <summary>
/// Represents a directory path wrapper that provides lightweight, non-I/O path
/// manipulation helpers.
/// </summary>
/// <param name="Path">
/// The directory path. This type does not validate existence or interact with the
/// file system; it only stores and manipulates the path string.
/// </param>
public readonly record struct DirectoryPath(string Path) : IFileSystemPath
{
    /// <summary>
    /// Gets a <see cref="System.IO.DirectoryInfo"/> wrapper for this path.
    /// </summary>
    /// <remarks>
    /// Creating a <see cref="System.IO.DirectoryInfo"/> does not access the file
    /// system. Accessing certain members of the returned instance may.
    /// </remarks>
    public DirectoryInfo DirectoryInfo => new(Path);
    FileSystemInfo IFileSystemPath.FileSystemInfo => DirectoryInfo;

    /// <summary>
    /// Gets the parent directory of this directory path, or <see langword="default"/>
    /// if the path has no parent.
    /// </summary>
    public DirectoryPath Parent => DirectoryInfo.Parent is { } parent
        ? new(parent)
        : default;

    /// <summary>
    /// Initializes a new instance of <see cref="DirectoryPath"/> from a
    /// <see cref="System.IO.DirectoryInfo"/> instance.
    /// </summary>
    /// <param name="directoryInfo">
    /// The <see cref="System.IO.DirectoryInfo"/> whose <see cref="FileSystemInfo.FullName"/>
    /// will be stored.
    /// </param>
    public DirectoryPath(DirectoryInfo directoryInfo)
        : this(directoryInfo.FullName) { }

    /// <summary>
    /// Returns a <see cref="FilePath"/> by combining this directory path with a
    /// file name.
    /// </summary>
    /// <param name="fileName">
    /// The file name to combine with <see cref="Path"/>.
    /// </param>
    public FilePath File(string fileName)
    {
        return SystemPath.Combine(Path, fileName);
    }

    /// <summary>
    /// Converts a path string to a <see cref="DirectoryPath"/> instance.
    /// </summary>
    public static implicit operator DirectoryPath(string path) => new(path);
    /// <summary>
    /// Converts a <see cref="System.IO.DirectoryInfo"/> instance to a
    /// <see cref="DirectoryPath"/> instance.
    /// </summary>
    public static implicit operator DirectoryPath(DirectoryInfo path) => new(path);
}
