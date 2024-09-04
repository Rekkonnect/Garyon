using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.IO.Path;

namespace Garyon.Functions;

/// <summary>Provides useful functions for path handling.</summary>
public static class PathUtilities
{
    /// <summary>Normalizes the path by making the directory separators consistent depending on the platform and returns the replaced string.</summary>
    /// <param name="path">The path to normalize.</param>
    /// <returns>The normalized path.</returns>
    public static string NormalizePath(string path) => path.Replace('/', DirectorySeparatorChar).Replace('\\', DirectorySeparatorChar);
    /// <summary>Normalizes the directory path by ensuring there is a directory separator character at the end of the string.</summary>
    /// <param name="dirPath">The directory path to normalize.</param>
    /// <returns>The normalized directory path.</returns>
    public static string NormalizeDirectoryPath(string dirPath)
    {
        if (dirPath.Length == 0)
            return dirPath;
        var result = NormalizePath(dirPath);
        if (!result.EndsWith(DirectorySeparatorChar)) 
            result += DirectorySeparatorChar;
        return result;
    }

    /// <summary>Analyzes the provided path by splitting the individual item names by the directory separator character and returns the array of the names that form the path. The path is automatically converted to its appropriate platform-specific form.</summary>
    /// <param name="path">The path to analyze.</param>
    public static string[] AnalyzePath(string path)
    {
        return NormalizePath(path)
            .Split(DirectorySeparatorChar)
            .NonEmpty()
            .ToArray();
    }

    /// <summary>Returns a concatenated string version of the provided directory collection including the directory separator character.</summary>
    /// <param name="directories">The directories to concatenate.</param>
    public static string ConcatenateDirectoryPath(IEnumerable<string> directories)
    {
        return directories.Combine(DirectorySeparatorChar) + DirectorySeparatorChar;
    }

    /// <summary>Returns a concatenated string version of the provided directory collection including the directory separator character.</summary>
    /// <param name="directories">The directories to concatenate.</param>
    public static string ConcatenateDirectoryPath(params string[] directories) => ConcatenateDirectoryPath((IEnumerable<string>)directories);

    /// <summary>Gets the deepest common directory path between two paths.</summary>
    /// <param name="pathA">The first path to get the deepest common directory of.</param>
    /// <param name="pathB">The second path to get the deepest common directory of.</param>
    public static string GetCommonDirectory(string pathA, string pathB)
    {
        var splitA = AnalyzePath(pathA);
        var splitB = AnalyzePath(pathB);

        int min = Math.Min(splitA.Length, splitB.Length);
        var result = new List<string>(min);

        for (int i = 0; i < min; i++)
        {
            if (splitA[i] != splitB[i])
                break;

            result.Add(splitA[i]);
        }

        return ConcatenateDirectoryPath(result);
    }
    // Since the explanation is not the most graspable, examples are provided.
    /// <summary>Gets the directory name of the directory in the previous path whose parent is the new path.</summary>
    /// <param name="previousPath">The previous path.</param>
    /// <param name="newPath">The new path in which the resulting previous path's directory name is contained.</param>
    /// <returns>
    /// The name of the directory one level deeper than the new path if the previous path is a subpath of the new path, otherwise <see langword="null"/>.<br/><br/>
    /// Examples:
    /// <list type="bullet">
    /// <item><c>GetPreviousPathDirectoryInNewPath("C:\users\user\Desktop\", "C:\users\")</c> returns "user".</item>
    /// <item><c>GetPreviousPathDirectoryInNewPath("C:\X\Y\Z\", "C:\")</c> returns "X".</item>
    /// <item><c>GetPreviousPathDirectoryInNewPath("C:\K\L\M\N\O\P\", "C:\K\L\M\N\")</c> returns "O".</item>
    /// </list>
    /// </returns>
    public static string GetPreviousPathDirectoryInNewPath(string previousPath, string newPath)
    {
        var splitPrevious = AnalyzePath(previousPath);
        var splitNew = AnalyzePath(newPath);
        if (splitNew.Length >= splitPrevious.Length)
            return null;

        int index = -1;
        while (++index < splitNew.Length)
            if (splitPrevious[index] != splitNew[index])
                break;
        return splitPrevious[index];
    }
    /// <summary>Returns the name of the item, without taking its parent directory into consideration.</summary>
    /// <param name="path">The path to take its individual item name.</param>
    public static string GetIndividualItemName(string path) => AnalyzePath(path).Last();

    /// <summary>Returns the path item type of the provided path.</summary>
    /// <param name="path">The path to determine its path item type.</param>
    public static PathItemType DeterminePathItemType(string path)
    {
        if (EndsWithVolumeSeparator(path) ||
            EndsWithVolumeSeparator(path.Substring(0, path.Length - 1)))
        {
            return PathItemType.Volume;
        }
        if (EndsWithDirectorySeparator(path) ||
            string.IsNullOrWhiteSpace(GetExtension(path)))
        {
            return PathItemType.Directory;
        }
        return PathItemType.File;
    }

    /// <summary>Determines whether a path ends with the platform-specific directory separator character.</summary>
    /// <param name="path">The path to determine whether it ends with the platform-specific directory separator.</param>
    public static bool EndsWithDirectorySeparator(string path) => path.EndsWith(DirectorySeparatorChar);
    /// <summary>Determines whether a path ends with the platform-specific volume separator character.</summary>
    /// <param name="path">The path to determine whether it ends with the platform-specific volume separator.</param>
    public static bool EndsWithVolumeSeparator(string path) => path.EndsWith(VolumeSeparatorChar);
}
