using Garyon.FrameworkCapabilityGenerator.NetVersioning;

namespace Garyon.FrameworkCapabilityGenerator.FrameworkCapabilities;

internal sealed record FrameworkCapability(string Capability, FrameworkVersionRange Range);
