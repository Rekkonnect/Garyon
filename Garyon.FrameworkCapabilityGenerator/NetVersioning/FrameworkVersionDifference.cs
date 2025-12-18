namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal sealed class FrameworkVersionDifference(
    BaseFrameworkVersionSet superset,
    BaseFrameworkVersionSet subset)
    : BaseFrameworkVersionSet
{
    public override bool Contains(BaseFrameworkVersion version)
    {
        return !subset.Contains(version)
            && superset.Contains(version);
    }
}
