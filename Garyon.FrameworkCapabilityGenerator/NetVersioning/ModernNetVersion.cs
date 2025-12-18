namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal sealed record ModernNetVersion(FrameworkVersionNumber Version)
    : MainlineNetVersion(Version)
{
    public override MainlineNetVersionType MainlineType => MainlineNetVersionType.ModernNet;

    protected override string GetMonikerName()
    {
        return $"net{Version.Major}.{Version.Minor}";
    }
}
