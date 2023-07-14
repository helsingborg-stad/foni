using System;
using System.Collections.Generic;

namespace Foni.Code.Util
{
    public static class ListUtils
    {
        public static List<T> InRandomOrder<T>(this IEnumerable<T> inputList, Random random = null)
        {
            var randomOrderList = new List<T>(inputList);
            var randomToUse = random ?? new Random();

            var n = randomOrderList.Count;
            while (n > 1)
            {
                n--;
                var k = randomToUse.Next(n + 1);
                (randomOrderList[k], randomOrderList[n]) = (randomOrderList[n], randomOrderList[k]);
            }

            return randomOrderList;
        }

        public static List<T> Sorted<T>(this IEnumerable<T> inputList, Comparison<T> comparer)
        {
            var newList = new List<T>(inputList);
            newList.Sort(comparer);
            return newList;
        }

        public static T PickRandom<T>(this List<T> inputList, Random random = null)
        {
            var randomToUse = random ?? new Random();
            var randomIndex = randomToUse.Next(0, inputList.Count);
            return inputList[randomIndex];
        }

        public static List<T> Pad<T>(this List<T> initial, List<T> paddingSource, int desiredLength)
        {
            if (initial.Count >= desiredLength)
            {
                return initial;
            }

            var newList = new List<T>(initial);
            newList.AddRange(paddingSource.GetRange(0, desiredLength - initial.Count));
            return newList;
        }

        public static void ForEachWithIndex<T>(this List<T> inputList, Action<T, int> action)
        {
            for (var i = 0; i < inputList.Count; i++)
            {
                action(inputList[i], i);
            }
        }
    }
}