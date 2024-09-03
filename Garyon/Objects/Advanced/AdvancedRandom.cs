using System;
#if HAS_INTRINSICS
using System.Numerics;
#endif

namespace Garyon.Objects.Advanced;

/// <summary>Provides a better alternative for the <seealso cref="Random"/> class, offering further functionality.</summary>
public class AdvancedRandom : Random
{
    /// <summary>Generates a random <seealso cref="byte"/> within the range [0, <seealso cref="byte.MaxValue"/>].</summary>
    /// <returns>The resulting <seealso cref="byte"/>.</returns>
    public virtual byte NextByte() => (byte)Sample(byte.MaxValue + 1);
    /// <summary>Generates a random <seealso cref="byte"/> within the range [0, <paramref name="maxValue"/>).</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="byte"/>.</returns>
    public virtual byte NextByte(byte maxValue) => (byte)Sample(maxValue);
    /// <summary>Generates a random <seealso cref="byte"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="byte"/>.</returns>
    public virtual byte NextByte(byte minValue, byte maxValue) => (byte)Sample(minValue, maxValue);

    /// <summary>Generates a random <seealso cref="short"/> within the range [0, <seealso cref="short.MaxValue"/>].</summary>
    /// <returns>The resulting <seealso cref="short"/>.</returns>
    public virtual short NextInt16() => (short)Sample(short.MaxValue + 1);
    /// <summary>Generates a random <seealso cref="short"/> within the range [0, <paramref name="maxValue"/>).</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="short"/>.</returns>
    public virtual short NextInt16(short maxValue) => (short)Sample(maxValue);
    /// <summary>Generates a random <seealso cref="short"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="short"/>.</returns>
    public virtual short NextInt16(short minValue, short maxValue) => (short)Sample(minValue, maxValue);

#if !HAS_RANDOM_NEXTINT64_NEXTSINGLE
    /// <summary>Generates a random <seealso cref="long"/> within the range [0, <seealso cref="long.MaxValue"/>]. May use up to 2 number generations due to <seealso cref="double"/>'s lesser precision.</summary>
    /// <returns>The resulting <seealso cref="long"/>.</returns>
    public virtual long NextInt64() => ExtendSample((long)Sample(long.MaxValue));
    /// <summary>Generates a random <seealso cref="long"/> within the range [0, <paramref name="maxValue"/>). May use up to 2 number generations due to <seealso cref="double"/>'s lesser precision.</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="long"/>.</returns>
    public virtual long NextInt64(long maxValue) => ExtendSample((long)Sample(maxValue));
    /// <summary>Generates a random <seealso cref="long"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>). May use up to 2 number generations due to <seealso cref="double"/>'s lesser precision.</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="long"/>.</returns>
    public virtual long NextInt64(long minValue, long maxValue) => ExtendSample((long)Sample(minValue, maxValue));
#endif

    /// <summary>Generates a random <seealso cref="sbyte"/> within the range [0, <seealso cref="sbyte.MaxValue"/>].</summary>
    /// <returns>The resulting <seealso cref="sbyte"/>.</returns>
    public virtual sbyte NextSByte() => (sbyte)Sample(sbyte.MaxValue + 1);
    /// <summary>Generates a random <seealso cref="sbyte"/> within the range [0, <paramref name="maxValue"/>).</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="sbyte"/>.</returns>
    public virtual sbyte NextSByte(sbyte maxValue) => (sbyte)Sample(maxValue);
    /// <summary>Generates a random <seealso cref="sbyte"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="sbyte"/>.</returns>
    public virtual sbyte NextSByte(sbyte minValue, sbyte maxValue) => (sbyte)Sample(minValue, maxValue);

    /// <summary>Generates a random <seealso cref="ushort"/> within the range [0, <seealso cref="ushort.MaxValue"/>].</summary>
    /// <returns>The resulting <seealso cref="ushort"/>.</returns>
    public virtual ushort NextUInt16() => (ushort)Sample(ushort.MaxValue + 1);
    /// <summary>Generates a random <seealso cref="ushort"/> within the range [0, <paramref name="maxValue"/>).</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="ushort"/>.</returns>
    public virtual ushort NextUInt16(ushort maxValue) => (ushort)Sample(maxValue);
    /// <summary>Generates a random <seealso cref="ushort"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="ushort"/>.</returns>
    public virtual ushort NextUInt16(ushort minValue, ushort maxValue) => (ushort)Sample(minValue, maxValue);

    /// <summary>Generates a random <seealso cref="uint"/> within the range [0, <seealso cref="uint.MaxValue"/>].</summary>
    /// <returns>The resulting <seealso cref="uint"/>.</returns>
    public virtual uint NextUInt32() => (uint)Sample(uint.MaxValue + 1UL);
    /// <summary>Generates a random <seealso cref="uint"/> within the range [0, <paramref name="maxValue"/>).</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="uint"/>.</returns>
    public virtual uint NextUInt32(uint maxValue) => (uint)Sample(maxValue);
    /// <summary>Generates a random <seealso cref="uint"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="uint"/>.</returns>
    public virtual uint NextUInt32(uint minValue, uint maxValue) => (uint)Sample(minValue, maxValue);

