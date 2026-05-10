using Garyon.FrameworkCapabilityGenerator.NetVersioning;

namespace Garyon.FrameworkCapabilityGenerator.FrameworkCapabilities;

internal static class KnownFrameworkCapabilities
{
    // As polyfills were introduced, some of these capabilities are not being evaluated
    // to conditionally define helpful extensions, extending them to more versions than
    // the official framework versions support.
    // They are defined here for reference and documentation purposes.

    public static IReadOnlyList<FrameworkCapabilityRange> Capabilities =>
        [
            new(
                Range(KnownFrameworkVersions.NetStandard21, null)
                | Range(KnownFrameworkVersions.NetCore31, null),
                [
                    "HAS_VALUE_TASK",
                    "HAS_SPAN",
                    "HAS_SLICES",
                    "HAS_HASH_CODE",
                    "HAS_MATHF",
                    "HAS_INTERFACE_DIMS",
                    "HAS_ASYNC_ENUMERABLE",
                    "HAS_NULLABLE_ANNOTATION_ATTRIBUTES",
                    "KNOWS_GENERIC_PARAMETER_TYPES_IN_REFLECTION",
                    "HAS_BYREF_LIKE",
                    "HAS_STRING_STARTSWITH_ENDSWITH_CHAR",
                    "HAS_HASHSET_CAPACITY_CTOR",
                    "HAS_DICTIONARY_TRYADD",
                    "HAS_ARRAY_FILL",
                    "HAS_DICTIONARY_KVPS_CTOR",
                    "HAS_TO_HASHSET",
                    "HAS_STREAM_READ_SIMPLE_BYTES",
                    "HAS_GENERIC_ENUM_PARSE",
                    "HAS_STRING_JOIN_CHAR",
                    "HAS_KVP_DECONSTRUCT",
                    "HAS_CONCURRENT_BAG_CLEAR",
                    "HAS_ASYNC_FILE_IO",
                    "HAS_DATA_ROW_FIELD_METHOD",
                    "HAS_NUMBER_PARSE_SPANSTRING",
                ]),

            new(
                Range(null, KnownFrameworkVersions.NetStandard21)
                | Range(null, KnownFrameworkVersions.Net5),
                [
                    "REQUIRES_IEQUATABLE_FOR_SPAN_SEQUENCE_EQUALS",
                ]),

            new(
                Range(KnownFrameworkVersions.NetCore31, null),
                [
                    "HAS_IMMUTABLE",
                    "HAS_CALLER_INFORMATION_ATTRIBUTES",
                    "HAS_INTRINSICS",
                    "HAS_UNSAFE",
                    "HAS_SIMD",
                    "HAS_BIT_OPERATIONS",
                    "HAS_CHANNELS",
                    "HAS_GET_CHUNKS",
                    "HAS_AVX",
                    "HAS_DYNAMIC_INVOCATION",
                    "HAS_MATH_LOG2",
                    "HAS_MATH_ILOGB",
                    "HAS_CODE_PAGES_ENCODING_PROVIDER",
                ]),

            new(
                Range(KnownFrameworkVersions.Net5, null),
                [
                    "HAS_SUPPORTED_OS_PLATFORM",
                    "HAS_GENERIC_ENUM_ISDEFINED",
                    "HAS_EXTERNAL_INIT",
                    "HAS_VECTOR_ALLBITS",
                    "HAS_MORE_OBSOLETE_PARAMS",
                    "SUPPORTS_COVARIANT_OVERRIDES",
                    "HAS_READONLY_SET",
                    "SUPPORTS_UNMANAGED_FUNPTRS",
                    "HAS_GENERIC_ENUM_GETVALUES",
                ]),

            new(
                Range(KnownFrameworkVersions.Net6, null),
                [
                    "HAS_DATEONLY_TIMEONLY",
                    "HAS_AGGREGATE_LINQ_BY",
                    "HAS_ENUM_PARSE_SPANSTRING",
                    "HAS_STRINGSPAN_ENUMERATE_LINES",
                    "HAS_RANDOM_NEXTINT64_NEXTSINGLE",
                    "HAS_TRY_GET_NON_ENUMERATED_COUNT",
                ]),

            new(
                Range(KnownFrameworkVersions.Net7, null),
                [
                    "HAS_UNREACHABLE_EXCEPTION",
                    "HAS_INUMBER",
                    "HAS_STATIC_ABSTRACTS",
                ]),

            new(
                Range(KnownFrameworkVersions.Net9, null),
                [
                    "HAS_INTERLOCKED_COMPARE_EXCHANGE_UNCONSTRAINED",
                    "HAS_SPAN_BASED_FILE_IO",
                ]),

            new(
                Range(KnownFrameworkVersions.Net10, null),
                [
                    "ALLOWS_REF_STRUCTS",
                ]),
        ];

    private static FrameworkVersionRange Range(BaseFrameworkVersion? lower, BaseFrameworkVersion? upper)
        => FrameworkVersionRange.Create(lower, upper);
}
