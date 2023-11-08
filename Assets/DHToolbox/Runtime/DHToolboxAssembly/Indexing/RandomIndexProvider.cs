using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Indexing
{
    /// <summary>
    /// Max exclusive
    /// </summary>
    public class RandomIndexProvider : IndexProvider
    {
        public RandomIndexProvider(int min, int max)
        {
            this.max = max;
            this.min = min;

            Current = Next;
        }

        public override int Next => Random.Range(min, max);
    }
}