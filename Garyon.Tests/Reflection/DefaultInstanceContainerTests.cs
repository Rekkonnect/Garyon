using Garyon.Reflection;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Reflection;

public class DefaultInstanceContainerTests
{
    private readonly NotInvalidTypeInstanceContainer container = new();

    [Test]
    public async Task GetDefaultInstanceTests()
    {
        await Assert.That(container.GetDefaultInstance<Base>()).IsNull();
        await Assert.That(container.GetDefaultInstance<InvalidImplementation>()).IsNull();
        await AssertInstanceOf<ImplementationA>();
        await AssertInstanceOf<ImplementationB>();
        await AssertInstanceOf<ImplementationC>();

        async Task AssertInstanceOf<T>()
            where T : Base
        {
            await Assert.That(container.GetDefaultInstance<T>()).IsTypeOf<T>();
        }
    }

    [Test]
    public async Task GetIrrelevantDefaultInstancesTests()
    {
        // Some other random types that are found in the assemblies
        await Assert.That(container.GetDefaultInstance(typeof(Enum))).IsNull();
        await Assert.That(container.GetDefaultInstance(typeof(ImplementationA[]))).IsNull();
        await Assert.That(container.GetDefaultInstance(typeof(Delegate))).IsNull();
        await Assert.That(container.GetDefaultInstance(typeof(DayOfWeek))).IsNull();
    }

    private sealed class NotInvalidTypeInstanceContainer : DefaultInstanceContainer<Base>
    {
        protected override object[] GetDefaultInstanceArguments()
        {
            return new object[] { Array.Empty<int>() };
        }

        protected override bool IsValidInstanceType(Type type)
        {
            return !type.Name.Contains("Invalid");
        }
    }

    private abstract class Base
    {
        protected Base(params int[] values) { }
    }

    private sealed class ImplementationA : Base
    {
        public ImplementationA(params int[] values)
            : base(values) { }
    }
    private sealed class ImplementationB : Base
    {
        public ImplementationB(params int[] values)
            : base(values) { }
    }
    private sealed class ImplementationC : Base
    {
        public ImplementationC(params int[] values)
            : base(values) { }
    }

    private sealed class InvalidImplementation : Base
    {
        public InvalidImplementation(params int[] values)
            : base(values) { }
    }
}