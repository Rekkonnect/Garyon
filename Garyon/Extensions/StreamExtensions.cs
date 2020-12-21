using System;
using System.IO;
using System.Text;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <see cref="Stream"/> class.</summary>
    public static unsafe class StreamExtensions
    {
        #region Write at current position
        /// <summary>Writes a <see langword="byte"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, byte value) => stream.WriteByte(value);
        /// <summary>Writes a <see langword="short"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, short value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <see langword="int"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, int value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <see langword="long"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, long value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <see langword="sbyte"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, sbyte value) => stream.WriteByte(*(byte*)&value);
        /// <summary>Writes a <see langword="ushort"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, ushort value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <see langword="uint"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, uint value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <see langword="ulong"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, ulong value) => stream.Write(value.GetBytes());

        /// <summary>Writes a value to the stream at the current position.</summary>
        /// <typeparam name="T">The type of the value to write to the stream.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write<T>(this Stream stream, T value)
            where T : unmanaged
        {
            stream.Write(value.GetBytes());
        }

        /// <summary>Writes a <see langword="string"/> encoded in UTF-7 to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF7String(this Stream stream, string s) => stream.Write(Encoding.UTF7.GetBytes(s));
        /// <summary>Writes a <see langword="string"/> encoded in UTF-8 to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF8String(this Stream stream, string s) => stream.Write(Encoding.UTF8.GetBytes(s));
        /// <summary>Writes a <see langword="string"/> encoded in Unicode to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUnicodeString(this Stream stream, string s) => stream.Write(Encoding.Unicode.GetBytes(s));
        /// <summary>Writes a <see langword="string"/> encoded in UTF-32 to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF32String(this Stream stream, string s) => stream.Write(Encoding.UTF32.GetBytes(s));
        /// <summary>Writes a <see langword="string"/> encoded in ASCII to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteASCIIString(this Stream stream, string s) => stream.Write(Encoding.ASCII.GetBytes(s));
        /// <summary>Writes a <see langword="string"/> encoded in a specified encoding to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteString(this Stream stream, string s, Encoding encoding) => stream.Write(encoding.GetBytes(s));
        #endregion

        #region Write at specified position
        /// <summary>Writes a <see langword="byte"/>[] to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="bytes">The bytes to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, byte[] bytes)
        {
            stream.Position = position;
            stream.Write(bytes);
        }
        /// <summary>Writes a <see langword="byte"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, byte value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <see langword="short"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, short value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes an <see langword="int"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, int value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <see langword="long"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, long value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <see langword="sbyte"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, sbyte value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <see langword="ushort"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, ushort value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <see langword="uint"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, uint value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <see langword="ulong"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, ulong value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a value to the stream at the specified position.</summary>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt<T>(this Stream stream, long position, T value)
            where T : unmanaged
        {
            stream.Position = position;
            stream.Write(value);
        }

        /// <summary>Writes a <see langword="string"/> encoded in UTF-7 to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF7StringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.UTF7);
        }
        /// <summary>Writes a <see langword="string"/> encoded in UTF-8 to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF8StringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.UTF8);
        }
        /// <summary>Writes a <see langword="string"/> encoded in Unicode to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUnicodeStringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.Unicode);
        }
        /// <summary>Writes a <see langword="string"/> encoded in UTF-32 to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF32StringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.UTF32);
        }
        /// <summary>Writes a <see langword="string"/> encoded in ASCII to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteASCIIStringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.ASCII);
        }
        /// <summary>Writes a <see langword="string"/> encoded in a specified encoding to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        /// <param name="encoding">The encoding of the string.</param>
        public static void WriteStringAt(this Stream stream, long position, string s, Encoding encoding)
        {
            stream.Position = position;
            stream.WriteString(s, encoding);
        }
        #endregion

        // Add more functions for reading
        #region Read
        public static int ReadInt32(this Stream stream)
        {
            byte[] bytes = new byte[sizeof(int)];
            stream.Read(bytes);
            return BitConverter.ToInt32(bytes);
        }
        public static long ReadInt64(this Stream stream)
        {
            byte[] bytes = new byte[sizeof(long)];
            stream.Read(bytes);
            return BitConverter.ToInt64(bytes);
        }

        public static string ReadUTF8String(this Stream stream, long length) => ReadString(stream, length, Encoding.UTF8);

        /// <summary>Reads a <see langword="string"/> encoded in a specified encoding from the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The encoding of the string.</param>
        /// <returns>The read string in the provided encoding.</returns>
        public static string ReadString(this Stream stream, long length, Encoding encoding)
        {
            byte[] bytes = new byte[length];
            stream.Read(bytes);
            return encoding.GetString(bytes);
        }
        #endregion
    }
}
