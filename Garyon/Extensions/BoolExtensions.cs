using System;

namespace Garyon.Extensions;

/// <summary>Provides extensions for <seealso cref="bool"/>.</summary>
public static class BoolExtensions
{
    extension(bool value)
    {
        public bool Not => !value;

        public int ToInt32()
        {
            return value ? 1 : 0;
        }

        public int ToInt64()
        {
            return value ? 1 : 0;
        }

        public T SwitchValue<T>(T whenTrue, T whenFalse)
        {
            return value ? whenTrue : whenFalse;
        }

        public void SwitchInvoke(Action whenTrue, Action whenFalse)
        {
            var action = value.SwitchValue(whenTrue, whenFalse);
            action();
        }

        public T SwitchInvoke<T>(Func<T> whenTrue, Func<T> whenFalse)
        {
            var func = value.SwitchValue(whenTrue, whenFalse);
            return func();
        }

        /// <summary>
        /// Gets either the value if the bool is <see langword="true"/>,
        /// or <see langword="null"/> if the bool is <see langword="false"/>.
        /// </summary>
        /// <param name="whenTrue">
        /// The value to return if the bool is <see langword="true"/>,
        /// otherwise <see langword="null"/>.
        /// </param>
        /// <remarks>
        /// This method is only valid for structs. For all other types,
        /// or to return <see langword="default"/> instead, use
        /// <see cref="ValueOrDefault{T}(bool, T)"/>.
        /// </remarks>
        public T? ValueOrNull<T>(T whenTrue)
            where T : struct
        {
            return value ? whenTrue : null;
        }

        /// <summary>
        /// Gets either the value if the bool is <see langword="true"/>,
        /// or <see langword="default"/> if the bool is <see langword="false"/>.
        /// </summary>
        /// <param name="whenTrue">
        /// The value to return if the bool is <see langword="true"/>,
        /// otherwise <see langword="default"/>.
        /// </param>
        public T? ValueOrDefault<T>(T? whenTrue)
        {
            return value ? whenTrue : default;
        }
    }
}
