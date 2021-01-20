using Garyon.Objects;
using NUnit.Framework;
using System;

namespace Garyon.Tests.Objects
{
    [Parallelizable(ParallelScope.Children)]
    public class HourMinuteTests
    {
        [Test]
        public void InitializationTest()
        {
            var hm = new HourMinute(11, 53);
            AssertProperties();

            hm = new HourMinute(11 * 60 + 53);
            AssertProperties();

            void AssertProperties()
            {
                Assert.AreEqual(11, hm.Hour);
                Assert.AreEqual(53, hm.Minute);
                Assert.AreEqual((11 * 60 + 53) * 60, hm.TotalSeconds);
                Assert.AreEqual(11 * 60 + 53, hm.TotalMinutes);
                Assert.AreEqual(11 + 53 / 60d, hm.TotalHours);
            }
        }

        [Test]
        public void EqualsTest()
        {
            Assert.AreEqual(new HourMinute(11, 53), new HourMinute(11, 53));
        }

        [Test]
        public void AddTest()
        {
            var hm = new HourMinute(11, 53);
            hm.Add(4);
            Assert.AreEqual(new HourMinute(11, 57), hm);
            hm.Add(4);
            Assert.AreEqual(new HourMinute(12, 01), hm);
            hm.Add(62);
            Assert.AreEqual(new HourMinute(13, 03), hm);

            hm.Add(1, 4);
            Assert.AreEqual(new HourMinute(14, 07), hm);

            Assert.AreEqual(new HourMinute(15, 00), new HourMinute(14, 37) + 23);
            Assert.AreEqual(new HourMinute(15, 00), new HourMinute(13, 37) + new HourMinute(01, 23));
        }
        [Test]
        public void SubtractTest()
        {
            var hm = new HourMinute(12, 01);
            hm.Subtract(4);
            Assert.AreEqual(new HourMinute(11, 57), hm);
            hm.Subtract(4);
            Assert.AreEqual(new HourMinute(11, 53), hm);
            hm.Subtract(62);
            Assert.AreEqual(new HourMinute(10, 51), hm);

            hm.Subtract(1, 4);
            Assert.AreEqual(new HourMinute(09, 47), hm);

            Assert.AreEqual(new HourMinute(14, 37), new HourMinute(15, 00) - 23);
            Assert.AreEqual(new HourMinute(13, 37), new HourMinute(15, 00) - new HourMinute(01, 23));
        }

        [Test]
        public void ComparisonTest()
        {
            Assert.IsTrue(new HourMinute(10, 59) < new HourMinute(11, 00));
            Assert.IsFalse(new HourMinute(11, 00) < new HourMinute(11, 00));
            Assert.IsFalse(new HourMinute(11, 01) < new HourMinute(11, 00));

            Assert.IsTrue(new HourMinute(10, 59) <= new HourMinute(11, 00));
            Assert.IsTrue(new HourMinute(11, 00) <= new HourMinute(11, 00));
            Assert.IsFalse(new HourMinute(11, 01) <= new HourMinute(11, 00));

            Assert.IsFalse(new HourMinute(10, 59) == new HourMinute(11, 00));
            Assert.IsTrue(new HourMinute(11, 00) == new HourMinute(11, 00));
            Assert.IsFalse(new HourMinute(11, 01) == new HourMinute(11, 00));

            Assert.IsFalse(new HourMinute(10, 59) >= new HourMinute(11, 00));
            Assert.IsTrue(new HourMinute(11, 00) >= new HourMinute(11, 00));
            Assert.IsTrue(new HourMinute(11, 01) >= new HourMinute(11, 00));

            Assert.IsFalse(new HourMinute(10, 59) > new HourMinute(11, 00));
            Assert.IsFalse(new HourMinute(11, 00) > new HourMinute(11, 00));
            Assert.IsTrue(new HourMinute(11, 01) > new HourMinute(11, 00));

            Assert.IsTrue(new HourMinute(10, 59) != new HourMinute(11, 00));
            Assert.IsFalse(new HourMinute(11, 00) != new HourMinute(11, 00));
            Assert.IsTrue(new HourMinute(11, 01) != new HourMinute(11, 00));
        }

        [Test]
        public void PropertySetterTest()
        {
            var hm = new HourMinute();
            hm.TotalHours = 14.25;
            Assert.AreEqual(new HourMinute(14, 15), hm);
            hm.Hour = 16;
            Assert.AreEqual(new HourMinute(16, 15), hm);
            hm.Minute = 42;
            Assert.AreEqual(new HourMinute(16, 42), hm);
        }

        [Test]
        public void ParseTest()
        {
            Assert.AreEqual(new HourMinute(09, 52), HourMinute.Parse("09:52"));
            Assert.AreEqual(new HourMinute(09, 52), HourMinute.Parse("09:52:51"));
            Assert.AreEqual(new HourMinute(09, 03), HourMinute.Parse("9:03"));
            Assert.AreEqual(new HourMinute(21, 03), HourMinute.Parse("21:3"));
            Assert.AreEqual(new HourMinute(09, 03), HourMinute.Parse("9:3"));
        }

        [Test]
        public void ToStringTest()
        {
            Assert.AreEqual("09:32", new HourMinute(09, 32).ToString());
            Assert.AreEqual("16:00", new HourMinute(16, 00).ToString());
            Assert.AreEqual("00:00", new HourMinute(00, 00).ToString());
        }

        [Test(ExpectedResult = true)]
        public bool NowTest()
        {
            return EvaluateTwice(Evaluate);

            static bool Evaluate()
            {
                var hmNow = HourMinute.Now;
                var dateTimeNow = DateTime.Now;

                return hmNow == (HourMinute)dateTimeNow;
            }
        }
        [Test(ExpectedResult = true)]
        public bool NextMinuteTest()
        {
            return EvaluateTwice(Evaluate);

            static bool Evaluate()
            {
                var hmNextMinute = HourMinute.NextMinute;
                var dateTimeNextMinute = DateTime.Now + new TimeSpan(0, 1, 0);

                return hmNextMinute == (HourMinute)dateTimeNextMinute;
            }
        }
        [Test(ExpectedResult = true)]
        public bool NextHourTest()
        {
            return EvaluateTwice(Evaluate);

            static bool Evaluate()
            {
                var hmNextHour = HourMinute.NextHour;
                var dateTimeNextHour = DateTime.Now + new TimeSpan(1, 0, 0);

                return hmNextHour == (HourMinute)dateTimeNextHour;
            }
        }
        [Test(ExpectedResult = true)]
        public bool ToDateTimeTest()
        {
            return EvaluateTwice(Evaluate);

            static bool Evaluate()
            {
                var hmNow = HourMinute.Now;
                DateTime dateTimeNow = hmNow;

                return hmNow == (HourMinute)dateTimeNow;
            }
        }
        [Test(ExpectedResult = true)]
        public bool ToDateTimeOffsetTest()
        {
            return EvaluateTwice(Evaluate);

            static bool Evaluate()
            {
                var hmNow = HourMinute.Now;
                DateTimeOffset dateTimeOffsetNow = hmNow;

                return hmNow == (HourMinute)dateTimeOffsetNow;
            }
        }

        private bool EvaluateTwice(Func<bool> test)
        {
            // Evaluate twice for the extremely unlikely event the Now properties are retrieved at a different minute
            // It would be interesting to calculate how unlikely this event is, but it's probable nonetheless
            for (int i = 0; i < 2; i++)
                if (test())
                    return true;

            return false;
        }
    }
}
