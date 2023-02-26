using System;
using System.IO;

namespace Garyon.Extensions;

/// <summary>
/// Contains extensions for the <seealso cref="DirectoryInfo"/> class.
/// </summary>
public static class DirectoryInfoExtensions
{
    /// <summary>
    /// Gets the depth of this directory, meaning the number of directories
    /// between the root and this directory.
    /// </summary>
    /// <param name="directory">The directory whose depth to get.</param>
    /// <returns>
    /// The number of parent directories that must be traversed before
    /// reaching the file system root.
    /// </returns>
    public static int Depth(this DirectoryInfo directory)
    {
        var current = directory;
        int depth = 0;
        while (true)
        {
            current = current.Parent;

            if (current is null)
                return depth;

            depth++;
        }
    }

    public static DirectoryInfo? RecursiveParent(this DirectoryInfo directory, int levels = 1)
    {
        var current = directory;
        for (int i = 0; i < levels; i++)
            current = current?.Parent;
        return current;
    }
    public static DirectoryInfo BoundRecursiveParent(this DirectoryInfo directory, int levels = 1)
    {
        var current = directory;
        for (int i = 0; i < levels; i++)
        {
            var parent = current.Parent;
            if (parent is null)
                return current;
            current = parent;
        }
        return current;
    }

    public static DirectoryInfo MoveUpBound(this DirectoryInfo directory, int levels = 1)
    {
        var parent = directory.BoundRecursiveParent(levels);
        directory.MoveTo(parent);
        return directory;
    }
    public static DirectoryInfo MoveUp(this DirectoryInfo directory, int levels = 1)
    {
        var parent = directory.RecursiveParent(levels)
            ?? throw new InvalidOperationException("The requested directory's depth is less than the number of levels to move it up by.");
        directory.MoveTo(parent);
        return directory;
    }

    private static void MoveTo(this DirectoryInfo directory, DirectoryInfo parent)
    {
        directory.MoveTo(parent.FullName);
    }

    /// <summary>
    /// Gets the total size of the files contained in this directory,
    /// traversing subdirectories and including their total sizes.
    /// </summary>
    /// <param name="directory">The directory whose total file size to get.</param>
    /// <returns>
    /// The total size of the files contained in this and nested subdirectories.
    /// </returns>
    public static long TotalSizeDeep(this DirectoryInfo directory)
    {
        long size = directory.TotalSize();

        // You never know
        checked
        {
            foreach (var childDirectory in directory.GetDirectories())
                size += TotalSizeDeep(childDirectory);
        }

        return size;
    }
    /// <summary>
    /// Gets the total size of the files contained in this directory,
    /// only accounting for the directly contained files, and not ones
    /// in subdirectories.
    /// </summary>
    /// <param name="directory">The directory that contains the files.</param>
    /// <returns>
    /// The total size of the files directly contained in this directory only.
    /// </returns>
    public static long TotalSize(this DirectoryInfo directory)
    {
        long size = 0;

        foreach (var file in directory.GetFiles())
            size += file.Length;

        return size;
    }

    public static bool TryGetSingleSubdirectory(this DirectoryInfo directoryInfo, out DirectoryInfo? singleDirectory)
    {
        return directoryInfo.GetFileSystemInfos().TryGetSingle(out singleDirectory);
    }
    /// <summary>
    /// Traverses the single subdirectory recursively and finds the deepest
    /// subdirectory that does not contain only a single subdirectory. The
    /// resulting subdirectory will contain more one file system objects,
    /// or a single file, or nothing.
    /// </summary>
    /// <param name="directoryInfo">
    /// The directory whose deepest single directory to get. It does not
    /// matter how many other directories are contained in the parent
    /// directory of the given directory.
    /// For instance, the given directory is D, and P is the parent directory
    /// of D. Whether P only contains a single directory, i.e. D, does not
    /// prevent the exploration of the subdirectories of D.
    /// </param>
    /// <returns>
    /// The deepest directory that does not contain a single directory, starting
    /// from the given directory.
    /// For example, assume the starting directory is D, D only contains the
    /// directory E, E only contains the directory F and F contains two other
    /// directories and 3 files. This method will return F, which is the last
    /// directory that was the only option to explore into.
    /// </returns>
    public static DirectoryInfo ExploreDeepestSingleDirectory(this DirectoryInfo directoryInfo)
    {
        var current = directoryInfo;

        while (true)
        {
            bool containedSingle = TryGetSingleSubdirectory(current, out var single);
            if (!containedSingle)
                return current;

            current = single;
        }
    }
}
