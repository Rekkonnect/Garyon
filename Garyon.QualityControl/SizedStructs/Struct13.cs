using System;
using System.Runtime.InteropServices;
using Garyon.Attributes;

namespace Garyon.QualityControl.SizedStructs;

/// <summary>A struct with a size of 13 bytes.</summary>
[StructLayout(LayoutKind.Sequential, Size = elementCount)]
[Autogenerated]
public unsafe struct Struct13 : ISizedStruct<Struct13>
{
    private const int elementCount = 13;

    private fixed byte elements[elementCount];

    public static bool operator ==(Struct13 left, Struct13 right) => left.Equals(right);
    public static bool operator !=(Struct13 left, Struct13 right) => !(left == right);
    public static Struct13 operator ~(Struct13 s)
    {
        var result = new Struct13();
        for (int i = 0; i < elementCount; i++)
            result.elements[i] = (byte)~s.elements[i];
        return result;
    }

    public bool Equals(Struct13 other)
    {
        for (int i = 0; i < elementCount; i++)
            if (elements[i] != other.elements[i])
                return false;
        return true;
    }
    public override bool Equals(object obj)
    {
        return Equals((Struct13)obj);
    }
    public override int GetHashCode()
    {
        var result = new HashCode();
        for (int i = 0; i < elementCount; i++)
            result.Add(elements[i]);
        return result.ToHashCode();
    }
}
