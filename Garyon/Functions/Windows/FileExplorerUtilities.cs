using System.Diagnostics;

namespace Garyon.Functions.Windows
{
    /// <summary>Provides utility functions regarding the Windows file explorer.</summary>
    public static class FileExplorerUtilities
    {
        /// <summary>Opens the file explorer at the directory at which a specified file is contained, while also selecting it.</summary>
        /// <param name="path">The path of the file to open in the file explorer.</param>
        public static void OpenFileInExplorer(string path)
        {
            Process.Start("explorer.exe", $"/select,{path}");
        }
        // TODO: Unsure about whether multiple files can be selected while initiating the file explorer instance
    }
}
