using System.Runtime.InteropServices;
using Garyon.Attributes;
using System;

namespace Garyon.QualityControl.SizedStructs;

/// <summary>A struct with a size of 5 bytes.</summary>
[StructLayout(LayoutKind.Sequential, Size = elementCount)]
[Autogenerated]
public unsafe struct Struct5 : ISizedStruct<Struct5>
{
    private const int elementCount = 5;

    private fixed byte elements[elementCount];

    public static bool operator ==(Struct5 left, Struct5 right) => left.Equals(right);
    public static bool operator !=(Struct5 left, Struct5 right) => !(left == right);
    public static Struct5 operator ~(Struct5 s)
    {
        var result = new Struct5();
        for (int i = 0; i < elementCount; i++)
            result.elements[i] = (byte)~s.elements[i];
        return result;
    }

    public bool Equals(Struct5 other)
    {
        for (int i = 0; i < elementCount; i++)
            if (elements[i] != other.elements[i])
                return false;
        return true;
    }
    public override bool Equals(object obj)
    {
        return Equals((Struct5)obj);
    }
    public override int GetHashCode()
    {
        var result = new HashCode();
        for (int i = 0; i < elementCount; i++)
            result.Add(elements[i]);
        return result.ToHashCode();
    }
}
