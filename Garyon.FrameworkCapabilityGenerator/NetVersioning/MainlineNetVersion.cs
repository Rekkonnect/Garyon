namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal abstract record MainlineNetVersion(FrameworkVersionNumber Version)
    : BaseFrameworkVersion(Version)
{
    public abstract MainlineNetVersionType MainlineType { get; }

    public override FrameworkVersionComparisonResult CompareTo(BaseFrameworkVersion other)
    {
        if (other is not MainlineNetVersion otherMainline)
            return FrameworkVersionComparisonResult.Indeterminate;

        var comparison = MainlineType.CompareTo(otherMainline.MainlineType);
        var comparisonResult = ComparisonResultFromInt(comparison);
        if (comparisonResult is FrameworkVersionComparisonResult.Same)
        {
            return CompareVersionNumberTo(other);
        }

        return comparisonResult;
    }
}
