using Garyon.Reflection;
using System;
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

/// <summary>
/// Contains the known drive format types, including FAT,
/// FAT32, NTFS, UDF, APFS and more.
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

/// <summary>
/// Contains extensions for the <seealso cref="FileInfo"/> class.
/// </summary>
public static class FileInfoExtensions
{
    /// <summary>
    /// Gets the depth of this file, meaning the number of directories
    /// between the root and this file.
    /// </summary>
    /// <param name="file">The file whose depth to get.</param>
    /// <returns>
    /// The number of parent directories that must be traversed before
    /// reaching the file system root.
    /// </returns>
    public static int Depth(this FileInfo file)
    {
        return file.Directory.Depth() + 1;
    }

    public static DirectoryInfo? RecursiveParent(this FileInfo file, int levels = 1)
    {
        return file.Directory.RecursiveParent(levels);
    }
    public static DirectoryInfo BoundRecursiveParent(this FileInfo file, int levels = 1)
    {
        return file.Directory.BoundRecursiveParent(levels);
    }

    /// <summary>
    /// Moves the file to a parent directory, up to a number of levels.
    /// If the levels are more than the current depth of the file, an
    /// exception will be thrown.
    /// </summary>
    /// <param name="file">The file to move up by a number of levels.</param>
    /// <param name="levels">The number of levels to move up.</param>
    /// <returns>
    /// The <seealso cref="FileInfo"/> that was moved, containing its
    /// new information after it was moved.
    /// </returns>
    /// <remarks>
    /// Consider using <seealso cref="MoveUpBound(FileInfo, int)"/> if
    /// the depth of the file is not meant to be at least the provided.
    /// </remarks>
    public static FileInfo MoveUp(this FileInfo file, int levels = 1)
    {
        var newDirectory = file.RecursiveParent(levels)
            ?? throw new InvalidOperationException("The requested file's depth is less than the number of levels to move it up by.");
        return file.MoveToDirectory(newDirectory);
    }
    /// <summary>
    /// Moves the file to a parent directory, up to a number of levels.
    /// If the levels are more than the current depth of the file, it
    /// will be moved to the root of the drive.
    /// </summary>
    /// <param name="file">The file to move up by a number of levels.</param>
    /// <param name="levels">The number of levels to move up.</param>
    /// <returns>
    /// The <seealso cref="FileInfo"/> that was moved, containing its
    /// new information after it was moved.
    /// </returns>
    public static FileInfo MoveUpBound(this FileInfo file, int levels = 1)
    {
        var newDirectory = file.BoundRecursiveParent(levels);
        return file.MoveToDirectory(newDirectory);
    }
    /// <summary>
    /// Moves the file to another directory, preserving its file name.
    /// </summary>
    /// <param name="file">The file to move to another directory.</param>
    /// <param name="directory">The new directory to move the file to.</param>
    /// <returns>The file info of the moved file.</returns>
    public static FileInfo MoveToDirectory(this FileInfo file, DirectoryInfo directory)
    {
        return MoveTo(file, directory, file.Name);
    }

    /// <summary>
    /// Moves the file to another directory, and changes its name
    /// and extension.
    /// </summary>
    /// <param name="file">The file to move.</param>
    /// <param name="directory">The new directory that will contain the file.</param>
    /// <param name="name">The new name of the file without the extension.</param>
    /// <param name="extension">The new extension of the file.</param>
    /// <returns>The file info of the moved file.</returns>
    /// <remarks>
    /// This method only uses one IO operation, effectively changing
    /// the absolute file path.
    /// </remarks>
    public static FileInfo MoveTo(this FileInfo file, DirectoryInfo directory, string name, string extension)
    {
        return file.MoveTo(directory, $"{name}.{extension}");
    }
    /// <param name="nameWithExtension">
    /// The new name of the file, including its extension.
    /// </param>
    /// <inheritdoc cref="MoveTo(FileInfo, DirectoryInfo, string, string)"/>
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
    public static FileInfo MoveTo(this FileInfo file, DirectoryInfo directory, string nameWithExtension)
    {
        file.MoveTo(Path.Combine(directory.Name!, nameWithExtension));
        return file;
    }
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)

    /// <summary>Renames a file, changing both its name and extension.</summary>
    /// <param name="file">The file to rename.</param>
    /// <param name="nameWithExtension">The new name to set, including its extension.</param>
    /// <returns>The file info of the renamed file.</returns>
    /// <remarks>
    /// Consider using <seealso cref="RenameWithoutExtension(FileInfo, string)"/>
    /// to preserve the extension, or <seealso cref="ChangeExtension(FileInfo, string)"/>
    /// to only change the extension of the file.
    /// </remarks>
    public static FileInfo Rename(this FileInfo file, string nameWithExtension)
    {
        return MoveTo(file, file.Directory!, nameWithExtension);
    }
    /// <summary>Renames a file, changing both its name and extension.</summary>
    /// <param name="file">The file to rename.</param>
    /// <param name="name">The new name to set to the file, excluding its extension.</param>
    /// <param name="extension">
    /// The new extension to set to the file. Specifying <see langword="null"/>,
    /// <seealso cref="string.Empty"/>, or a string only with whitespace will
    /// not add any extension to the file name.
    /// </param>
    /// <returns>The file info of the renamed file.</returns>
    /// <remarks>
    /// Consider using <seealso cref="RenameWithoutExtension(FileInfo, string)"/>
    /// to preserve the extension, or <seealso cref="ChangeExtension(FileInfo, string)"/>
    /// to only change the extension of the file.
    /// </remarks>
    public static FileInfo Rename(this FileInfo file, string name, string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            return file.Rename(name);

        return file.Rename($"{name}.{extension}");
    }

    /// <summary>
    /// Removes the extension of a file, preserving its name.
    /// </summary>
    /// <param name="file">The file whose extension to remove.</param>
    /// <returns>The file with the removed extension.</returns>
    public static FileInfo RemoveExtension(this FileInfo file)
    {
        var extensionlessName = Path.GetFileNameWithoutExtension(file.Name);
        return Rename(file, extensionlessName);
    }
    /// <summary>
    /// Renames a file, preserving its extension.
    /// </summary>
    /// <param name="file">The file whose extensionless name to change.</param>
    /// <param name="extensionlessName">
    /// The name of the file without the extension part. Including an
    /// extension part will not adjust the file's extension. For example,
    /// given a file "notes.txt", changing its extensionless name to
    /// "notes.dat" will result in the file being named "notes.dat.txt".
    /// </param>
    /// <returns>The file info of the renamed file.</returns>
    public static FileInfo RenameWithoutExtension(this FileInfo file, string extensionlessName)
    {
        var extension = Path.GetExtension(file.Name);
        return Rename(file, extensionlessName, extension);
    }
    /// <summary>
    /// Changes the extension of a file, preserving its extensionless name.
    /// </summary>
    /// <param name="file">The file whose extension to change.</param>
    /// <param name="extension">
    /// The new extension to set to the file.
    /// </param>
    /// <returns>The file info of the renamed file.</returns>
    public static FileInfo ChangeExtension(this FileInfo file, string extension)
    {
        var extensionlessName = Path.GetFileNameWithoutExtension(file.Name);
        return Rename(file, extensionlessName, extension);
    }
}
