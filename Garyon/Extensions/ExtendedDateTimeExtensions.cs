#if HAS_DATEONLY_TIMEONLY

using System;

namespace Garyon.Extensions;

/// <summary>
/// Contains extensions focused on interoperability between
/// <see cref="DateTime"/>, <see cref="DateTimeOffset"/>,
/// <see cref="DateOnly"/> and <see cref="TimeOnly"/>.
/// </summary>
public static class ExtendedDateTimeExtensions
{
    /// <summary>
    /// Converts a <see cref="DateTime"/> into a <see cref="DateOnly"/>.
    /// </summary>
    public static DateOnly ToDateOnly(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }

    public static DateTime ToDateTime(this DateOnly dateTime)
    {
        return dateTime.ToDateTime(time: default);
    }

    public static TimeSpan Subtract(this DateOnly date, DateOnly other)
    {
        return date.ToDateTime(default) - other.ToDateTime(default);
    }
}

#endif
