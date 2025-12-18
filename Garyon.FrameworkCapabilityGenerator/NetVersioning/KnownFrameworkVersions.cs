namespace Garyon.FrameworkCapabilityGenerator.NetVersioning;

internal static class KnownFrameworkVersions
{
    public static readonly NetFrameworkVersion
        NetFramework10 = new(new(1, 0)),
        NetFramework11 = new(new(1, 1)),
        NetFramework20 = new(new(2, 0)),
        NetFramework30 = new(new(3, 0)),
        NetFramework35 = new(new(3, 5)),
        NetFramework40 = new(new(4, 0)),
        NetFramework45 = new(new(4, 5)),
        NetFramework451 = new(new(4, 5, 1)),
        NetFramework452 = new(new(4, 5, 2)),
        NetFramework46 = new(new(4, 6)),
        NetFramework461 = new(new(4, 6, 1)),
        NetFramework462 = new(new(4, 6, 2)),
        NetFramework47 = new(new(4, 7)),
        NetFramework471 = new(new(4, 7, 1)),
        NetFramework472 = new(new(4, 7, 2)),
        NetFramework48 = new(new(4, 8)),
        NetFramework481 = new(new(4, 8, 1))
        ;

    public static readonly NetStandardVersion
        NetStandard10 = new(new(1, 0)),
        NetStandard11 = new(new(1, 1)),
        NetStandard12 = new(new(1, 2)),
        NetStandard13 = new(new(1, 3)),
        NetStandard14 = new(new(1, 4)),
        NetStandard15 = new(new(1, 5)),
        NetStandard16 = new(new(1, 6)),
        NetStandard20 = new(new(2, 0)),
        NetStandard21 = new(new(2, 1))
        ;

    public static readonly NetCoreVersion
        NetCore10 = new(new(1, 0)),
        NetCore11 = new(new(1, 1)),
        NetCore20 = new(new(2, 0)),
        NetCore21 = new(new(2, 1)),
        NetCore22 = new(new(2, 2)),
        NetCore30 = new(new(3, 0)),
        NetCore31 = new(new(3, 1))
        ;

    public static readonly ModernNetVersion
        Net5 = new(new(5, 0)),
        Net6 = new(new(6, 0)),
        Net7 = new(new(7, 0)),
        Net8 = new(new(8, 0)),
        Net9 = new(new(9, 0)),
        Net10 = new(new(10, 0))
        ;

    public static IReadOnlyList<BaseFrameworkVersion> Versions
    {
        get
        {
            return field ??= GetVersions();

            static IReadOnlyList<BaseFrameworkVersion> GetVersions()
            {
                var versionFields = typeof(KnownFrameworkVersions)
                    .GetFields()
                    .Where(s => s.FieldType.IsSubclassOf(typeof(BaseFrameworkVersion)))
                    .ToList();

                var versions = versionFields
                    .Select(f => f.GetValue(null) as BaseFrameworkVersion)
                    .Where(v => v is not null)
                    .Select(v => v!)
                    .OrderBy(v => v.Version)
                    .ToList();

                return versions;
            }
        }
    }

    public static IReadOnlyDictionary<string, BaseFrameworkVersion> VersionsByMoniker
    {
        get
        {
            return field ??= GetVersionsDictionary();

            static IReadOnlyDictionary<string, BaseFrameworkVersion> GetVersionsDictionary()
            {
                return Versions
                    .ToDictionary(s => s.MonikerName);
            }
        }
    }
}
