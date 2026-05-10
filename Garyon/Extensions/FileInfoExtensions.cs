using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            if (file.Directory is var directory and not null)
            {
                return directory.Depth() + 1;
            }

            return 0;
        }

        public DirectoryInfo? RecursiveParent(int levels = 1)
        {
            return file.Directory?.RecursiveParent(levels);
        }
        public DirectoryInfo? BoundRecursiveParent(int levels = 1)
        {
            return file.Directory?.BoundRecursiveParent(levels);
        }

        public void CopyTo(FileInfo target, bool overwrite = false)
        {
            file.CopyTo(target.FullName, overwrite);
        }

        /// <summary>
        /// Moves the file to a parent directory, up to a number of levels. If
        /// the levels are more than the current depth of the file, an exception
        /// will be thrown.
        /// </summary>
        /// <param name="levels">
        /// The number of levels to move up.
        /// </param>
        /// <returns>
        /// The <seealso cref="FileInfo"/> that was moved, containing its new
        /// information after it was moved.
        /// </returns>
        /// <remarks>
        /// Consider using <seealso cref="MoveUpBound(FileInfo, int)"/> if the
        /// depth of the file is not meant to be at least the provided.
        /// </remarks>
        public FileInfo MoveUp(int levels = 1)
        {
            var newDirectory = file.RecursiveParent(levels)
                ?? throw new InvalidOperationException("The requested file's depth is less than the number of levels to move it up by.");
            return file.MoveToDirectory(newDirectory);
        }
        /// <summary>
        /// Moves the file to a parent directory, up to a number of levels. If
        /// the levels are more than the current depth of the file, it will be
        /// moved to the root of the drive.
        /// </summary>
        /// <param name="levels">
        /// The number of levels to move up.
        /// </param>
        /// <returns>
        /// The <seealso cref="FileInfo"/> that was moved, containing its new
        /// information after it was moved.
        /// </returns>
        public FileInfo MoveUpBound(int levels = 1)
        {
            var newDirectory = file.BoundRecursiveParent(levels);
            if (newDirectory is null)
            {
                return file;
            }

            return file.MoveToDirectory(newDirectory);
        }
        /// <summary>
        /// Moves the file to another directory, preserving its file name.
        /// </summary>
        /// <param name="directory">
        /// The new directory to move the file to.
        /// </param>
        /// <returns>
        /// The file info of the moved file.
        /// </returns>
        public FileInfo MoveToDirectory(DirectoryInfo directory)
        {
            return MoveTo(file, directory, file.Name);
        }

        /// <summary>
        /// Moves the file to another directory, and changes its name and
        /// extension.
        /// </summary>
        /// <param name="directory">
        /// The new directory that will contain the file.
        /// </param>
        /// <param name="name">
        /// The new name of the file without the extension.
        /// </param>
        /// <param name="extension">
        /// The new extension of the file.
        /// </param>
        /// <returns>
        /// The file info of the moved file.
        /// </returns>
        /// <remarks>
        /// This method only uses one IO operation, effectively changing the
        /// absolute file path.
        /// </remarks>
        public FileInfo MoveTo(DirectoryInfo directory, string name, string extension)
        {
            return file.MoveTo(directory, $"{name}.{extension}");
        }
        /// <param name="nameWithExtension">
        /// The new name of the file, including its extension.
        /// </param>
        /// <inheritdoc cref="MoveTo(FileInfo, DirectoryInfo, string, string)"/>
        public FileInfo MoveTo(DirectoryInfo directory, string nameWithExtension)
        {
            file.MoveTo(Path.Combine(directory.FullName, nameWithExtension));
            return file;
        }

        /// <summary>
        /// Renames a file, changing both its name and extension.
        /// </summary>
        /// <param name="nameWithExtension">
        /// The new name to set, including its extension.
        /// </param>
        /// <returns>
        /// The file info of the renamed file.
        /// </returns>
        /// <remarks>
        /// Consider using
        /// <seealso cref="RenameWithoutExtension(FileInfo, string)"/> to
        /// preserve the extension, or
        /// <seealso cref="ChangeExtension(FileInfo, string)"/> to only change
        /// the extension of the file.
        /// </remarks>
        public FileInfo Rename(string nameWithExtension)
        {
            return MoveTo(file, file.Directory!, nameWithExtension);
        }
        /// <summary>
        /// Renames a file, changing both its name and extension.
        /// </summary>
        /// <param name="name">
        /// The new name to set to the file, excluding its extension.
        /// </param>
        /// <param name="extension">
        /// The new extension to set to the file. Specifying
        /// <see langword="null"/>, <seealso cref="string.Empty"/>, or a string
        /// only with whitespace will not add any extension to the file name.
        /// </param>
        /// <returns>
        /// The file info of the renamed file.
        /// </returns>
        /// <remarks>
        /// Consider using
        /// <seealso cref="RenameWithoutExtension(FileInfo, string)"/> to
        /// preserve the extension, or
        /// <seealso cref="ChangeExtension(FileInfo, string)"/> to only change
        /// the extension of the file.
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
        /// <returns>
        /// The file with the removed extension.
        /// </returns>
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
        /// <returns>
        /// The file info of the renamed file.
        /// </returns>
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
        /// <returns>
        /// The file info of the renamed file.
        /// </returns>
        public FileInfo ChangeExtension(string extension)
        {
            var extensionlessName = Path.GetFileNameWithoutExtension(file.Name);
            return Rename(file, extensionlessName, extension);
        }

        /// <summary>
        /// Reads all bytes using <see cref="File.ReadAllBytes(string)"/>.
        /// </summary>
        public byte[] ReadAllBytes()
        {
            return File.ReadAllBytes(file.FullName);
        }

        /// <summary>
        /// Reads all text using <see cref="File.ReadAllText(string)"/>.
        /// </summary>
        public string ReadAllText()
        {
            return File.ReadAllText(file.FullName);
        }

        /// <summary>
        /// Reads all lines using <see cref="File.ReadAllLines(string)"/>.
        /// </summary>
        public string[] ReadAllLines()
        {
            return File.ReadAllLines(file.FullName);
        }

        /// <summary>
        /// Writes all bytes using
        /// <see cref="File.WriteAllBytes(string, byte[])"/>.
        /// </summary>
        public void WriteAllBytes(byte[] bytes)
        {
            File.WriteAllBytes(file.FullName, bytes);
        }

