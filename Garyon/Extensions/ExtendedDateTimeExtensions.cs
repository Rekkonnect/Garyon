#if HAS_DATEONLY_TIMEONLY

using System;

namespace Garyon.Extensions;

/// <summary>
/// Contains extensions focused on interoperability between
/// <see cref="DateTime"/>, <see cref="DateOnly"/> and <see cref="TimeOnly"/>.
/// </summary>
public static class ExtendedDateTimeExtensions
{
    public static DateOnly? ToDateOnly(this DateTime? dateTime)
    {
        return dateTime?.ToDateOnly();
    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> into a <see cref="DateOnly"/>.
    /// </summary>
    public static DateOnly ToDateOnly(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }

    public static DateTime? ToDateTime(this DateOnly? date)
    {
        return date?.ToDateTime();
    }

    public static DateTime ToDateTime(this DateOnly dateTime)
    {
        return dateTime.ToDateTime(time: default);
    }
}

#endif
