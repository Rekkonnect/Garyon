using System.Runtime.InteropServices;
using Garyon.Attributes;
using System;

namespace Garyon.QualityControl.SizedStructs
{
    /// <summary>A struct with a size of 20 bytes.</summary>
    [StructLayout(LayoutKind.Sequential, Size = elementCount)]
    [Autogenerated]
    public unsafe struct Struct20 : ISizedStruct<Struct20>
    {
        private const int elementCount = 20;

        private fixed byte elements[elementCount];

        public static bool operator ==(Struct20 left, Struct20 right) => left.Equals(right);
        public static bool operator !=(Struct20 left, Struct20 right) => !(left == right);
        public static Struct20 operator ~(Struct20 s)
        {
            var result = new Struct20();
            for (int i = 0; i < elementCount; i++)
                result.elements[i] = (byte)~s.elements[i];
            return result;
        }

        public bool Equals(Struct20 other)
        {
            for (int i = 0; i < elementCount; i++)
                if (elements[i] != other.elements[i])
                    return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            return Equals((Struct20)obj);
        }
        public override int GetHashCode()
        {
            var result = new HashCode();
            for (int i = 0; i < elementCount; i++)
                result.Add(elements[i]);
            return result.ToHashCode();
        }
    }
}