#if HAS_SPAN_BASED_FILE_IO
        /// <summary>
        /// Writes all bytes using <see cref="File.WriteAllBytes(string, ReadOnlySpan{byte})"/>.
        /// </summary>
        public void WriteAllBytes(ReadOnlySpan<byte> bytes)
        {
            File.WriteAllBytes(file.FullName, bytes);
        }
#endif

        /// <summary>
        /// Writes all text using
        /// <see cref="File.WriteAllText(string, string)"/>.
        /// </summary>
        public void WriteAllText(string content)
        {
            File.WriteAllText(file.FullName, content);
        }

        /// <summary>
        /// Writes all text using
        /// <see cref="File.WriteAllText(string, string, Encoding)"/>.
        /// </summary>
        public void WriteAllText(string content, Encoding encoding)
        {
            File.WriteAllText(file.FullName, content, encoding);
        }

#if HAS_SPAN_BASED_FILE_IO
        /// <summary>
        /// Writes all text using <see cref="File.WriteAllText(string, ReadOnlySpan{char})"/>.
        /// </summary>
        public void WriteAllText(ReadOnlySpan<char> content)
        {
            File.WriteAllText(file.FullName, content);
        }

        /// <summary>
        /// Writes all text using <see cref="File.WriteAllText(string, ReadOnlySpan{char}, Encoding)"/>.
        /// </summary>
        public void WriteAllText(ReadOnlySpan<char> content, Encoding encoding)
        {
            File.WriteAllText(file.FullName, content, encoding);
        }
#endif

        /// <summary>
        /// Writes all lines using
        /// <see cref="File.WriteAllLines(string, string[])"/>.
        /// </summary>
        public void WriteAllLines(string[] lines)
        {
            File.WriteAllLines(file.FullName, lines);
        }

        /// <summary>
        /// Writes all lines using
        /// <see cref="File.WriteAllLines(string, string[], Encoding)"/>.
        /// </summary>
        public void WriteAllLines(string[] lines, Encoding encoding)
        {
            File.WriteAllLines(file.FullName, lines, encoding);
        }

        /// <summary>
        /// Writes all lines using
        /// <see cref="File.WriteAllLines(string, IEnumerable{string})"/>.
        /// </summary>
        public void WriteAllLines(IEnumerable<string> lines)
        {
            File.WriteAllLines(file.FullName, lines);
        }

        /// <summary>
        /// Writes all lines using
        /// <see cref="File.WriteAllLines(string, IEnumerable{string}, Encoding)"/>.
        /// </summary>
        public void WriteAllLines(IEnumerable<string> lines, Encoding encoding)
        {
            File.WriteAllLines(file.FullName, lines, encoding);
        }

