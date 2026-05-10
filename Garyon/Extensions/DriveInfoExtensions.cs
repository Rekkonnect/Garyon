using Garyon.Reflection;
using System;
using System.Collections.Generic;
using System.IO;

namespace Garyon.Extensions;

public static class DriveInfoExtensions
{
    private static readonly Dictionary<string, DriveFormatType> driveFormatTypeByCode = BuildDriveFormatTypeByCode();

    public static DriveFormatType GetDriveFormatType(this DriveInfo driveInfo)
    {
        var driveFormat = driveInfo.DriveFormat;
        if (driveFormat is null)
        {
            return DriveFormatType.Unknown;
        }

        return driveFormatTypeByCode.TryGetValue(driveFormat, out var value) ? value : DriveFormatType.Unknown;
    }

    private static Dictionary<string, DriveFormatType> BuildDriveFormatTypeByCode()
    {
        var enumToCode = AttributeMapping
            .ForEnum<DriveFormatType>()
            .WithAttributeKey<CodeAttribute, string>(static a => a.Code)
            .Build();

        var dictionary = new Dictionary<string, DriveFormatType>(enumToCode.Count, StringComparer.OrdinalIgnoreCase);
        foreach (var pair in enumToCode)
        {
            dictionary[pair.Value] = pair.Key;
        }

        return dictionary;
    }
}
