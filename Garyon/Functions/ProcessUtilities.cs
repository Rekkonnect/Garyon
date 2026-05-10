using Garyon.Functions.Windows;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Garyon.Functions;

public static class ProcessUtilities
{
    // --- https://stackoverflow.com/a/43232486
    // |
    //  -> https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
    //     "Fun times in this cross-platform world."
    public static Process OpenUrl(string url)
    {
        try
        {
            return Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                var startInfo = new ProcessStartInfo(url) { UseShellExecute = true };
                return Process.Start(startInfo)!;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }

    public static Process ShowFileInFileViewer(string filePath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return FileExplorerUtilities.SelectFileInExplorer(filePath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return Process.Start("open", $"-R \"{filePath}\"");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Process.Start("xdg-open", filePath);
        }
        else
        {
            throw new NotSupportedException("Operating system not supported");
        }
    }

    public static Process ShowDirectoryInFileViewer(string directoryPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return FileExplorerUtilities.StartExplorerAtRoot(directoryPath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return Process.Start("open", directoryPath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Process.Start("xdg-open", directoryPath);
        }
        else
        {
            throw new NotSupportedException("Operating system not supported");
        }
    }
}
