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

    /// <summary>
    /// Gets the ancestor directory of this directory, at a specified number of
    /// levels up in the directory tree. If the current directory's depth is less
    /// than the provided number of levels to walk up, <see langword="null"/> is
    /// returned.
    /// </summary>
    /// <param name="directory">The directory to walk up from.</param>
    /// <param name="levels">
    /// The number of levels to walk up the directory tree.
    /// 0 returns the current directory.
    /// </param>
    /// <returns>
    /// The ancestor directory at the given level, or <see langword="null"/> if
    /// the given directory's depth was less than the given number of levels to
    /// walk up.
    /// </returns>
    public static DirectoryInfo? RecursiveParent(this DirectoryInfo directory, int levels = 1)
    {
        var current = directory;
        for (int i = 0; i < levels; i++)
            current = current?.Parent;
        return current;
    }

    /// <summary>
    /// Gets the ancestor directory of this directory, at a specified number of
    /// levels up in the directory tree. If the current directory's depth is less
    /// than the provided number of levels to walk up, the returned directory is
    /// the root directory.
    /// </summary>
    /// <param name="directory">The directory to walk up from.</param>
    /// <param name="levels">
    /// The number of levels to walk up the directory tree.
    /// 0 returns the current directory.
    /// </param>
    /// <returns>
    /// The ancestor directory at the given level, or the root directory.
    /// </returns>
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

    /// <summary>
    /// Moves the given subdirectory a number of levels up, based on
    /// <see cref="BoundRecursiveParent(DirectoryInfo, int)"/>.
    /// </summary>
    /// <param name="directory">The directory to move up.</param>
    /// <param name="levels">
    /// The number of levels to walk up the directory tree.
    /// 0 returns the current directory.
    /// </param>
    /// <returns>
    /// The result of <see cref="BoundRecursiveParent(DirectoryInfo, int)"/>.
    /// </returns>
    public static DirectoryInfo MoveUpBound(this DirectoryInfo directory, int levels = 1)
    {
        var parent = directory.BoundRecursiveParent(levels);
        directory.MoveTo(parent);
        return parent;
    }

    /// <summary>
    /// Moves the given subdirectory a number of levels up, based on
    /// <see cref="RecursiveParent(DirectoryInfo, int)"/>. If the returned
    /// directory from that method is <see langword="null"/>, an exception
    /// is thrown.
    /// </summary>
    /// <param name="directory">The directory to move up.</param>
    /// <param name="levels">
    /// The number of levels to walk up the directory tree.
    /// 0 returns the current directory.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <see cref="RecursiveParent(DirectoryInfo, int)"/> returns
    /// <see langword="null"/>.
    /// </exception>
    /// <returns>
    /// The result of <see cref="BoundRecursiveParent(DirectoryInfo, int)"/>.
    /// </returns>
    public static DirectoryInfo MoveUp(this DirectoryInfo directory, int levels = 1)
    {
        var parent = directory.RecursiveParent(levels)
            ?? throw new InvalidOperationException("The requested directory's depth is less than the number of levels to move it up by.");
        directory.MoveTo(parent);
        return parent;
    }

    /// <summary>
    /// Moves a directory to the given destination directory.
    /// </summary>
    /// <param name="directory">The directory to move.</param>
    /// <param name="destination">The target directory to move the source directory to.</param>
    public static void MoveTo(this DirectoryInfo directory, DirectoryInfo destination)
    {
        directory.MoveTo(destination.FullName);
    }

    /// <summary>
    /// Gets the total size of the files contained in this directory,
    /// traversing subdirectories and including their total sizes.
    /// </summary>
    /// <param name="directory">The directory whose total file size to get.</param>
    /// <returns>
    /// The total size of the files contained in this and nested subdirectories,
    /// in bytes as returned for each file by <see cref="FileInfo.Length"/>
    /// and via <see cref="TotalSize(DirectoryInfo)"/> for each directory.
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
    /// The total size of the files directly contained in this directory only,
    /// in bytes as returned for each file by <see cref="FileInfo.Length"/>.
    /// </returns>
    public static long TotalSize(this DirectoryInfo directory)
    {
        long size = 0;

        foreach (var file in directory.GetFiles())
            size += file.Length;

        return size;
    }

    /// <summary>
    /// Gets the single subdirectory contained in the given directory.
    /// If the directory does not contain exactly one subdirectory,
    /// <paramref name="singleDirectory"/> will be <see langword="null"/>.
    /// </summary>
    /// <param name="directoryInfo"></param>
    /// <param name="singleDirectory">
    /// The single subdirectory contained in the given directory, or
    /// <see langword="null"/> if the directory did not contain exactly one
    /// subdirectory.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the directory contains a single subdirectory,
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryGetSingleSubdirectory(
        this DirectoryInfo directoryInfo, out DirectoryInfo? singleDirectory)
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
    /// directories. This method will return F, which is the last
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

    /// <summary>
    /// Attempts to delete the given directory, catching any thrown exception
    /// from the operation and returning whether the operation succeeded.
    /// </summary>
    /// <param name="directory">The directory to delete.</param>
    /// <param name="recursive">
    /// <see langword="true"/> to delete all the contents of the directory
    /// and subdirectories; <see langword="false"/> to delete only the directory
    /// itself if it is empty, following <see cref="DirectoryInfo.Delete(bool)"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the operation succeeded, otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryDelete(this DirectoryInfo directory, bool recursive)
    {
        try
        {
            directory.Delete(recursive);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
