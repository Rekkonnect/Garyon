using Garyon.Objects;
using Garyon.Reflection;
using NUnit.Framework;
using System;
using System.Linq;

namespace Garyon.Tests.Objects
{
    public class NumericTests
    {
        [Test]
        public void NumericInitialization()
        {
            var supportedTypes = new[]
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal),
            };
            foreach (var t in supportedTypes)
                Assert.DoesNotThrow(() => InitializeInstance(t));

            TestForUnsupportedTypes();

            // Call this function after asserting for supported types once type checking with analyzers is implemented
            void TestForUnsupportedTypes()
            {
                var unsupportedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(IsValidTypeArgumentForINumeric).Except(supportedTypes);

                foreach (var t in unsupportedTypes)
                    Assert.Throws<TypeInitializationException>(() => InitializeInstance(t));
            }

            static void InitializeInstance(Type t)
            {
                var instance = typeof(Numeric<>).MakeGenericType(t).InitializeInstance(t.GetDefaultValue());
            }
        }

        [Test]
        public void OperationTest()
        {
            Assert.AreEqual(9, Add(4, 5));
            Assert.AreEqual(4, Subtract(9, 5));
            Assert.AreEqual(64, Multiply(32, 2));
            Assert.AreEqual(4, Divide(12, 3));
            Assert.AreEqual(2, Modulo(12, 5));

            var numeric = new Numeric<int>(15);
            numeric++;
            Assert.AreEqual(16, numeric.Value);
            numeric--;
            Assert.AreEqual(15, numeric.Value);

            Assert.IsTrue(GreaterThan(5, 4));
            Assert.IsTrue(GreaterThanOrEqual(5, 4));
            Assert.IsFalse(LessThan(5, 4));
            Assert.IsFalse(LessThanOrEqual(5, 4));
            Assert.IsFalse(Equal(5, 4));
            Assert.IsTrue(NotEqual(5, 4));

            Assert.IsFalse(GreaterThan(5, 5));
            Assert.IsTrue(GreaterThanOrEqual(5, 5));
            Assert.IsFalse(LessThan(5, 5));
            Assert.IsTrue(LessThanOrEqual(5, 5));
            Assert.IsTrue(Equal(5, 5));
            Assert.IsFalse(NotEqual(5, 5));
        }

        #region Operations
        private static T Add<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return (new Numeric<T>(left) + new Numeric<T>(right)).Value;
        }
        private static T Subtract<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return (new Numeric<T>(left) - new Numeric<T>(right)).Value;
        }
        private static T Multiply<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return (new Numeric<T>(left) * new Numeric<T>(right)).Value;
        }
        private static T Divide<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return (new Numeric<T>(left) / new Numeric<T>(right)).Value;
        }
        private static T Modulo<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return (new Numeric<T>(left) % new Numeric<T>(right)).Value;
        }

        private static bool LessThan<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new Numeric<T>(left) < new Numeric<T>(right);
        }
        private static bool LessThanOrEqual<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new Numeric<T>(left) <= new Numeric<T>(right);
        }
        private static bool GreaterThan<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new Numeric<T>(left) > new Numeric<T>(right);
        }
        private static bool GreaterThanOrEqual<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new Numeric<T>(left) >= new Numeric<T>(right);
        }
        private static bool Equal<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new Numeric<T>(left) == new Numeric<T>(right);
        }
        private static bool NotEqual<T>(T left, T right)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new Numeric<T>(left) != new Numeric<T>(right);
        }
        #endregion

        private static bool IsValidTypeArgumentForINumeric(Type t)
        {
            if (!t.IsValueType || t.ContainsGenericParameters || !t.IsValidTypeArgument() || !t.CanInheritInterfaces())
                return false;

            var comparable = typeof(IComparable<>).MakeGenericType(t);
            var equatable = typeof(IEquatable<>).MakeGenericType(t);
            return t.Implements(comparable) && t.Implements(equatable);
        }
    }
}
