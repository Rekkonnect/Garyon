using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace Garyon.Extensions;

/// <summary>
/// Contains extensions for the <seealso cref="FileSystemInfo"/> class.
/// </summary>
public static class FileSystemInfoExtensions
{
    extension(FileSystemInfo info)
    {
        /// <summary>
        /// Gets the parent directory containing this file system object, either
        /// the containing directory of the file, or the parent directory of the
        /// given directory.
        /// </summary>
        /// <returns>
        /// The <seealso cref="DirectoryInfo"/> instance of the parent of the
        /// given file system object.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the given <see cref="FileSystemInfo"/> is not
        /// <see cref="DirectoryInfo"/> or <see cref="FileInfo"/>.
        /// </exception>
        public DirectoryInfo? Parent => info switch
        {
            DirectoryInfo directory => directory.Parent,
            FileInfo file => file.Directory,
            _ => ThrowInvalidFileSystemInfo<DirectoryInfo?>(),
        };

        public DirectoryInfo? RecursiveParent(int levels)
        {
            return info switch
            {
                DirectoryInfo directory => directory.RecursiveParent(levels),
                FileInfo file => file.RecursiveParent(levels),
                _ => ThrowInvalidFileSystemInfo<DirectoryInfo?>(),
            };
        }

        public DirectoryInfo? BoundRecursiveParent(int levels)
        {
            return info switch
            {
                DirectoryInfo directory => directory.BoundRecursiveParent(levels),
                FileInfo file => file.BoundRecursiveParent(levels),
                _ => ThrowInvalidFileSystemInfo<DirectoryInfo?>(),
            };
        }

        /// <summary>
        /// Gets the depth of this file system object, meaning the number of
        /// directories between the root and this object.
        /// </summary>
        /// <returns>
        /// The number of parent directories that must be traversed before
        /// reaching the file system root.
        /// </returns>
        public int Depth()
        {
            return info switch
            {
                DirectoryInfo dir => dir.Depth(),
                FileInfo file => file.Depth(),
                _ => ThrowInvalidFileSystemInfo<int>(),
            };
        }

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static T ThrowInvalidFileSystemInfo<T>()
        {
            throw new ArgumentException("Unknown file system info object type", nameof(info));
        }
    }
}
