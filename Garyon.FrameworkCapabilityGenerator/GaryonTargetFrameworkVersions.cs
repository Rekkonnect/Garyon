using Garyon.FrameworkCapabilityGenerator.NetVersioning;

namespace Garyon.FrameworkCapabilityGenerator;

internal static class GaryonTargetFrameworkVersions
{
    public static IReadOnlyList<BaseFrameworkVersion> Versions =>
        [
            KnownFrameworkVersions.NetFramework45,
            KnownFrameworkVersions.NetFramework472,
            KnownFrameworkVersions.NetFramework46,
            KnownFrameworkVersions.NetFramework48,

            KnownFrameworkVersions.NetStandard10,
            KnownFrameworkVersions.NetStandard20,
            KnownFrameworkVersions.NetStandard21,

            KnownFrameworkVersions.NetCore10,
            KnownFrameworkVersions.NetCore20,
            KnownFrameworkVersions.NetCore30,
            KnownFrameworkVersions.NetCore31,

            KnownFrameworkVersions.Net5,
            KnownFrameworkVersions.Net6,
            KnownFrameworkVersions.Net7,
            KnownFrameworkVersions.Net8,
            KnownFrameworkVersions.Net9,
            KnownFrameworkVersions.Net10,
        ];
}
