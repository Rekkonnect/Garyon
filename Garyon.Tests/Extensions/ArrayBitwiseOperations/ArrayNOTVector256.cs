using Garyon.Functions.UnmanagedHelpers;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations
{
    public class ArrayNOTVector256 : ArrayManipulationHelpersTestsBase
    {
        protected unsafe void NOTArray<TStruct>()
            where TStruct : unmanaged
        {
            PerformManipulationArray<TStruct>(NOTArrayVector256CustomType);
        }

        protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index) => ValueManipulation.NOT((TTarget)base.GetExpectedResult<TOrigin, TTarget>(origin, index));

        [Test]
        public unsafe void NOTByteArray()
        {
            NOTArray<byte>();
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((byte)~OriginalByteArray[i], TargetByteArray[i]);
        }
        [Test]
        public unsafe void NOTInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalInt16Array[i], TargetInt16Array[i]);
        }
        [Test]
        public unsafe void NOTInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalInt32Array[i], TargetInt32Array[i]);
        }
        [Test]
        public unsafe void NOTInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalInt64Array[i], TargetInt64Array[i]);
        }
        [Test]
        public unsafe void NOTSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalSByteArray[i], TargetSByteArray[i]);
        }
        [Test]
        public unsafe void NOTUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((ushort)~OriginalUInt16Array[i], TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void NOTUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalUInt32Array[i], TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void NOTUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalUInt64Array[i], TargetUInt64Array[i]);
        }
    }
}
