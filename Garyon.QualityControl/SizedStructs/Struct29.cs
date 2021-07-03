using System.Runtime.InteropServices;
using Garyon.Attributes;
using System;

namespace Garyon.QualityControl.SizedStructs
{
    /// <summary>A struct with a size of 29 bytes.</summary>
    [StructLayout(LayoutKind.Sequential, Size = elementCount)]
    [Autogenerated]
    public unsafe struct Struct29 : ISizedStruct<Struct29>
    {
        private const int elementCount = 29;

        private fixed byte elements[elementCount];

        public static bool operator ==(Struct29 left, Struct29 right) => left.Equals(right);
        public static bool operator !=(Struct29 left, Struct29 right) => !(left == right);
        public static Struct29 operator ~(Struct29 s)
        {
            var result = new Struct29();
            for (int i = 0; i < elementCount; i++)
                result.elements[i] = (byte)~s.elements[i];
            return result;
        }

        public bool Equals(Struct29 other)
        {
            for (int i = 0; i < elementCount; i++)
                if (elements[i] != other.elements[i])
                    return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            return Equals((Struct29)obj);
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
