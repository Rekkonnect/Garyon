using Garyon.FrameworkCapabilityGenerator.NetVersioning;

namespace Garyon.FrameworkCapabilityGenerator.FrameworkCapabilities;

internal sealed record FrameworkCapabilityRange(
    BaseFrameworkVersionSet VersionRange,
    IReadOnlyList<string> Capabilities);
