using Garyon.FrameworkCapabilityGenerator;

Log("Generating FrameworkCapabilities.props file");

var outputPath = args[0];
Log($"Received output path '{outputPath}'");

var props = FrameworkCapabilityGeneratorHelpers.GenerateProps();
File.WriteAllText(outputPath, props);

Log("Finished generating FrameworkCapabilities.props file");

return;

static void Log(string message)
{
    Console.WriteLine($"[Garyon.FrameworkCapabilityGenerator] [{DateTimeOffset.Now}] {message}");
}
