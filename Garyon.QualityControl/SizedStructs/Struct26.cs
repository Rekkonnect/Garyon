using System.Runtime.InteropServices;
using Garyon.Attributes;

namespace Garyon.QualityControl.SizedStructs
{
    /// <summary>A struct with a size of 26 bytes.</summary>
    [StructLayout(LayoutKind.Sequential, Size = elementCount)]
    [Autogenerated]
    public unsafe struct Struct26 : ISizedStruct<Struct26>
    {
        private const int elementCount = 26;

        private fixed byte elements[elementCount];

        public static bool operator ==(Struct26 left, Struct26 right) => left.Equals(right);
        public static bool operator !=(Struct26 left, Struct26 right) => !(left == right);
        public static Struct26 operator ~(Struct26 s)
        {
            var result = new Struct26();
            for (int i = 0; i < elementCount; i++)
                result.elements[i] = (byte)~s.elements[i];
            return result;
        }

        public bool Equals(Struct26 other)
        {
            for (int i = 0; i < elementCount; i++)
                if (elements[i] != other.elements[i])
                    return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            return Equals((Struct26)obj);
        }
    }
}