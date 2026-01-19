using System;
using System.Collections.Generic;

namespace Garyon.Objects;

/// <summary>
/// A base implementation of <see cref="IEquatable{T}"/>, overriding the
/// <see cref="object.Equals(object?)"/> method, and implementing the interface,
/// with focus on comparing the non-null instance via
/// <see cref="EqualsCore(T)"/>.
/// </summary>
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public abstract class BaseEquatable<T> : IEquatable<T>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    protected abstract bool EqualsCore(T other);

    public bool Equals(T? other)
    {
        return other is not null && EqualsCore(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is T t && EqualsCore(t);
    }
}
