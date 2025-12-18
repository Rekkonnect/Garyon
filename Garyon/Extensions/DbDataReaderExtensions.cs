using System.Data.Common;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="DbDataReader"/>.
/// </summary>
public static class DbDataReaderExtensions
{
    public static string? GetNullableString(this DbDataReader reader, int ordinal)
    {
        if (reader.IsDBNull(ordinal))
            return null;
        return reader.GetString(ordinal);
    }

    public static long? GetNullableInt64(this DbDataReader reader, int ordinal)
    {
        if (reader.IsDBNull(ordinal))
            return null;
        return reader.GetInt64(ordinal);
    }

    public static int? GetNullableInt32(this DbDataReader reader, int ordinal)
    {
        if (reader.IsDBNull(ordinal))
            return null;
        return reader.GetInt32(ordinal);
    }
}
