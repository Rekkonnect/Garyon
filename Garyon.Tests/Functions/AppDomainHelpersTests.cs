using Garyon.Functions;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Functions;

public class AppDomainHelpersTests
{
    [Test]
    public async Task GetStaticAssembliesContainsCurrentAssemblyTest()
    {
        var assemblies = AppDomain.CurrentDomain.GetStaticAssemblies().ToArray();

        await Assert.That(assemblies.Any(a => a == typeof(AppDomainHelpers).Assembly)).IsTrue();
        await Assert.That(assemblies.All(a => !a.IsDynamic)).IsTrue();
    }

    [Test]
    public async Task GetDynamicAssembliesContainsDynamicAssemblyTest()
    {
        var assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("Garyon.Tests.DynamicAssembly"), AssemblyBuilderAccess.Run);
        _ = assembly.DefineDynamicModule("Main");

        var assemblies = AppDomain.CurrentDomain.GetDynamicAssemblies().ToArray();

        await Assert.That(assembly.IsDynamic).IsTrue();
        await Assert.That(assemblies.All(a => a.IsDynamic)).IsTrue();
        await Assert.That(assemblies.Any(a => a == typeof(AppDomainHelpers).Assembly)).IsFalse();
    }

    [Test]
    public async Task StaticAndDynamicAssembliesDoNotOverlapTest()
    {
        var staticAssemblies = AppDomain.CurrentDomain.GetStaticAssemblies().ToArray();
        var dynamicAssemblies = AppDomain.CurrentDomain.GetDynamicAssemblies().ToArray();

        await Assert.That(staticAssemblies.Intersect(dynamicAssemblies).Any()).IsFalse();
    }
}
