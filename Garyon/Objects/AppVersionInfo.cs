using Garyon.Extensions;
using Garyon.Mathematics;
using System;
using System.Reflection;

namespace Garyon.Objects;

/// <summary>
/// Provides version information about the application, usually parsed from
/// <see cref="AssemblyInformationalVersionAttribute"/>.
/// </summary>
/// <param name="Version">
/// The main part of the informational version. Assuming a version like
/// '1.2.3-preview', the main version is the '1.2.3' part.
/// </param>
/// <param name="PreviewIndicator">
/// The optional preview indicator that follows the version string. Assuming a
/// version like '1.2.3-preview', the preview indicator is the 'preview' part.
/// </param>
/// <param name="CommitSha">
/// The SHA of the commit that built the application, if available.
/// </param>
public sealed record AppVersionInfo(
    string Version,
    string? PreviewIndicator,
    string? CommitSha)
{
    private AppVersionInfo(
        ReadOnlySpan<char> version,
        ReadOnlySpan<char> previewIndicator,
        ReadOnlySpan<char> commitSha)
        : this(
              version.ToString(),
              previewIndicator.ToStringOrNull(),
              commitSha.ToStringOrNull()) { }

    public static AppVersionInfo Parse(AssemblyInformationalVersionAttribute attribute)
    {
        return Parse(attribute.InformationalVersion);
    }

    public static AppVersionInfo Parse(string versionString)
    {
        versionString.AsSpan().SplitOnce('+', out var left, out var commitSha);
        left.SplitOnce('-', out var realVersion, out var previewIndicator);
        return new(realVersion, previewIndicator, commitSha);
    }

    public static AppVersionInfo? InformationalVersionForAssembly(Assembly assembly)
    {
        var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        return attribute?.Apply(Parse);
    }
}
