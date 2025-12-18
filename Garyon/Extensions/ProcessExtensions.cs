using System.Diagnostics;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="Process"/>.
/// </summary>
public static class ProcessExtensions
{
    extension(Process process)
    {
        public void AwaitProcessInitialized()
        {
            if (process.HasExited)
            {
                return;
            }

            process.WaitForInputIdle();
        }
    }
}
