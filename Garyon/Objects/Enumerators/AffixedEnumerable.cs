using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Objects.Enumerators;

/// <summary>
/// Represents an enumerable that supports being prefixed
/// and suffixed with other enumerables or single values.
/// </summary>
/// <typeparam name="T">The type of values enumerated.</typeparam>
public class AffixedEnumerable<T> : IEnumerable<T>
{
    private readonly SingleOrEnumerable<T> prefix = new();
    private readonly SingleOrEnumerable<T> suffix = new();

    private readonly IEnumerable<T> mainEnumerable;

    public AffixedEnumerable(IEnumerable<T> enumerable)
    {
        mainEnumerable = enumerable ?? Enumerable.Empty<T>();
    }

    public AffixedEnumerable<T> WithPrefix(T value)
    {
        prefix.Assign(value);
        return this;
    }
    public AffixedEnumerable<T> WithPrefix(IEnumerable<T> enumerable)
    {
        prefix.Assign(enumerable);
        return this;
    }
    public AffixedEnumerable<T> WithPrefix(SingleOrEnumerable<T> value)
    {
        prefix.Assign(value);
        return this;
    }

    public AffixedEnumerable<T> WithSuffix(T value)
    {
        suffix.Assign(value);
        return this;
    }
    public AffixedEnumerable<T> WithSuffix(IEnumerable<T> enumerable)
    {
        suffix.Assign(enumerable);
        return this;
    }
    public AffixedEnumerable<T> WithSuffix(SingleOrEnumerable<T> value)
    {
        suffix.Assign(value);
        return this;
    }

    public AffixedEnumerable<T> WithExtraPrefix(T value)
    {
        return Wrap().WithPrefix(value);
    }
    public AffixedEnumerable<T> WithExtraPrefix(IEnumerable<T> enumerable)
    {
        return Wrap().WithPrefix(enumerable);
    }
    public AffixedEnumerable<T> WithExtraPrefix(SingleOrEnumerable<T> value)
    {
        return Wrap().WithPrefix(value);
    }

    public AffixedEnumerable<T> WithExtraSuffix(T value)
    {
        return Wrap().WithSuffix(value);
    }
    public AffixedEnumerable<T> WithExtraSuffix(IEnumerable<T> enumerable)
    {
        return Wrap().WithSuffix(enumerable);
    }
    public AffixedEnumerable<T> WithExtraSuffix(SingleOrEnumerable<T> value)
    {
        return Wrap().WithSuffix(value);
    }

    public AffixedEnumerable<T> Wrap() => new(this);

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<T>
    {
        private readonly AffixedEnumerable<T> affixed;
        private IEnumerator<T> currentEnumerator;
        private AffixedEnumeratorState state;

        object IEnumerator.Current => Current;
        public T Current
        {
            get
            {
                return state switch
                {
                    AffixedEnumeratorState.Prefix or
                    AffixedEnumeratorState.Main or
                    AffixedEnumeratorState.Suffix => currentEnumerator.Current,

                    _ => throw new InvalidOperationException("The enumerator is not in a state of yielding a value."),
                };
            }
        }

        public Enumerator(AffixedEnumerable<T> affixedEnumerable)
        {
            affixed = affixedEnumerable;
            Reset();
        }

        void IDisposable.Dispose() { }

        public bool MoveNext()
        {
            bool next = currentEnumerator?.MoveNext() is true;
            if (next)
                return true;

            while (true)
            {
                state++;
                switch (state)
                {
                    case AffixedEnumeratorState.Prefix:
                        currentEnumerator = affixed.prefix.GetEnumerator();
                        break;

                    case AffixedEnumeratorState.Main:
                        currentEnumerator = affixed.mainEnumerable.GetEnumerator();
                        break;

                    case AffixedEnumeratorState.Suffix:
                        currentEnumerator = affixed.suffix.GetEnumerator();
                        break;

                    case AffixedEnumeratorState.After:
                        return false;
                }

                next = currentEnumerator.MoveNext();
                if (next)
                    return true;
            }
        }

        public void Reset()
        {
            currentEnumerator = null;
            state = AffixedEnumeratorState.Before;
        }
    }

    private enum AffixedEnumeratorState
    {
        Before = -1,
        Prefix = 0,
        Main = 1,
        Suffix = 2,
        After = 3,
    }
}
