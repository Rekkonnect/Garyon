using System;
using System.Collections.Specialized;
using System.Numerics;
using System.Text;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="BitVector32"/>.
/// </summary>
public static class BitVector32Extensions
{
    extension(BitVector32 vector)
    {
        public uint UInt => (uint)vector.Data;

        public bool Get(int index)
        {
            int mask = 1 << index;
            return vector[mask];
        }

        public BitVector32 And(BitVector32 other)
        {
            return new(vector.Data & other.Data);
        }
        public BitVector32 Or(BitVector32 other)
        {
            return new(vector.Data | other.Data);
        }
        public BitVector32 Xor(BitVector32 other)
        {
            return new(vector.Data ^ other.Data);
        }

#if HAS_BIT_OPERATIONS
        public int PopCount()
        {
            return BitOperations.PopCount(vector.UInt);
        }

        public int FirstBitIndex()
        {
            return BitOperations.TrailingZeroCount(vector.Data);
        }

        public int LastBitIndex()
        {
            return 31 - BitOperations.LeadingZeroCount((uint)vector.Data);
        }
#endif
    }

    public static void Set(this ref BitVector32 vector, int index, bool value)
    {
        int mask = 1 << index;
        vector[mask] = value;
    }
}
