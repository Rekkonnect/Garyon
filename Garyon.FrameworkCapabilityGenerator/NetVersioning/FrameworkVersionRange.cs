namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal sealed class FrameworkVersionRange : BaseFrameworkVersionSet
{
    public static readonly FrameworkVersionRange All = new(null, null);

    public BaseFrameworkVersion? Lower { get; }
    public BaseFrameworkVersion? Upper { get; }

    private FrameworkVersionRange(BaseFrameworkVersion? lower, BaseFrameworkVersion? upper)
    {
        Lower = lower;
        Upper = upper;
    }

    public override bool Contains(BaseFrameworkVersion version)
    {
        if (Lower is not null)
        {
            var lowerComparison = Lower.CompareTo(version);
            if (lowerComparison
                is FrameworkVersionComparisonResult.Later
                or FrameworkVersionComparisonResult.Indeterminate)
            {
                return false;
            }
        }

        if (Upper is not null)
        {
            var upperComparison = Upper.CompareTo(version);
            if (upperComparison
                is FrameworkVersionComparisonResult.Earlier
                or FrameworkVersionComparisonResult.Indeterminate)
            {
                return false;
            }
        }

        return true;
    }

    public static FrameworkVersionRange Create(
        BaseFrameworkVersion? lower,
        BaseFrameworkVersion? upper)
    {
        if (lower is null && upper is null)
        {
            return All;
        }

        if (lower is not null && upper is not null)
        {
            var comparison = lower.CompareTo(upper);
            if (comparison is FrameworkVersionComparisonResult.Later)
            {
                throw new ArgumentException("The lower bound cannot be later than the upper bound.");
            }
            if (comparison is FrameworkVersionComparisonResult.Indeterminate)
            {
                throw new ArgumentException("The specified bounds must be comparable with each other.");
            }
        }

        return new FrameworkVersionRange(lower, upper);
    }
}
