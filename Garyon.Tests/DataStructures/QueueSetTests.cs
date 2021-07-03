using Garyon.DataStructures;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Garyon.Tests.DataStructures
{
    [Parallelizable(ParallelScope.Children)]
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
}
