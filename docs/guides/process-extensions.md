# Process Extensions

Garyon provides extensions for working with `System.Diagnostics.Process`, making process management more convenient.

> [!VERSION]
> **C# 14 required:** this guide uses C# 14 extension members (`extension(Process)`).

## Overview

The process extensions provide utilities for:
- Awaiting process initialization
- Process state management
- Integration with other Garyon utilities

## Extensions

### AwaitProcessInitialized

Wait for a process to initialize its main window:

```csharp
using Garyon.Extensions;
using System.Diagnostics;

var process = Process.Start("notepad.exe");
process.AwaitProcessInitialized();
// Process main window is now ready
```

#### How It Works

The `AwaitProcessInitialized` method:
1. Checks if the process has already exited
2. If not, calls `WaitForInputIdle()` to wait for the process to reach an idle state
3. Returns when the process is ready to accept input

This is useful when you need to interact with a process's UI or ensure it's fully started before proceeding.

## Usage Examples

### Starting and Waiting for Application

```csharp
using Garyon.Extensions;
using System.Diagnostics;

public class ApplicationLauncher
{
    public void LaunchAndInitialize(string applicationPath)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = applicationPath,
                UseShellExecute = false,
                CreateNoWindow = false
            }
        };

        process.Start();

        // Wait for the application to be ready
        process.AwaitProcessInitialized();

        Console.WriteLine($"Process {process.ProcessName} is now initialized");
    }
}
```

### Batch Process Management

```csharp
using Garyon.Extensions;
using System.Diagnostics;
using System.Threading.Tasks;

public class BatchProcessor
{
    public void StartMultipleProcesses(params string[] applications)
    {
        var processes = new List<Process>();

        // Start all processes
        foreach (var app in applications)
        {
            var process = Process.Start(app);
            processes.Add(process);
        }

        // Wait for all to initialize
        foreach (var process in processes)
        {
            process.AwaitProcessInitialized();
        }

        Console.WriteLine($"All {processes.Count} processes initialized");
    }

    public async Task StartAndMonitorAsync(string application)
    {
        var process = Process.Start(application);

        // Initialize
        process.AwaitProcessInitialized();

        // Monitor
        await process.WaitForExitAsync();

        Console.WriteLine($"Process exited with code: {process.ExitCode}");
    }
}
```

## Best Practices

1. **Check for Exit**: The extension handles exited processes gracefully, but be aware of the process state
2. **UI Applications Only**: `WaitForInputIdle()` is primarily for GUI applications; console apps may not benefit
3. **Timeout Handling**: Consider implementing timeouts for processes that may hang during initialization

### Timeout Example

```csharp
using Garyon.Extensions;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public async Task<bool> TryAwaitInitializationAsync(
    Process process, 
    TimeSpan timeout)
{
    using var cts = new CancellationTokenSource(timeout);

    try
    {
        await Task.Run(() => process.AwaitProcessInitialized(), cts.Token);
        return true;
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Process initialization timed out");
        return false;
    }
}
```

## Error Handling

```csharp
using Garyon.Extensions;
using System.Diagnostics;

public class SafeProcessLauncher
{
    public bool TryLaunchAndInitialize(string path)
    {
        Process process = null;
        try
        {
            process = Process.Start(path);
            if (process == null)
            {
                Console.WriteLine("Failed to start process");
                return false;
            }

            process.AwaitProcessInitialized();
            Console.WriteLine("Process initialized successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        finally
        {
            // Clean up if needed
            // Note: Don't dispose if you need to keep the process running
        }
    }
}
```

## Platform Considerations

- **Windows**: `WaitForInputIdle()` works well with GUI applications
- **Linux/macOS**: Behavior may vary; test on target platforms
- **Console Applications**: May not have an "idle" state in the traditional sense

## API Reference

See the [ProcessExtensions API Reference](../api/Garyon.Extensions.ProcessExtensions.yml) for complete documentation.

## Related

- [Task Handling](task-handling.md) - For async patterns

## Additional Process Utilities

For more advanced process scenarios, consider also:
- `Garyon.Functions.ProcessUtilities` - Process helpers
- Standard `System.Diagnostics.Process` features
