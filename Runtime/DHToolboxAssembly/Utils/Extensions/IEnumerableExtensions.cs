using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Utils.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            int newIndex = Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(newIndex);
        }

        public static T[] Shuffle<T>(this T[] array)
        {
            // Fisher Yates shuffle algorithm, see https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            for (int t = 0; t < array.Length; t++)
            {
                T tmp = array[t];
                int randomIndex = Random.Range(t, array.Length);
                array[t] = array[randomIndex];
                array[randomIndex] = tmp;
            }

            return array;
        }

        public static List<T> Shuffle<T>(this List<T> array)
        {
            // Fisher Yates shuffle algorithm, see https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            for (int t = 0; t < array.Count; t++)
            {
                T tmp = array[t];
                int randomIndex = Random.Range(t, array.Count);
                array[t] = array[randomIndex];
                array[randomIndex] = tmp;
            }

            return array;
        }
    }
}