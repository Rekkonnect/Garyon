using Garyon.Functions.UnmanagedHelpers;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using static TUnit.Assertions.Assert;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.Functions;

public class ValueManipulationTests
{
    private const byte value8 = 0x01;
    private const ushort value16 = 0xFF01;
    private const uint value32 = 0x1337FF01;
    private const ulong value64 = 0x223344551337FF01;

    #region Byte > T
    [Test]
    public async Task RescaleByteToByte()
    {
        byte rescaled = ValueManipulation.Rescale<byte, byte>(value8);
        await Assert.That(rescaled).IsEqualTo(value8);
    }
    [Test]
    public async Task RescaleByteToUInt16()
    {
        ushort rescaled = ValueManipulation.Rescale<byte, ushort>(value8);
        await Assert.That(rescaled).IsEqualTo(value8);
    }
    [Test]
    public async Task RescaleByteToUInt32()
    {
        uint rescaled = ValueManipulation.Rescale<byte, uint>(value8);
        await Assert.That(rescaled).IsEqualTo(value8);
    }
    [Test]
    public async Task RescaleByteToUInt64()
    {
        ulong rescaled = ValueManipulation.Rescale<byte, ulong>(value8);
        await Assert.That(rescaled).IsEqualTo(value8);
    }
    #endregion

    #region UInt16 > T
    [Test]
    public async Task RescaleUInt16ToByte()
    {
        byte rescaled = ValueManipulation.Rescale<ushort, byte>(value16);
        await Assert.That(rescaled).IsEqualTo(value8);
    }
    [Test]
    public async Task RescaleUInt16ToUInt16()
    {
        ushort rescaled = ValueManipulation.Rescale<ushort, ushort>(value16);
        await Assert.That(rescaled).IsEqualTo(value16);
    }
    [Test]
    public async Task RescaleUInt16ToUInt32()
    {
        uint rescaled = ValueManipulation.Rescale<ushort, uint>(value16);
        await Assert.That(rescaled).IsEqualTo(value16);
    }
    [Test]
    public async Task RescaleUInt16ToUInt64()
    {
        ulong rescaled = ValueManipulation.Rescale<ushort, ulong>(value16);
        await Assert.That(rescaled).IsEqualTo(value16);
    }
    #endregion

    #region UInt32 > T
    [Test]
    public async Task RescaleUInt32ToByte()
    {
        byte rescaled = ValueManipulation.Rescale<uint, byte>(value32);
        await Assert.That(rescaled).IsEqualTo(value8);
    }
    [Test]
    public async Task RescaleUInt32ToUInt16()
    {
        ushort rescaled = ValueManipulation.Rescale<uint, ushort>(value32);
        await Assert.That(rescaled).IsEqualTo(value16);
    }
    [Test]
    public async Task RescaleUInt32ToUInt32()
    {
        uint rescaled = ValueManipulation.Rescale<uint, uint>(value32);
        await Assert.That(rescaled).IsEqualTo(value32);
    }
    [Test]
    public async Task RescaleUInt32ToUInt64()
    {
        ulong rescaled = ValueManipulation.Rescale<uint, ulong>(value32);
        await Assert.That(rescaled).IsEqualTo(value32);
    }
    #endregion

    #region UInt64 > T
    [Test]
    public async Task RescaleUInt64ToByte()
    {
        byte rescaled = ValueManipulation.Rescale<ulong, byte>(value64);
        await Assert.That(rescaled).IsEqualTo(value8);
    }
    [Test]
    public async Task RescaleUInt64ToUInt16()
    {
        ushort rescaled = ValueManipulation.Rescale<ulong, ushort>(value64);
        await Assert.That(rescaled).IsEqualTo(value16);
    }
    [Test]
    public async Task RescaleUInt64ToUInt32()
    {
        uint rescaled = ValueManipulation.Rescale<ulong, uint>(value64);
        await Assert.That(rescaled).IsEqualTo(value32);
    }
    [Test]
    public async Task RescaleUInt64ToUInt64()
    {
        ulong rescaled = ValueManipulation.Rescale<ulong, ulong>(value64);
        await Assert.That(rescaled).IsEqualTo(value64);
    }
    #endregion
}