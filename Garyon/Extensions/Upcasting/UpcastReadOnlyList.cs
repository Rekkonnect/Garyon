using Garyon.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions.Upcasting;

internal sealed class UpcastReadOnlyList<TBase, TUp>(IReadOnlyList<TBase> backingList)
    : IReadOnlyList<TUp>
    where TUp : class, TBase
{
    private readonly IReadOnlyList<TBase> _backing = backingList;

    public TUp this[int index] => (TUp)_backing[index]!;

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
