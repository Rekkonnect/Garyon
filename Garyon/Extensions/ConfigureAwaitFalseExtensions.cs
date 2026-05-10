using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Garyon.Extensions;

[ExcludeFromCodeCoverage]
public static class ConfigureAwaitFalseExtensions
{
    extension(Task task)
    {
        public ConfiguredTaskAwaitable NoContext => task.ConfigureAwait(false);
    }

    extension<T>(Task<T> task)
    {
        public ConfiguredTaskAwaitable<T> NoContext => task.ConfigureAwait(false);
    }

    extension(ValueTask task)
    {
        public ConfiguredValueTaskAwaitable NoContext => task.ConfigureAwait(false);
    }

    extension<T>(ValueTask<T> task)
    {
        public ConfiguredValueTaskAwaitable<T> NoContext => task.ConfigureAwait(false);
    }
}
