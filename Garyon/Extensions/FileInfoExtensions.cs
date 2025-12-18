using System;
using System.IO;

namespace Garyon.Extensions;

/// <summary>
/// Contains extensions for the <seealso cref="FileInfo"/> class.
/// </summary>
public static class FileInfoExtensions
{
    extension(FileInfo file)
    {
        /// <summary>
        /// Gets the depth of this file, meaning the number of directories
        /// between the root and this file.
        /// </summary>
        /// <returns>
        /// The number of parent directories that must be traversed before
        /// reaching the file system root.
        /// </returns>
        public int Depth()
        {
            return file.Directory.Depth() + 1;
        }

        public DirectoryInfo? RecursiveParent(int levels = 1)
        {
            return file.Directory.RecursiveParent(levels);
        }
        public DirectoryInfo BoundRecursiveParent(int levels = 1)
        {
            return file.Directory.BoundRecursiveParent(levels);
        }

        /// <summary>
        /// Moves the file to a parent directory, up to a number of levels.
        /// If the levels are more than the current depth of the file, an
        /// exception will be thrown.
        /// </summary>
        /// <param name="levels">The number of levels to move up.</param>
        /// <returns>
        /// The <seealso cref="FileInfo"/> that was moved, containing its
        /// new information after it was moved.
        /// </returns>
        /// <remarks>
        /// Consider using <seealso cref="MoveUpBound(FileInfo, int)"/> if
        /// the depth of the file is not meant to be at least the provided.
        /// </remarks>
        public FileInfo MoveUp(int levels = 1)
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
        /// <param name="levels">The number of levels to move up.</param>
        /// <returns>
        /// The <seealso cref="FileInfo"/> that was moved, containing its
        /// new information after it was moved.
        /// </returns>
        public FileInfo MoveUpBound(int levels = 1)
        {
            var newDirectory = file.BoundRecursiveParent(levels);
            return file.MoveToDirectory(newDirectory);
        }
        /// <summary>
        /// Moves the file to another directory, preserving its file name.
        /// </summary>
        /// <param name="directory">The new directory to move the file to.</param>
        /// <returns>The file info of the moved file.</returns>
        public FileInfo MoveToDirectory(DirectoryInfo directory)
        {
            return MoveTo(file, directory, file.Name);
        }

        /// <summary>
        /// Moves the file to another directory, and changes its name
        /// and extension.
        /// </summary>
        /// <param name="directory">The new directory that will contain the file.</param>
        /// <param name="name">The new name of the file without the extension.</param>
        /// <param name="extension">The new extension of the file.</param>
        /// <returns>The file info of the moved file.</returns>
        /// <remarks>
        /// This method only uses one IO operation, effectively changing
        /// the absolute file path.
        /// </remarks>
        public FileInfo MoveTo(DirectoryInfo directory, string name, string extension)
        {
            return file.MoveTo(directory, $"{name}.{extension}");
        }
        /// <param name="nameWithExtension">
        /// The new name of the file, including its extension.
        /// </param>
        /// <inheritdoc cref="MoveTo(FileInfo, DirectoryInfo, string, string)"/>
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        public FileInfo MoveTo(DirectoryInfo directory, string nameWithExtension)
        {
            file.MoveTo(Path.Combine(directory.Name!, nameWithExtension));
            return file;
        }
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)

        /// <summary>Renames a file, changing both its name and extension.</summary>
        /// <param name="nameWithExtension">The new name to set, including its extension.</param>
        /// <returns>The file info of the renamed file.</returns>
        /// <remarks>
        /// Consider using <seealso cref="RenameWithoutExtension(FileInfo, string)"/>
        /// to preserve the extension, or <seealso cref="ChangeExtension(FileInfo, string)"/>
        /// to only change the extension of the file.
        /// </remarks>
        public FileInfo Rename(string nameWithExtension)
        {
            return MoveTo(file, file.Directory!, nameWithExtension);
        }
        /// <summary>Renames a file, changing both its name and extension.</summary>
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
        public FileInfo Rename(string name, string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return file.Rename(name);

            return file.Rename($"{name}.{extension}");
        }

        /// <summary>
        /// Removes the extension of a file, preserving its name.
        /// </summary>
        /// <returns>The file with the removed extension.</returns>
        public FileInfo RemoveExtension()
        {
            var extensionlessName = Path.GetFileNameWithoutExtension(file.Name);
            return Rename(file, extensionlessName);
        }
        /// <summary>
        /// Renames a file, preserving its extension.
        /// </summary>
        /// <param name="extensionlessName">
        /// The name of the file without the extension part. Including an
        /// extension part will not adjust the file's extension. For example,
        /// given a file "notes.txt", changing its extensionless name to
        /// "notes.dat" will result in the file being named "notes.dat.txt".
        /// </param>
        /// <returns>The file info of the renamed file.</returns>
        public FileInfo RenameWithoutExtension(string extensionlessName)
        {
            var extension = Path.GetExtension(file.Name);
            return Rename(file, extensionlessName, extension);
        }
        /// <summary>
        /// Changes the extension of a file, preserving its extensionless name.
        /// </summary>
        /// <param name="extension">
        /// The new extension to set to the file.
        /// </param>
        /// <returns>The file info of the renamed file.</returns>
        public FileInfo ChangeExtension(string extension)
        {
            var extensionlessName = Path.GetFileNameWithoutExtension(file.Name);
            return Rename(file, extensionlessName, extension);
        }
    }
}