    // TODO: Ensure that ulong.MaxValue will be returned from this method
    /// <summary>Generates a random <seealso cref="ulong"/> within the range [0, <seealso cref="ulong.MaxValue"/>]. May use up to 2 number generations due to <seealso cref="double"/>'s lesser precision.</summary>
    /// <returns>The resulting <seealso cref="ulong"/>.</returns>
    public virtual ulong NextUInt64() => ExtendSample((ulong)Sample(ulong.MaxValue));
    /// <summary>Generates a random <seealso cref="ulong"/> within the range [0, <paramref name="maxValue"/>). May use up to 2 number generations due to <seealso cref="double"/>'s lesser precision.</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="ulong"/>.</returns>
    public virtual ulong NextUInt64(ulong maxValue) => ExtendSample((ulong)Sample(maxValue));
    /// <summary>Generates a random <seealso cref="ulong"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>). May use up to 2 number generations due to <seealso cref="double"/>'s lesser precision.</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="ulong"/>.</returns>
    public virtual ulong NextUInt64(ulong minValue, ulong maxValue) => ExtendSample((ulong)Sample(minValue, maxValue));

#if !HAS_RANDOM_NEXTINT64_NEXTSINGLE
    /// <summary>Generates a random <seealso cref="float"/> within the range [0, 1).</summary>
    /// <returns>The resulting <seealso cref="float"/>.</returns>
    public virtual float NextSingle() => (float)Sample();
#endif
    /// <summary>Generates a random <seealso cref="float"/> within the range [0, <paramref name="maxValue"/>).</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="float"/>.</returns>
    public virtual float NextSingle(float maxValue) => (float)Sample(maxValue);
    /// <summary>Generates a random <seealso cref="float"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="float"/>.</returns>
    public virtual float NextSingle(float minValue, float maxValue) => (float)Sample(minValue, maxValue);

    /// <summary>Generates a random <seealso cref="double"/> within the range [0, <paramref name="maxValue"/>).</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="double"/>.</returns>
    public virtual double NextDouble(double maxValue) => Sample(maxValue);
    /// <summary>Generates a random <seealso cref="double"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="double"/>.</returns>
    public virtual double NextDouble(double minValue, double maxValue) => Sample(minValue, maxValue);

    /// <summary>Generates a random <seealso cref="char"/> within the range [0, <seealso cref="char.MaxValue"/>].</summary>
    /// <returns>The resulting <seealso cref="char"/>.</returns>
    public virtual char NextChar() => (char)Sample(char.MaxValue + 1);
    /// <summary>Generates a random <seealso cref="char"/> within the range [0, <paramref name="maxValue"/>).</summary>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than 0.</param>
    /// <returns>The resulting <seealso cref="char"/>.</returns>
    public virtual char NextChar(char maxValue) => (char)Sample(maxValue);
    /// <summary>Generates a random <seealso cref="char"/> within the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than <paramref name="minValue"/>.</param>
    /// <returns>The resulting <seealso cref="char"/>.</returns>
    public virtual char NextChar(char minValue, char maxValue) => (char)Sample(minValue, maxValue);

    /// <summary>Generates a random <seealso cref="bool"/>.</summary>
    /// <returns>The resulting <seealso cref="bool"/>.</returns>
    public virtual bool NextBoolean() => Sample() >= 0.5;

    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using a time-dependent default seed value.</summary>
    public AdvancedRandom() : base() { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
    public AdvancedRandom(byte seed) : base(seed) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
    public AdvancedRandom(sbyte seed) : base(seed) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
    public AdvancedRandom(short seed) : base(seed) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
    public AdvancedRandom(ushort seed) : base(seed) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
    public AdvancedRandom(int seed) : base(seed) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
    public AdvancedRandom(uint seed) : base((int)seed) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number, whose hash code will be used to calculate a starting value for the pseudo-random number sequence.</param>
    public AdvancedRandom(long seed) : base(seed.GetHashCode()) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number, whose hash code will be used to calculate a starting value for the pseudo-random number sequence.</param>
    public AdvancedRandom(ulong seed) : base(seed.GetHashCode()) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A floating-point number, whose bit reinterpretation as an <seealso cref="int"/> will be used to calculate a starting value for the pseudo-random number sequence.</param>
    public unsafe AdvancedRandom(float seed) : base(*(int*)&seed) { }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedRandom"/> class, using the specified seed value.</summary>
    /// <param name="seed">A number, whose hash code will be used to calculate a starting value for the pseudo-random number sequence.</param>
    public AdvancedRandom(double seed) : base(seed.GetHashCode()) { }

    // TODO: Verify when this function is needed
    protected virtual long ExtendSample(long result)
    {
        const int maxLeadingZeroes = 64 - 52;

        int leadingZeroes =
#if HAS_INTRINSICS
            BitOperations.LeadingZeroCount((ulong)result);
#else
            maxLeadingZeroes;
#endif

        if (leadingZeroes > maxLeadingZeroes)
            result += Next(Math.Min((int)(long.MaxValue - result), 1 << leadingZeroes));
        return result;
    }
    protected virtual ulong ExtendSample(ulong result)
    {
        const int maxLeadingZeroes = 64 - 52;

        int leadingZeroes =
#if HAS_INTRINSICS
            BitOperations.LeadingZeroCount(result);
#else
            maxLeadingZeroes;
#endif

        if (leadingZeroes > maxLeadingZeroes)
            result += NextUInt32(Math.Min((uint)(ulong.MaxValue - result), 1u << leadingZeroes));
        return result;
    }

    protected virtual double Sample(double maxValue) => Sample(0, maxValue);
    protected virtual double Sample(double minValue, double maxValue)
    {
        if (minValue == maxValue)
            return 0;
        return Sample() * (maxValue - minValue) + minValue;
    }
}
