using System.Diagnostics;
using System.IO;

#if HAS_SUPPORTED_OS_PLATFORM
using System.Runtime.Versioning;
#endif

namespace Garyon.Functions.Windows;

/// <summary>Provides utility functions regarding the Windows file explorer.</summary>
#if HAS_SUPPORTED_OS_PLATFORM
[SupportedOSPlatform("windows")]
#endif
public static class FileExplorerUtilities
{
    /// <summary>Opens the file explorer at the directory at which a specified file is contained, while also selecting it.</summary>
    /// <param name="path">The path of the file to open in the file explorer.</param>
    public static void SelectFileInExplorer(string path)
    {
        Process.Start("explorer.exe", $"/select,{path}");
    }

    /// <summary>Opens the file explorer at the directory that is specified.</summary>
    /// <param name="root">The root folder to open the file explorer at.</param>
    public static void StartExplorerAtRoot(string root)
    {
        Process.Start("explorer.exe", $"/root,{root}");
    }

    /// <summary>Opens the file explorer at the default directory.</summary>
    public static void OpenNewDefaultExplorerProcess()
    {
        Process.Start("explorer.exe", $"/n");
    }

    /// <summary>Opens the default set file explorer at the directory that is specified.</summary>
    /// <param name="root">The root folder to open the file explorer at.</param>
    /// <remarks>
    /// This method uses the start command to open the file explorer. This is used
    /// by some third-party file explorers to open their custom explorer instead of
    /// the Windows one, for example File Pilot.
    /// </remarks>
    public static void StartCustomExplorerAtRoot(string root)
    {
        Process.Start("start", Path.NormalizePath(root));
    }
}
