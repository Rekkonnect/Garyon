using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions.Upcasting;

internal sealed class UpcastReadOnlyCollection<TBase, TUp>(IReadOnlyCollection<TBase> backingCollection)
    : IReadOnlyCollection<TUp>
    where TUp : class, TBase
{
    private readonly IReadOnlyCollection<TBase> _backing = backingCollection;

    public int Count => _backing.Count;

    public IEnumerator<TUp> GetEnumerator()
    {
        return _backing.Cast<TUp>()
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

