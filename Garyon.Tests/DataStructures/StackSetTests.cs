using Garyon.DataStructures;
using Garyon.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Tests.DataStructures;

[Parallelizable(ParallelScope.Children)]
public class StackSetTests : BaseSetLinearCollectionTests
{
    protected override Index ProjectToCollectionIndex(Index index)
    {
        return index.Invert();
    }
    protected override IEnumerable<T> TransformForExpectedEnumerationOrder<T>(IEnumerable<T> enumerable)
    {
        return enumerable.Reverse();
    }

    protected override BaseSetLinearCollection<T> InitializeInstance<T>()
    {
        return new StackSet<T>();
    }
}
