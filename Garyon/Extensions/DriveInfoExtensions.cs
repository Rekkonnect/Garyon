using Garyon.Reflection;
using System.IO;

namespace Garyon.Extensions;

public static class DriveInfoExtensions
{
    public static DriveFormatType GetDriveFormatType(this DriveInfo driveInfo)
    {
        try
        {
            return CodedEnumInfo.ParseCode<DriveFormatType>(driveInfo.DriveFormat);
        }
        catch
        {
            return DriveFormatType.Unknown;
        }
    }
}