#if HAS_ASYNC_FILE_IO
        /// <summary>
        /// Reads all bytes using <see cref="File.ReadAllBytesAsync(string, CancellationToken)"/>.
        /// </summary>
        public async Task<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken = default)
        {
            return await File.ReadAllBytesAsync(file.FullName, cancellationToken);
        }

        /// <summary>
        /// Reads all text using <see cref="File.ReadAllTextAsync(string, CancellationToken)"/>.
        /// </summary>
        public async Task<string> ReadAllTextAsync(CancellationToken cancellationToken = default)
        {
            return await File.ReadAllTextAsync(file.FullName, cancellationToken);
        }

        /// <summary>
        /// Reads all lines using <see cref="File.ReadAllLinesAsync(string, CancellationToken)"/>.
        /// </summary>
        public async Task<string[]> ReadAllLinesAsync(CancellationToken cancellationToken = default)
        {
            return await File.ReadAllLinesAsync(file.FullName, cancellationToken);
        }

        /// <summary>
        /// Writes all bytes using <see cref="File.WriteAllBytesAsync(string, byte[], CancellationToken)"/>.
        /// </summary>
        public async Task WriteAllBytesAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            await File.WriteAllBytesAsync(file.FullName, bytes, cancellationToken);
        }

#if HAS_SPAN_BASED_FILE_IO
        /// <summary>
        /// Writes all bytes using <see cref="File.WriteAllBytesAsync(string, ReadOnlyMemory{byte}, CancellationToken)"/>.
        /// </summary>
        public async Task WriteAllBytesAsync(ReadOnlyMemory<byte> bytes, CancellationToken cancellationToken = default)
        {
            await File.WriteAllBytesAsync(file.FullName, bytes, cancellationToken);
        }
#endif

        /// <summary>
        /// Writes all text using <see cref="File.WriteAllTextAsync(string, string, CancellationToken)"/>.
        /// </summary>
        public async Task WriteAllTextAsync(string content, CancellationToken cancellationToken = default)
        {
            await File.WriteAllTextAsync(file.FullName, content, cancellationToken);
        }

        /// <summary>
        /// Writes all text using <see cref="File.WriteAllTextAsync(string, string, Encoding, CancellationToken)"/>.
        /// </summary>
        public async Task WriteAllTextAsync(
            string content,
            Encoding encoding,
            CancellationToken cancellationToken = default)
        {
            await File.WriteAllTextAsync(file.FullName, content, encoding, cancellationToken);
        }

#if HAS_SPAN_BASED_FILE_IO
        /// <summary>
        /// Writes all text using <see cref="File.WriteAllTextAsync(string, ReadOnlyMemory{char}, CancellationToken)"/>.
        /// </summary>
        public async Task WriteAllTextAsync(
            ReadOnlyMemory<char> content,
            CancellationToken cancellationToken = default)
        {
            await File.WriteAllTextAsync(file.FullName, content, cancellationToken);
        }

        /// <summary>
        /// Writes all text using <see cref="File.WriteAllTextAsync(string, ReadOnlyMemory{char}, Encoding, CancellationToken)"/>.
        /// </summary>
        public async Task WriteAllTextAsync(
            ReadOnlyMemory<char> content,
            Encoding encoding,
            CancellationToken cancellationToken = default)
        {
            await File.WriteAllTextAsync(file.FullName, content, encoding, cancellationToken);
        }
#endif

        /// <summary>
        /// Writes all lines using <see cref="File.WriteAllLinesAsync(string, IEnumerable{string}, CancellationToken)"/>.
        /// </summary>
        public async Task WriteAllLinesAsync(
            IEnumerable<string> lines,
            CancellationToken cancellationToken = default)
        {
            await File.WriteAllLinesAsync(file.FullName, lines, cancellationToken);
        }

        /// <summary>
        /// Writes all lines using <see cref="File.WriteAllLinesAsync(string, IEnumerable{string}, Encoding, CancellationToken)"/>.
        /// </summary>
        public async Task WriteAllLinesAsync(
            IEnumerable<string> lines,
            Encoding encoding,
            CancellationToken cancellationToken = default)
        {
            await File.WriteAllLinesAsync(file.FullName, lines, encoding, cancellationToken);
        }
#endif
    }
}
