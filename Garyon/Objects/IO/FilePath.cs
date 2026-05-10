using System.IO;
using SystemPath = System.IO.Path;

namespace Garyon.Objects.IO;

/// <summary>
/// Represents a file path wrapper that provides lightweight, non-I/O path
/// manipulation helpers.
/// </summary>
/// <param name="Path">
/// The file path. This type does not validate existence or interact with the file
/// system; it only stores and manipulates the path string.
/// </param>
public readonly record struct FilePath(string Path) : IFileSystemPath
{
    /// <summary>
    /// Gets the directory portion of this file path.
    /// </summary>
    public DirectoryPath Directory => new(SystemPath.GetDirectoryName(Path) ?? string.Empty);

    /// <summary>
    /// Gets the file name portion of this file path.
    /// </summary>
    public string FileName => SystemPath.GetFileName(Path);
    /// <summary>
    /// Gets the file name portion of this file path without an extension.
    /// </summary>
    public string ExtensionlessFileName => SystemPath.GetFileNameWithoutExtension(Path);
    /// <summary>
    /// Gets the extension portion of this file path, including the leading dot
    /// when present.
    /// </summary>
    public string Extension => SystemPath.GetExtension(Path);

    /// <summary>
    /// Gets a <see cref="System.IO.FileInfo"/> wrapper for this path.
    /// </summary>
    /// <remarks>
    /// Creating a <see cref="System.IO.FileInfo"/> does not access the file system.
    /// Accessing certain members of the returned instance may.
    /// </remarks>
    public FileInfo FileInfo => new(Path);
    FileSystemInfo IFileSystemPath.FileSystemInfo => FileInfo;

    /// <summary>
    /// Initializes a new instance of <see cref="FilePath"/> from a
    /// <see cref="System.IO.FileInfo"/> instance.
    /// </summary>
    /// <param name="fileInfo">
    /// The <see cref="System.IO.FileInfo"/> whose <see cref="FileSystemInfo.FullName"/>
    /// will be stored.
    /// </param>
    public FilePath(FileInfo fileInfo)
        : this(fileInfo.FullName) { }

    /// <summary>
    /// Returns a new <see cref="FilePath"/> with its extension changed.
    /// </summary>
    /// <param name="newExtension">
    /// The new extension to apply. The leading dot is optional.
    /// </param>
    public FilePath WithExtension(string newExtension)
    {
        return SystemPath.ChangeExtension(Path, newExtension);
    }

    /// <summary>
    /// Returns a new <see cref="FilePath"/> with its file name changed, preserving
    /// the directory portion of this path.
    /// </summary>
    /// <param name="newFileName">
    /// The new file name to use.
    /// </param>
    public FilePath WithFileName(string newFileName)
    {
        return Directory.File(newFileName);
    }

    /// <summary>
    /// Converts a path string to a <see cref="FilePath"/> instance.
    /// </summary>
    public static implicit operator FilePath(string path) => new(path);
    /// <summary>
    /// Converts a <see cref="System.IO.FileInfo"/> instance to a
    /// <see cref="FilePath"/> instance.
    /// </summary>
    public static implicit operator FilePath(FileInfo path) => new(path);
}
