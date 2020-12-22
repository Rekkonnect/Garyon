using System.Runtime.InteropServices;
using Garyon.Attributes;

namespace Garyon.QualityControl.SizedStructs
{
    /// <summary>A struct with a size of 22 bytes.</summary>
    [StructLayout(LayoutKind.Sequential, Size = elementCount)]
    [Autogenerated]
    public unsafe struct Struct22 : ISizedStruct<Struct22>
    {
        private const int elementCount = 22;

        private fixed byte elements[elementCount];

        public static bool operator ==(Struct22 left, Struct22 right) => left.Equals(right);
        public static bool operator !=(Struct22 left, Struct22 right) => !(left == right);
        public static Struct22 operator ~(Struct22 s)
        {
            var result = new Struct22();
            for (int i = 0; i < elementCount; i++)
                result.elements[i] = (byte)~s.elements[i];
            return result;
        }

        public bool Equals(Struct22 other)
        {
            for (int i = 0; i < elementCount; i++)
                if (elements[i] != other.elements[i])
                    return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            return Equals((Struct22)obj);
        }
    }
}