using System.Diagnostics;

namespace Garyon.Functions.Windows;

/// <summary>Provides utility functions regarding the Windows file explorer.</summary>
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
}
