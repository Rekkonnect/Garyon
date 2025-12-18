namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal sealed record NetFrameworkVersion(FrameworkVersionNumber Version)
    : MainlineNetVersion(Version)
{
    public override MainlineNetVersionType MainlineType => MainlineNetVersionType.NetFramework;

    protected override string GetMonikerName()
    {
        return $"net{Version.Major}{Version.Minor}{PatchOrEmpty(Version.Patch)}";
    }

    private static string PatchOrEmpty(int minor) => minor > 0 ? minor.ToString() : string.Empty;
}
