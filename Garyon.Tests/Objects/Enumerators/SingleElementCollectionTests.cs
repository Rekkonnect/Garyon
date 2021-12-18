using Garyon.Objects.Enumerators;
using NUnit.Framework;
using System.Linq;

namespace Garyon.Tests.Objects.Enumerators
{
    public class SingleElementCollectionTests
    {
        [Test]
        public void Test()
        {
            var collection = new SingleElementCollection<int>(2);
            Assert.AreEqual(new[] { 2 }, collection.ToArray());
        }
    }
}