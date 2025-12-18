using System.Numerics;

namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal readonly record struct FrameworkVersionNumber(int Major, int Minor, int Patch)
    : IComparisonOperators<FrameworkVersionNumber, FrameworkVersionNumber, bool>,
        IComparable<FrameworkVersionNumber>
{
    public FrameworkVersionNumber(int major, int minor)
        : this(major, minor, 0) { }

    public int CompareTo(FrameworkVersionNumber other)
    {
        return Compare(this, other);
    }

    public static bool operator <(FrameworkVersionNumber left, FrameworkVersionNumber right)
    {
        return Compare(left, right) < 0;
    }
    public static bool operator <=(FrameworkVersionNumber left, FrameworkVersionNumber right)
    {
        return Compare(left, right) <= 0;
    }

    public static bool operator >(FrameworkVersionNumber left, FrameworkVersionNumber right)
    {
        return Compare(left, right) > 0;
    }
    public static bool operator >=(FrameworkVersionNumber left, FrameworkVersionNumber right)
    {
        return Compare(left, right) >= 0;
    }

    private static int Compare(FrameworkVersionNumber left, FrameworkVersionNumber right)
    {
        if (left.Major != right.Major)
            return left.Major.CompareTo(right.Major);
        if (left.Minor != right.Minor)
            return left.Minor.CompareTo(right.Minor);
        return left.Patch.CompareTo(right.Patch);
    }

    public override string ToString()
    {
        return $"{Major}.{Minor}.{Patch}";
    }
}

