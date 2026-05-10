using Garyon.Objects.Advanced;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class AdvancedRandomTests
{
    [Test]
    public async Task InvokingPublicRandomMethodsMultipleTimesDoesNotThrowTest()
    {
        AdvancedRandom[] randoms =
        [
            new(),
            new AdvancedRandom((byte)1),
            new AdvancedRandom((sbyte)2),
            new AdvancedRandom((short)3),
            new AdvancedRandom((ushort)4),
            new AdvancedRandom(5),
            new AdvancedRandom((uint)6),
            new AdvancedRandom(7L),
            new AdvancedRandom(8UL),
            new AdvancedRandom(1.25f),
            new AdvancedRandom(2.5),
        ];

        Action<AdvancedRandom>[] invokers =
        [
            static random => { _ = random.Next(); },
            static random => { _ = random.Next(10); },
            static random => { _ = random.Next(2, 10); },
            static random => { var buffer = new byte[8]; random.NextBytes(buffer); },
            static random => { Span<byte> buffer = stackalloc byte[8]; random.NextBytes(buffer); },
            static random => { _ = random.NextDouble(); },
            static random => { _ = random.NextInt64(); },
            static random => { _ = random.NextInt64(10); },
            static random => { _ = random.NextInt64(2, 10); },
            static random => { _ = random.NextSingle(); },
            static random => { _ = random.NextByte(); },
            static random => { _ = random.NextByte((byte)10); },
            static random => { _ = random.NextByte((byte)2, (byte)10); },
            static random => { _ = random.NextInt16(); },
            static random => { _ = random.NextInt16((short)10); },
            static random => { _ = random.NextInt16((short)2, (short)10); },
            static random => { _ = random.NextSByte(); },
            static random => { _ = random.NextSByte((sbyte)10); },
            static random => { _ = random.NextSByte((sbyte)2, (sbyte)10); },
            static random => { _ = random.NextUInt16(); },
            static random => { _ = random.NextUInt16((ushort)10); },
            static random => { _ = random.NextUInt16((ushort)2, (ushort)10); },
            static random => { _ = random.NextUInt32(); },
            static random => { _ = random.NextUInt32((uint)10); },
            static random => { _ = random.NextUInt32((uint)2, (uint)10); },
            static random => { _ = random.NextUInt64(); },
            static random => { _ = random.NextUInt64((ulong)10); },
            static random => { _ = random.NextUInt64((ulong)2, (ulong)10); },
            static random => { _ = random.NextSingle(10); },
            static random => { _ = random.NextSingle(2, 10); },
            static random => { _ = random.NextDouble(10); },
            static random => { _ = random.NextDouble(2, 10); },
            static random => { _ = random.NextChar(); },
            static random => { _ = random.NextChar((char)10); },
            static random => { _ = random.NextChar((char)2, (char)10); },
            static random => { _ = random.NextBoolean(); },
        ];

        foreach (var random in randoms)
        {
            foreach (var invoker in invokers)
            {
                for (int i = 0; i < 10; i++)
                    invoker(random);
            }
        }

        await Assert.That(true).IsTrue();
    }
}
