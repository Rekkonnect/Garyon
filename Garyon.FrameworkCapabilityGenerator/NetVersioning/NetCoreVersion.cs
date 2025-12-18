namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal sealed record NetCoreVersion(FrameworkVersionNumber Version)
    : MainlineNetVersion(Version)
{
    public override MainlineNetVersionType MainlineType => MainlineNetVersionType.NetCore;

    protected override string GetMonikerName()
    {
        return $"netcoreapp{Version.Major}.{Version.Minor}";
    }
}
