using System.IO;

namespace Garyon.Extensions;

/// <summary>
/// Contains extensions for the <seealso cref="FileSystemInfo"/> class.
/// </summary>
public static class FileSystemInfoExtensions
{
    /// <summary>
    /// Gets the parent directory containing this file system object,
    /// either the containing directory of the file, or the parent
    /// directory of the given directory.
    /// </summary>
    /// <param name="info">The file system object whose parent to get.</param>
    /// <returns>
    /// The <seealso cref="DirectoryInfo"/> instance of the parent of the
    /// given file system object.
    /// </returns>
    public static DirectoryInfo? Parent(this FileSystemInfo info)
    {
        return info switch
        {
            DirectoryInfo directory => directory.Parent,
            FileInfo file => file.Directory,
        };
    }

    public static DirectoryInfo? RecursiveParent(this FileSystemInfo info, int levels)
    {
        return info switch
        {
            DirectoryInfo directory => directory.RecursiveParent(levels),
            FileInfo file => file.RecursiveParent(levels),
        };
    }
    public static DirectoryInfo? BoundRecursiveParent(this FileSystemInfo info, int levels)
    {
        return info switch
        {
            DirectoryInfo directory => directory.BoundRecursiveParent(levels),
            FileInfo file => file.BoundRecursiveParent(levels),
        };
    }

    /// <summary>
    /// Gets the depth of this file system object, meaning the number of
    /// directories between the root and this object.
    /// </summary>
    /// <param name="info">The file system object whose depth to get.</param>
    /// <returns>
    /// The number of parent directories that must be traversed before
    /// reaching the file system root.
    /// </returns>
    public static int Depth(this FileSystemInfo info)
    {
        return info switch
        {
            DirectoryInfo dir => dir.Depth(),
            FileInfo file => file.Depth(),
        };
    }
}
