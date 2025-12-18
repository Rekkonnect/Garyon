namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal sealed record NetStandardVersion(FrameworkVersionNumber Version)
    : BaseFrameworkVersion(Version)
{
    protected override string GetMonikerName()
    {
        return $"netstandard{Version.Major}.{Version.Minor}";
    }

    public override FrameworkVersionComparisonResult CompareTo(BaseFrameworkVersion other)
    {
        if (other is not NetStandardVersion otherNetStandard)
            return FrameworkVersionComparisonResult.Indeterminate;

        return CompareVersionNumberTo(otherNetStandard);
    }
}
