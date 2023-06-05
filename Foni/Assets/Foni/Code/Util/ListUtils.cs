using System;
using System.Collections.Generic;

namespace Foni.Code.Util
{
    public static class ListUtils
    {
        public static List<T> GetRandomOrder<T>(IEnumerable<T> inputList, int? seed = null)
        {
            var randomOrderList = new List<T>(inputList);
            var random = seed.HasValue ? new Random(seed.Value) : new Random();

            var n = randomOrderList.Count;
            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                (randomOrderList[k], randomOrderList[n]) = (randomOrderList[n], randomOrderList[k]);
            }

            return randomOrderList;
        }
    }
}