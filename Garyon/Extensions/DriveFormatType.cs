using Garyon.Reflection;

namespace Garyon.Extensions;

/// <summary>
/// Contains the known drive format types, including FAT, FAT32, NTFS, UDF, APFS
/// and more.
/// </summary>
public enum DriveFormatType
{
    /// <summary>
    /// Denotes that the drive format type is unknown.
    /// </summary>
    Unknown,

    [Code("FAT")]
    Fat,
    [Code("FAT32")]
    Fat32,
    [Code("NTFS")]
    Ntfs,
    [Code("exFAT")]
    ExFat,
    [Code("ReFS")]
    ReFS,
    [Code("UDF")]
    Udf,
    [Code("APFS")]
    Apfs,
}
