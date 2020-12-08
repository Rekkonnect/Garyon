using Garyon.Functions.PointerHelpers;
using Garyon.QualityControl.SizedStructs;
using NUnit.Framework;

namespace Garyon.Tests.Functions.PointersHelpers
{
    public unsafe class PointerArithmeticTests
    {
        private byte* p1;
        private sbyte* p2;
        private short* p3;
        private int* p4;
        private long* p5;
        private decimal* p6;
        private Struct3* p7;
        private Struct31* p8;

        #region Increment
        [Test]
        public void IncrementInt32()
        {
            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, 2);
            AssertPointerAdjustment(p1, 2);
            AssertPointerAdjustment(p2, 2);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p3, ref p4, 3);
            AssertPointerAdjustment(p3, 3);
            AssertPointerAdjustment(p4, 3);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p5, ref p6, ref p7, 4);
            AssertPointerAdjustment(p5, 4);
            AssertPointerAdjustment(p6, 4);
            AssertPointerAdjustment(p7, 4);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p8, 1);
            AssertPointerAdjustment(p1, 1);
            AssertPointerAdjustment(p2, 1);
            AssertPointerAdjustment(p3, 1);
            AssertPointerAdjustment(p8, 1);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p4, ref p5, ref p8, 1);
            AssertPointerAdjustment(p1, 1);
            AssertPointerAdjustment(p2, 1);
            AssertPointerAdjustment(p4, 1);
            AssertPointerAdjustment(p5, 1);
            AssertPointerAdjustment(p8, 1);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p4, ref p6, ref p7, ref p8, 4);
            AssertPointerAdjustment(p1, 4);
            AssertPointerAdjustment(p2, 4);
            AssertPointerAdjustment(p4, 4);
            AssertPointerAdjustment(p6, 4);
            AssertPointerAdjustment(p7, 4);
            AssertPointerAdjustment(p8, 4);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, 2);
            AssertPointerAdjustment(p1, 2);
            AssertPointerAdjustment(p2, 2);
            AssertPointerAdjustment(p3, 2);
            AssertPointerAdjustment(p4, 2);
            AssertPointerAdjustment(p5, 2);
            AssertPointerAdjustment(p6, 2);
            AssertPointerAdjustment(p7, 2);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, 3);
            AssertPointerAdjustment(p1, 3);
            AssertPointerAdjustment(p2, 3);
            AssertPointerAdjustment(p3, 3);
            AssertPointerAdjustment(p4, 3);
            AssertPointerAdjustment(p5, 3);
            AssertPointerAdjustment(p6, 3);
            AssertPointerAdjustment(p7, 3);
            AssertPointerAdjustment(p8, 3);
        }
        [Test]
        public void IncrementUInt32()
        {
            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, 2U);
            AssertPointerAdjustment(p1, 2);
            AssertPointerAdjustment(p2, 2);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p3, ref p4, 3U);
            AssertPointerAdjustment(p3, 3);
            AssertPointerAdjustment(p4, 3);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p5, ref p6, ref p7, 4U);
            AssertPointerAdjustment(p5, 4);
            AssertPointerAdjustment(p6, 4);
            AssertPointerAdjustment(p7, 4);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p8, 1U);
            AssertPointerAdjustment(p1, 1);
            AssertPointerAdjustment(p2, 1);
            AssertPointerAdjustment(p3, 1);
            AssertPointerAdjustment(p8, 1);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p4, ref p5, ref p8, 1U);
            AssertPointerAdjustment(p1, 1);
            AssertPointerAdjustment(p2, 1);
            AssertPointerAdjustment(p4, 1);
            AssertPointerAdjustment(p5, 1);
            AssertPointerAdjustment(p8, 1);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p4, ref p6, ref p7, ref p8, 4U);
            AssertPointerAdjustment(p1, 4);
            AssertPointerAdjustment(p2, 4);
            AssertPointerAdjustment(p4, 4);
            AssertPointerAdjustment(p6, 4);
            AssertPointerAdjustment(p7, 4);
            AssertPointerAdjustment(p8, 4);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, 2U);
            AssertPointerAdjustment(p1, 2);
            AssertPointerAdjustment(p2, 2);
            AssertPointerAdjustment(p3, 2);
            AssertPointerAdjustment(p4, 2);
            AssertPointerAdjustment(p5, 2);
            AssertPointerAdjustment(p6, 2);
            AssertPointerAdjustment(p7, 2);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, 3U);
            AssertPointerAdjustment(p1, 3);
            AssertPointerAdjustment(p2, 3);
            AssertPointerAdjustment(p3, 3);
            AssertPointerAdjustment(p4, 3);
            AssertPointerAdjustment(p5, 3);
            AssertPointerAdjustment(p6, 3);
            AssertPointerAdjustment(p7, 3);
            AssertPointerAdjustment(p8, 3);
        }
        [Test]
        public void IncrementInt64()
        {
            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, 2L);
            AssertPointerAdjustment(p1, 2);
            AssertPointerAdjustment(p2, 2);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p3, ref p4, 3L);
            AssertPointerAdjustment(p3, 3);
            AssertPointerAdjustment(p4, 3);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p5, ref p6, ref p7, 4L);
            AssertPointerAdjustment(p5, 4);
            AssertPointerAdjustment(p6, 4);
            AssertPointerAdjustment(p7, 4);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p8, 1L);
            AssertPointerAdjustment(p1, 1);
            AssertPointerAdjustment(p2, 1);
            AssertPointerAdjustment(p3, 1);
            AssertPointerAdjustment(p8, 1);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p4, ref p5, ref p8, 1L);
            AssertPointerAdjustment(p1, 1);
            AssertPointerAdjustment(p2, 1);
            AssertPointerAdjustment(p4, 1);
            AssertPointerAdjustment(p5, 1);
            AssertPointerAdjustment(p8, 1);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p4, ref p6, ref p7, ref p8, 4L);
            AssertPointerAdjustment(p1, 4);
            AssertPointerAdjustment(p2, 4);
            AssertPointerAdjustment(p4, 4);
            AssertPointerAdjustment(p6, 4);
            AssertPointerAdjustment(p7, 4);
            AssertPointerAdjustment(p8, 4);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, 2L);
            AssertPointerAdjustment(p1, 2);
            AssertPointerAdjustment(p2, 2);
            AssertPointerAdjustment(p3, 2);
            AssertPointerAdjustment(p4, 2);
            AssertPointerAdjustment(p5, 2);
            AssertPointerAdjustment(p6, 2);
            AssertPointerAdjustment(p7, 2);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, 3L);
            AssertPointerAdjustment(p1, 3);
            AssertPointerAdjustment(p2, 3);
            AssertPointerAdjustment(p3, 3);
            AssertPointerAdjustment(p4, 3);
            AssertPointerAdjustment(p5, 3);
            AssertPointerAdjustment(p6, 3);
            AssertPointerAdjustment(p7, 3);
            AssertPointerAdjustment(p8, 3);
        }
        [Test]
        public void IncrementUInt64()
        {
            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, 2UL);
            AssertPointerAdjustment(p1, 2);
            AssertPointerAdjustment(p2, 2);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p3, ref p4, 3UL);
            AssertPointerAdjustment(p3, 3);
            AssertPointerAdjustment(p4, 3);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p5, ref p6, ref p7, 4UL);
            AssertPointerAdjustment(p5, 4);
            AssertPointerAdjustment(p6, 4);
            AssertPointerAdjustment(p7, 4);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p8, 1UL);
            AssertPointerAdjustment(p1, 1);
            AssertPointerAdjustment(p2, 1);
            AssertPointerAdjustment(p3, 1);
            AssertPointerAdjustment(p8, 1);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p4, ref p5, ref p8, 1UL);
            AssertPointerAdjustment(p1, 1);
            AssertPointerAdjustment(p2, 1);
            AssertPointerAdjustment(p4, 1);
            AssertPointerAdjustment(p5, 1);
            AssertPointerAdjustment(p8, 1);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p4, ref p6, ref p7, ref p8, 4UL);
            AssertPointerAdjustment(p1, 4);
            AssertPointerAdjustment(p2, 4);
            AssertPointerAdjustment(p4, 4);
            AssertPointerAdjustment(p6, 4);
            AssertPointerAdjustment(p7, 4);
            AssertPointerAdjustment(p8, 4);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, 2UL);
            AssertPointerAdjustment(p1, 2);
            AssertPointerAdjustment(p2, 2);
            AssertPointerAdjustment(p3, 2);
            AssertPointerAdjustment(p4, 2);
            AssertPointerAdjustment(p5, 2);
            AssertPointerAdjustment(p6, 2);
            AssertPointerAdjustment(p7, 2);

            ResetPointersForIncrementation();

            PointerArithmetic.Increment(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, 3UL);
            AssertPointerAdjustment(p1, 3);
            AssertPointerAdjustment(p2, 3);
            AssertPointerAdjustment(p3, 3);
            AssertPointerAdjustment(p4, 3);
            AssertPointerAdjustment(p5, 3);
            AssertPointerAdjustment(p6, 3);
            AssertPointerAdjustment(p7, 3);
            AssertPointerAdjustment(p8, 3);
        }
        #endregion

        #region Decrement
        [Test]
        public void DecrementInt32()
        {
            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, 2);
            AssertPointerAdjustment(p1, 8);
            AssertPointerAdjustment(p2, 8);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p3, ref p4, 3);
            AssertPointerAdjustment(p3, 7);
            AssertPointerAdjustment(p4, 7);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p5, ref p6, ref p7, 4);
            AssertPointerAdjustment(p5, 6);
            AssertPointerAdjustment(p6, 6);
            AssertPointerAdjustment(p7, 6);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p8, 1);
            AssertPointerAdjustment(p1, 9);
            AssertPointerAdjustment(p2, 9);
            AssertPointerAdjustment(p3, 9);
            AssertPointerAdjustment(p8, 9);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p4, ref p5, ref p8, 1);
            AssertPointerAdjustment(p1, 9);
            AssertPointerAdjustment(p2, 9);
            AssertPointerAdjustment(p4, 9);
            AssertPointerAdjustment(p5, 9);
            AssertPointerAdjustment(p8, 9);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p4, ref p6, ref p7, ref p8, 4);
            AssertPointerAdjustment(p1, 6);
            AssertPointerAdjustment(p2, 6);
            AssertPointerAdjustment(p4, 6);
            AssertPointerAdjustment(p6, 6);
            AssertPointerAdjustment(p7, 6);
            AssertPointerAdjustment(p8, 6);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, 2);
            AssertPointerAdjustment(p1, 8);
            AssertPointerAdjustment(p2, 8);
            AssertPointerAdjustment(p3, 8);
            AssertPointerAdjustment(p4, 8);
            AssertPointerAdjustment(p5, 8);
            AssertPointerAdjustment(p6, 8);
            AssertPointerAdjustment(p7, 8);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, 3);
            AssertPointerAdjustment(p1, 7);
            AssertPointerAdjustment(p2, 7);
            AssertPointerAdjustment(p3, 7);
            AssertPointerAdjustment(p4, 7);
            AssertPointerAdjustment(p5, 7);
            AssertPointerAdjustment(p6, 7);
            AssertPointerAdjustment(p7, 7);
            AssertPointerAdjustment(p8, 7);
        }
        [Test]
        public void DecrementUInt32()
        {
            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, 2U);
            AssertPointerAdjustment(p1, 8);
            AssertPointerAdjustment(p2, 8);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p3, ref p4, 3U);
            AssertPointerAdjustment(p3, 7);
            AssertPointerAdjustment(p4, 7);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p5, ref p6, ref p7, 4U);
            AssertPointerAdjustment(p5, 6);
            AssertPointerAdjustment(p6, 6);
            AssertPointerAdjustment(p7, 6);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p8, 1U);
            AssertPointerAdjustment(p1, 9);
            AssertPointerAdjustment(p2, 9);
            AssertPointerAdjustment(p3, 9);
            AssertPointerAdjustment(p8, 9);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p4, ref p5, ref p8, 1U);
            AssertPointerAdjustment(p1, 9);
            AssertPointerAdjustment(p2, 9);
            AssertPointerAdjustment(p4, 9);
            AssertPointerAdjustment(p5, 9);
            AssertPointerAdjustment(p8, 9);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p4, ref p6, ref p7, ref p8, 4U);
            AssertPointerAdjustment(p1, 6);
            AssertPointerAdjustment(p2, 6);
            AssertPointerAdjustment(p4, 6);
            AssertPointerAdjustment(p6, 6);
            AssertPointerAdjustment(p7, 6);
            AssertPointerAdjustment(p8, 6);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, 2U);
            AssertPointerAdjustment(p1, 8);
            AssertPointerAdjustment(p2, 8);
            AssertPointerAdjustment(p3, 8);
            AssertPointerAdjustment(p4, 8);
            AssertPointerAdjustment(p5, 8);
            AssertPointerAdjustment(p6, 8);
            AssertPointerAdjustment(p7, 8);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, 3U);
            AssertPointerAdjustment(p1, 7);
            AssertPointerAdjustment(p2, 7);
            AssertPointerAdjustment(p3, 7);
            AssertPointerAdjustment(p4, 7);
            AssertPointerAdjustment(p5, 7);
            AssertPointerAdjustment(p6, 7);
            AssertPointerAdjustment(p7, 7);
            AssertPointerAdjustment(p8, 7);
        }
        [Test]
        public void DecrementInt64()
        {
            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, 2L);
            AssertPointerAdjustment(p1, 8);
            AssertPointerAdjustment(p2, 8);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p3, ref p4, 3L);
            AssertPointerAdjustment(p3, 7);
            AssertPointerAdjustment(p4, 7);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p5, ref p6, ref p7, 4L);
            AssertPointerAdjustment(p5, 6);
            AssertPointerAdjustment(p6, 6);
            AssertPointerAdjustment(p7, 6);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p8, 1L);
            AssertPointerAdjustment(p1, 9);
            AssertPointerAdjustment(p2, 9);
            AssertPointerAdjustment(p3, 9);
            AssertPointerAdjustment(p8, 9);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p4, ref p5, ref p8, 1L);
            AssertPointerAdjustment(p1, 9);
            AssertPointerAdjustment(p2, 9);
            AssertPointerAdjustment(p4, 9);
            AssertPointerAdjustment(p5, 9);
            AssertPointerAdjustment(p8, 9);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p4, ref p6, ref p7, ref p8, 4L);
            AssertPointerAdjustment(p1, 6);
            AssertPointerAdjustment(p2, 6);
            AssertPointerAdjustment(p4, 6);
            AssertPointerAdjustment(p6, 6);
            AssertPointerAdjustment(p7, 6);
            AssertPointerAdjustment(p8, 6);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, 2L);
            AssertPointerAdjustment(p1, 8);
            AssertPointerAdjustment(p2, 8);
            AssertPointerAdjustment(p3, 8);
            AssertPointerAdjustment(p4, 8);
            AssertPointerAdjustment(p5, 8);
            AssertPointerAdjustment(p6, 8);
            AssertPointerAdjustment(p7, 8);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, 3L);
            AssertPointerAdjustment(p1, 7);
            AssertPointerAdjustment(p2, 7);
            AssertPointerAdjustment(p3, 7);
            AssertPointerAdjustment(p4, 7);
            AssertPointerAdjustment(p5, 7);
            AssertPointerAdjustment(p6, 7);
            AssertPointerAdjustment(p7, 7);
            AssertPointerAdjustment(p8, 7);
        }
        [Test]
        public void DecrementUInt64()
        {
            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, 2UL);
            AssertPointerAdjustment(p1, 8);
            AssertPointerAdjustment(p2, 8);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p3, ref p4, 3UL);
            AssertPointerAdjustment(p3, 7);
            AssertPointerAdjustment(p4, 7);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p5, ref p6, ref p7, 4UL);
            AssertPointerAdjustment(p5, 6);
            AssertPointerAdjustment(p6, 6);
            AssertPointerAdjustment(p7, 6);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p8, 1UL);
            AssertPointerAdjustment(p1, 9);
            AssertPointerAdjustment(p2, 9);
            AssertPointerAdjustment(p3, 9);
            AssertPointerAdjustment(p8, 9);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p4, ref p5, ref p8, 1UL);
            AssertPointerAdjustment(p1, 9);
            AssertPointerAdjustment(p2, 9);
            AssertPointerAdjustment(p4, 9);
            AssertPointerAdjustment(p5, 9);
            AssertPointerAdjustment(p8, 9);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p4, ref p6, ref p7, ref p8, 4UL);
            AssertPointerAdjustment(p1, 6);
            AssertPointerAdjustment(p2, 6);
            AssertPointerAdjustment(p4, 6);
            AssertPointerAdjustment(p6, 6);
            AssertPointerAdjustment(p7, 6);
            AssertPointerAdjustment(p8, 6);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, 2UL);
            AssertPointerAdjustment(p1, 8);
            AssertPointerAdjustment(p2, 8);
            AssertPointerAdjustment(p3, 8);
            AssertPointerAdjustment(p4, 8);
            AssertPointerAdjustment(p5, 8);
            AssertPointerAdjustment(p6, 8);
            AssertPointerAdjustment(p7, 8);

            ResetPointersForDecrementation();

            PointerArithmetic.Decrement(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, 3UL);
            AssertPointerAdjustment(p1, 7);
            AssertPointerAdjustment(p2, 7);
            AssertPointerAdjustment(p3, 7);
            AssertPointerAdjustment(p4, 7);
            AssertPointerAdjustment(p5, 7);
            AssertPointerAdjustment(p6, 7);
            AssertPointerAdjustment(p7, 7);
            AssertPointerAdjustment(p8, 7);
        }
        #endregion

        private static void AssertPointerAdjustment<T>(T* pointer, int totalValue)
            where T : unmanaged
        {
            Assert.IsTrue((int)pointer == totalValue * sizeof(T));
        }

        private void ResetPointersForIncrementation() => ResetPointers();
        private void ResetPointersForDecrementation() => ResetPointers(10);

        private void ResetPointers(int value = default)
        {
            ResetPointer(ref p1, value);
            ResetPointer(ref p2, value);
            ResetPointer(ref p3, value);
            ResetPointer(ref p4, value);
            ResetPointer(ref p5, value);
            ResetPointer(ref p6, value);
            ResetPointer(ref p7, value);
            ResetPointer(ref p8, value);
        }
        private static void ResetPointer<T>(ref T* pointer, int value)
            where T : unmanaged
        {
            pointer = (T*)(value * sizeof(T));
        }
    }
}
