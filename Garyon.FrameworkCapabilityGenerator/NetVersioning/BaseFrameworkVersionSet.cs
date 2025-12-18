namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal abstract class BaseFrameworkVersionSet
{
    public abstract bool Contains(BaseFrameworkVersion version);

    public IReadOnlyList<BaseFrameworkVersion> Reduce(
        IReadOnlyList<BaseFrameworkVersion> sourceVersions)
    {
        var result = new List<BaseFrameworkVersion>();
        foreach (var version in sourceVersions)
        {
            if (Contains(version))
            {
                result.Add(version);
            }
        }
        return result;
    }

    public static FrameworkVersionDifference operator -(
        BaseFrameworkVersionSet left,
        BaseFrameworkVersionSet right)
    {
        return new FrameworkVersionDifference(left, right);
    }

    public static FrameworkVersionUnion operator |(
        BaseFrameworkVersionSet left,
        BaseFrameworkVersionSet right)
    {
        return new FrameworkVersionUnion(left, right);
    }
}
