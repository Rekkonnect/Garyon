using Garyon.DataStructures;
using System;
using System.Collections.Generic;
using TUnit.Core;

namespace Garyon.Tests.DataStructures;

[InheritsTests]
public class QueueSetTests : BaseSetLinearCollectionTests
{
    protected override Index ProjectToCollectionIndex(Index index)
    {
        return index;
    }
    protected override IEnumerable<T> TransformForExpectedEnumerationOrder<T>(IEnumerable<T> enumerable)
    {
        return enumerable;
    }

    protected override BaseSetLinearCollection<T> InitializeInstance<T>()
    {
        return new QueueSet<T>();
    }
}