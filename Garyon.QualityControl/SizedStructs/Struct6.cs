using System.Runtime.InteropServices;
using Garyon.Attributes;
using System;

namespace Garyon.QualityControl.SizedStructs;

/// <summary>A struct with a size of 6 bytes.</summary>
[StructLayout(LayoutKind.Sequential, Size = elementCount)]
[Autogenerated]
public unsafe struct Struct6 : ISizedStruct<Struct6>
{
    private const int elementCount = 6;

    private fixed byte elements[elementCount];

    public static bool operator ==(Struct6 left, Struct6 right) => left.Equals(right);
    public static bool operator !=(Struct6 left, Struct6 right) => !(left == right);
    public static Struct6 operator ~(Struct6 s)
    {
        var result = new Struct6();
        for (int i = 0; i < elementCount; i++)
            result.elements[i] = (byte)~s.elements[i];
        return result;
    }

    public bool Equals(Struct6 other)
    {
        for (int i = 0; i < elementCount; i++)
            if (elements[i] != other.elements[i])
                return false;
        return true;
    }
    public override bool Equals(object obj)
    {
        return Equals((Struct6)obj);
    }
    public override int GetHashCode()
    {
        var result = new HashCode();
        for (int i = 0; i < elementCount; i++)
            result.Add(elements[i]);
        return result.ToHashCode();
    }
}
