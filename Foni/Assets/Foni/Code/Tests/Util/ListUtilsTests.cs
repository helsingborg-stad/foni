using System.Collections.Generic;
using System.Linq;
using Foni.Code.Util;
using NUnit.Framework;
using Random = System.Random;

namespace Foni.Code.Tests.Util
{
    public class ListUtilsTests
    {
        [Test]
        public void InRandomOrder()
        {
            var random = new Random(1337);
            var list = new[] { "a", "b", "c", "d", "e" };
            var expected = new[] { "c", "e", "d", "a", "b" };

            var result = list.InRandomOrder(random);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void PickRandom()
        {
            var random = new Random(1337);
            var list = new List<string> { "a", "b", "c", "d", "e" };
            const string expected = "b";

            var result = list.PickRandom(random);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Pad()
        {
            var list = new List<string> { "a", "b", "c" };
            var padding = new List<string> { "d", "e", "f" };
            var expected = new List<string> { "a", "b", "c", "d", "e" };

            var result = list.Pad(padding, 5);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void ForEachWithIndex()
        {
            var list = new List<int> { 0, 1, 2, 3, 4 };
            var results = new List<bool>(5);

            list.ForEachWithIndex((item, index) => { results.Add(item == index); });

            Assert.IsTrue(results.All(result => result));
        }
    }
}