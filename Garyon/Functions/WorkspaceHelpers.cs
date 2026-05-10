using Garyon.Extensions;
using System;
using System.IO;

namespace Garyon.Functions;

/// <summary>
/// Provides helper functions for standard workspace operations, involving solutions,
/// project roots, git repositores, and other common workspaces.
/// </summary>
public static class WorkspaceHelpers
{
    public static DirectoryInfo? GetThisSolutionRoot()
    {
        var currentDirectory = DirectoryInfo.GetCurrentDirectory();
        return GetSolutionRoot(currentDirectory);
    }

    public static DirectoryInfo? GetSolutionRoot(DirectoryInfo directory)
    {
        return directory.GetAncestorOrSelfDirectoryWithFiles("*.sln*");
    }

    public static DirectoryInfo? GetGitRepositoryRoot(DirectoryInfo directory)
    {
        return directory.GetAncestorOrSelfDirectoryWithDirectories(".git");
    }
}
