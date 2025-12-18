namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal sealed class FrameworkVersionUnion(
    params IReadOnlyList<BaseFrameworkVersionSet> ranges)
    : BaseFrameworkVersionSet
{
    public override bool Contains(BaseFrameworkVersion version)
    {
        return ranges.Any(s => s.Contains(version));
    }
}
