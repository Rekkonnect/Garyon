using System.Runtime.Intrinsics.X86;

namespace Garyon.Functions.IntrinsicsHelpers
{
    /// <summary>Provides helper functions for the LZCNT CPU instruction set. Every function checks whether the LZCNT CPU instruction set is supported, and if it's not, the functions do nothing.</summary>
    public class LZCNTHelper
    {
        public static uint LeadingZeroCount(byte value)
        {
            if (Lzcnt.IsSupported)
                return Lzcnt.LeadingZeroCount((uint)value << 24);
            return default;
        }
        public static uint LeadingZeroCount(ushort value)
        {
            if (Lzcnt.IsSupported)
                return Lzcnt.LeadingZeroCount((uint)value << 16);
            return default;
        }
        public static uint LeadingZeroCount(sbyte value)
        {
            if (Lzcnt.IsSupported)
                return Lzcnt.LeadingZeroCount((uint)value << 24);
            return default;
        }
        public static uint LeadingZeroCount(short value)
        {
            if (Lzcnt.IsSupported)
                return Lzcnt.LeadingZeroCount((uint)value << 16);
            return default;
        }
        public static uint LeadingZeroCount(int value)
        {
            if (Lzcnt.IsSupported)
                return Lzcnt.LeadingZeroCount((uint)value);
            return default;
        }

        public abstract class X64
        {
            public static ulong LeadingZeroCount(long value)
            {
                if (Lzcnt.IsSupported)
                    return Lzcnt.X64.LeadingZeroCount((ulong)value);
                return default;
            }
        }
    }
}
