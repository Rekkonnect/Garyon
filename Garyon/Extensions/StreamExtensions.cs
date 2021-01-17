using Garyon.Exceptions;
using System;
using System.IO;
using System.Text;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <see cref="Stream"/> class.</summary>
    public static unsafe class StreamExtensions
    {
        private const string UTF7ObsoletionMessage = "The UTF-7 encoding is insecure and should not be used. Consider using UTF-8 instead.";
        private const string UTF7ObsoletionDiagnosticID = "SYSLIB0001";
        private const string UTF7ObsoletionURLFormat = "https://aka.ms/dotnet-warnings/{0}";

        #region Write at current position
        /// <summary>Writes a <seealso cref="byte"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, byte value) => stream.WriteByte(value);
        /// <summary>Writes a <seealso cref="short"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, short value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <seealso cref="int"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, int value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <seealso cref="long"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, long value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <seealso cref="sbyte"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, sbyte value) => stream.WriteByte(*(byte*)&value);
        /// <summary>Writes a <seealso cref="ushort"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, ushort value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <seealso cref="uint"/> to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void Write(this Stream stream, uint value) => stream.Write(value.GetBytes());
        /// <summary>Writes a <seealso cref="ulong"/> to the stream at the current position.</summary>
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

        /// <summary>Writes a <seealso cref="string"/> encoded in UTF-7 to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
        public static void WriteUTF7String(this Stream stream, string s) => stream.Write(Encoding.UTF7.GetBytes(s));
        /// <summary>Writes a <seealso cref="string"/> encoded in UTF-8 to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF8String(this Stream stream, string s) => stream.Write(Encoding.UTF8.GetBytes(s));
        /// <summary>Writes a <seealso cref="string"/> encoded in Unicode (UTF-16) to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUnicodeString(this Stream stream, string s) => stream.Write(Encoding.Unicode.GetBytes(s));
        /// <summary>Writes a <seealso cref="string"/> encoded in UTF-32 to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF32String(this Stream stream, string s) => stream.Write(Encoding.UTF32.GetBytes(s));
        /// <summary>Writes a <seealso cref="string"/> encoded in ASCII to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteASCIIString(this Stream stream, string s) => stream.Write(Encoding.ASCII.GetBytes(s));
        /// <summary>Writes a <seealso cref="string"/> encoded in a specified encoding to the stream at the current position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="s">The string to write to the stream.</param>
        /// <param name="encoding">The encoding of the string.</param>
        public static void WriteString(this Stream stream, string s, Encoding encoding) => stream.Write(encoding.GetBytes(s));
        #endregion

        #region Write at specified position
        /// <summary>Writes a <seealso cref="byte"/>[] to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="bytes">The bytes to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, byte[] bytes)
        {
            stream.Position = position;
            stream.Write(bytes);
        }
        /// <summary>Writes a <seealso cref="byte"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, byte value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <seealso cref="short"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, short value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes an <seealso cref="int"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, int value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <seealso cref="long"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, long value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <seealso cref="sbyte"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, sbyte value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <seealso cref="ushort"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, ushort value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <seealso cref="uint"/> to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteAt(this Stream stream, long position, uint value)
        {
            stream.Position = position;
            stream.Write(value);
        }
        /// <summary>Writes a <seealso cref="ulong"/> to the stream at the specified position.</summary>
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

        /// <summary>Writes a <seealso cref="string"/> encoded in UTF-7 to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
        public static void WriteUTF7StringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.UTF7);
        }
        /// <summary>Writes a <seealso cref="string"/> encoded in UTF-8 to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF8StringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.UTF8);
        }
        /// <summary>Writes a <seealso cref="string"/> encoded in Unicode (UTF-16) to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUnicodeStringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.Unicode);
        }
        /// <summary>Writes a <seealso cref="string"/> encoded in UTF-32 to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteUTF32StringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.UTF32);
        }
        /// <summary>Writes a <seealso cref="string"/> encoded in ASCII to the stream at the specified position.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="position">The position in the stream to write at.</param>
        /// <param name="s">The string to write to the stream.</param>
        public static void WriteASCIIStringAt(this Stream stream, long position, string s)
        {
            WriteStringAt(stream, position, s, Encoding.ASCII);
        }
        /// <summary>Writes a <seealso cref="string"/> encoded in a specified encoding to the stream at the specified position.</summary>
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

        #region Read at current position
        /// <summary>Reads a <seealso cref="byte"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the first byte of the <seealso cref="int"/> that <seealso cref="Stream.ReadByte()"/> returns. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="byte"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadByte(this Stream stream, out byte value)
        {
            int result = stream.ReadByte();
            value = *(byte*)&result;
            return result != -1;
        }
        /// <summary>Reads a <seealso cref="sbyte"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the first byte of the <seealso cref="int"/> that <seealso cref="Stream.ReadByte()"/> returns. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="sbyte"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadSByte(this Stream stream, out sbyte value)
        {
            int result = stream.ReadByte();
            value = *(sbyte*)&result;
            return result != -1;
        }
        /// <summary>Reads a <seealso cref="short"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="short"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadInt16(this Stream stream, out short value) => TryReadValue(stream, out value);
        /// <summary>Reads a <seealso cref="ushort"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="ushort"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadUInt16(this Stream stream, out ushort value) => TryReadValue(stream, out value);
        /// <summary>Reads an <seealso cref="int"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="int"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadInt32(this Stream stream, out int value) => TryReadValue(stream, out value);
        /// <summary>Reads a <seealso cref="uint"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="uint"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadUInt32(this Stream stream, out uint value) => TryReadValue(stream, out value);
        /// <summary>Reads a <seealso cref="long"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="long"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadInt64(this Stream stream, out long value) => TryReadValue(stream, out value);
        /// <summary>Reads a <seealso cref="ulong"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="ulong"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadUInt64(this Stream stream, out ulong value) => TryReadValue(stream, out value);

        /// <summary>Reads a <typeparamref name="T"/> from the stream at the current position.</summary>
        /// <typeparam name="T">The type of the value to read.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="ushort"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadValue<T>(this Stream stream, out T value)
            where T : unmanaged
        {
            byte[] bytes = new byte[sizeof(T)];
            int read = stream.Read(bytes);
            fixed (byte* b = bytes)
                value = *(T*)b;
            return read == bytes.Length;
        }

        /// <summary>Reads a <seealso cref="sbyte"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static sbyte ReadSByte(this Stream stream)
        {
            bool success = TryReadSByte(stream, out sbyte value);
            if (!success)
                ThrowHelper.Throw<EndOfStreamException>();
            return value;
        }
        /// <summary>Reads a <seealso cref="short"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static short ReadInt16(this Stream stream) => ReadValue<short>(stream);
        /// <summary>Reads a <seealso cref="ushort"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static ushort ReadUInt16(this Stream stream) => ReadValue<ushort>(stream);
        /// <summary>Reads an <seealso cref="int"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static int ReadInt32(this Stream stream) => ReadValue<int>(stream);
        /// <summary>Reads a <seealso cref="uint"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static uint ReadUInt32(this Stream stream) => ReadValue<uint>(stream);
        /// <summary>Reads a <seealso cref="long"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static long ReadInt64(this Stream stream) => ReadValue<long>(stream);
        /// <summary>Reads a <seealso cref="ulong"/> from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static ulong ReadUInt64(this Stream stream) => ReadValue<ulong>(stream);

        /// <summary>Reads a value from the stream at the current position.</summary>
        /// <typeparam name="T">The type of the value to read from the stream.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static T ReadValue<T>(this Stream stream)
            where T : unmanaged
        {
            bool success = TryReadValue(stream, out T value);
            if (!success)
                ThrowHelper.Throw<EndOfStreamException>();
            return value;
        }

        /// <summary>Reads a <seealso cref="string"/> encoded in UTF-7 from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
        public static string ReadUTF7String(this Stream stream, long byteLength) => ReadString(stream, byteLength, Encoding.UTF7);
        /// <summary>Reads a <seealso cref="string"/> encoded in UTF-8 from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadUTF8String(this Stream stream, long byteLength) => ReadString(stream, byteLength, Encoding.UTF8);
        /// <summary>Reads a <seealso cref="string"/> encoded in Unicode (UTF-16) from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadUnicodeString(this Stream stream, long byteLength) => ReadString(stream, byteLength, Encoding.Unicode);
        /// <summary>Reads a <seealso cref="string"/> encoded in UTF-32 from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadUTF32String(this Stream stream, long byteLength) => ReadString(stream, byteLength, Encoding.UTF32);
        /// <summary>Reads a <seealso cref="string"/> encoded in ASCII from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadASCIIString(this Stream stream, long byteLength) => ReadString(stream, byteLength, Encoding.ASCII);

        /// <summary>Reads a <seealso cref="string"/> encoded in a specified encoding from the stream at the current position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <param name="encoding">The encoding of the string.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadString(this Stream stream, long byteLength, Encoding encoding)
        {
            byte[] bytes = new byte[byteLength];
            stream.Read(bytes);
            return encoding.GetString(bytes);
        }
        #endregion

        #region Read at specified position
        /// <summary>Reads a <seealso cref="byte"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the first byte of the <seealso cref="int"/> that <seealso cref="Stream.ReadByte()"/> returns. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="byte"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadByteAt(this Stream stream, long position, out byte value)
        {
            stream.Position = position;
            return TryReadByte(stream, out value);
        }
        /// <summary>Reads a <seealso cref="sbyte"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the first byte of the <seealso cref="int"/> that <seealso cref="Stream.ReadByte()"/> returns. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="sbyte"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadSByteAt(this Stream stream, long position, out sbyte value)
        {
            stream.Position = position;
            return TryReadSByte(stream, out value);
        }
        /// <summary>Reads a <seealso cref="short"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="short"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadInt16At(this Stream stream, long position, out short value) => TryReadValueAt(stream, position, out value);
        /// <summary>Reads a <seealso cref="ushort"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="ushort"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadUInt16At(this Stream stream, long position, out ushort value) => TryReadValueAt(stream, position, out value);
        /// <summary>Reads an <seealso cref="int"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="int"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadInt32At(this Stream stream, long position, out int value) => TryReadValueAt(stream, position, out value);
        /// <summary>Reads a <seealso cref="uint"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="uint"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadUInt32At(this Stream stream, long position, out uint value) => TryReadValueAt(stream, position, out value);
        /// <summary>Reads a <seealso cref="long"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="long"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadInt64At(this Stream stream, long position, out long value) => TryReadValueAt(stream, position, out value);
        /// <summary>Reads a <seealso cref="ulong"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="ulong"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadUInt64At(this Stream stream, long position, out ulong value) => TryReadValueAt(stream, position, out value);

        /// <summary>Reads a <typeparamref name="T"/> from the stream at the specified position.</summary>
        /// <typeparam name="T">The type of the value to read.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="value">The value. Its value is always equal to the buffer after calling the <seealso cref="Stream.Read(Span{byte})"/> function on it. Only use the value if the method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the <seealso cref="ushort"/> was successfully read from the stream, otherwise <see langword="false"/>, if the stream has reached its end.</returns>
        public static bool TryReadValueAt<T>(this Stream stream, long position, out T value)
            where T : unmanaged
        {
            stream.Position = position;
            return TryReadValue(stream, out value);
        }

        /// <summary>Reads a <seealso cref="sbyte"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static sbyte ReadSByteAt(this Stream stream, long position)
        {
            stream.Position = position;
            return ReadSByte(stream);
        }
        /// <summary>Reads a <seealso cref="short"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static short ReadInt16At(this Stream stream, long position) => ReadValueAt<short>(stream, position);
        /// <summary>Reads a <seealso cref="ushort"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static ushort ReadUInt16At(this Stream stream, long position) => ReadValueAt<ushort>(stream, position);
        /// <summary>Reads an <seealso cref="int"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static int ReadInt32At(this Stream stream, long position) => ReadValueAt<int>(stream, position);
        /// <summary>Reads a <seealso cref="uint"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static uint ReadUInt32At(this Stream stream, long position) => ReadValueAt<uint>(stream, position);
        /// <summary>Reads a <seealso cref="long"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static long ReadInt64At(this Stream stream, long position) => ReadValueAt<long>(stream, position);
        /// <summary>Reads a <seealso cref="ulong"/> from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static ulong ReadUInt64At(this Stream stream, long position) => ReadValueAt<ulong>(stream, position);

        /// <summary>Reads a value from the stream at the specified position.</summary>
        /// <typeparam name="T">The type of the value to read from the stream.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <returns>The read value from the stream.</returns>
        /// <exception cref="EndOfStreamException">Thrown when the stream would not provide enough bytes to read.</exception>
        public static T ReadValueAt<T>(this Stream stream, long position)
            where T : unmanaged
        {
            stream.Position = position;
            return ReadValue<T>(stream);
        }

        /// <summary>Reads a <seealso cref="string"/> encoded in UTF-7 from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        [Obsolete(UTF7ObsoletionMessage, DiagnosticId = UTF7ObsoletionDiagnosticID, UrlFormat = UTF7ObsoletionURLFormat)]
        public static string ReadUTF7StringAt(this Stream stream, long position, long byteLength) => ReadStringAt(stream, position, byteLength, Encoding.UTF7);
        /// <summary>Reads a <seealso cref="string"/> encoded in UTF-8 from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadUTF8StringAt(this Stream stream, long position, long byteLength) => ReadStringAt(stream, position, byteLength, Encoding.UTF8);
        /// <summary>Reads a <seealso cref="string"/> encoded in Unicode (UTF-16) from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadUnicodeStringAt(this Stream stream, long position, long byteLength) => ReadStringAt(stream, position, byteLength, Encoding.Unicode);
        /// <summary>Reads a <seealso cref="string"/> encoded in UTF-32 from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadUTF32StringAt(this Stream stream, long position, long byteLength) => ReadStringAt(stream, position, byteLength, Encoding.UTF32);
        /// <summary>Reads a <seealso cref="string"/> encoded in ASCII from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadASCIIStringAt(this Stream stream, long position, long byteLength) => ReadStringAt(stream, position, byteLength, Encoding.ASCII);

        /// <summary>Reads a <seealso cref="string"/> encoded in a specified encoding from the stream at the specified position.</summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="position">The position in the stream to read from.</param>
        /// <param name="byteLength">The number of bytes to read. This does not necessarily equal the number of characters the resulting <seealso cref="string"/> contains.</param>
        /// <param name="encoding">The encoding of the string.</param>
        /// <returns>The read <seealso cref="string"/> from the stream in the provided encoding.</returns>
        public static string ReadStringAt(this Stream stream, long position, long byteLength, Encoding encoding)
        {
            stream.Position = position;
            return ReadString(stream, byteLength, encoding);
        }
        #endregion
    }
}
