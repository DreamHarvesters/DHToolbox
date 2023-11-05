using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Indexing
{
    /// <summary>
    /// Max exclusive
    /// </summary>
    public class RandomIndexProvider : IndexProvider
    {
        public RandomIndexProvider(int max, int min)
        {
            this.max = max;
            this.min = min;
        }

        public override int Next => Random.Range(min, max);
    }
}