using System.Text;

namespace Garyon.Extensions;

public static class EncodingExtensions
{
    extension(Encoding)
    {
        public static Encoding Utf8WithoutBom
            => new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

#if HAS_CODE_PAGES_ENCODING_PROVIDER
        public static Encoding? Windows1250
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1250");

        public static Encoding? Windows1251
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1251");

        public static Encoding? Windows1252
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1252");

        public static Encoding? Windows1253
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1253");

        public static Encoding? Windows1254
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1254");

        public static Encoding? Windows1255
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1255");

        public static Encoding? Windows1256
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1256");

        public static Encoding? Windows1257
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1257");

        public static Encoding? Windows1258
            => CodePagesEncodingProvider.Instance.GetEncoding("windows-1258");
#endif
    }
}
