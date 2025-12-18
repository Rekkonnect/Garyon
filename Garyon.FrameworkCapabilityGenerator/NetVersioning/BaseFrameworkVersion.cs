namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal abstract record BaseFrameworkVersion(FrameworkVersionNumber Version)
{
    public string MonikerName => field ??= GetMonikerName();

    protected abstract string GetMonikerName();

    public abstract FrameworkVersionComparisonResult CompareTo(
        BaseFrameworkVersion other);

    protected FrameworkVersionComparisonResult CompareVersionNumberTo(
        BaseFrameworkVersion other)
    {
        return CompareVersionNumbers(Version, other.Version);
    }

    protected static FrameworkVersionComparisonResult CompareVersionNumbers(
        FrameworkVersionNumber thisVersion, FrameworkVersionNumber otherVersion)
    {
        var comparison = thisVersion.CompareTo(otherVersion);

        return ComparisonResultFromInt(comparison);
    }
    protected static FrameworkVersionComparisonResult ComparisonResultFromInt(
        int comparison)
    {
        return comparison switch
        {
            0 => FrameworkVersionComparisonResult.Same,
            < 0 => FrameworkVersionComparisonResult.Earlier,
            > 0 => FrameworkVersionComparisonResult.Later,
        };
    }
}
