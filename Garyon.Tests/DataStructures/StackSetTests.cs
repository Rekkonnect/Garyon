using Garyon.DataStructures;
using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TUnit.Core;

namespace Garyon.Tests.DataStructures;

[InheritsTests]
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