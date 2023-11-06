using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Indexing
{
    /// <summary>
    /// Min and max included
    /// </summary>
    public class ClampedIndexProvider : IndexProvider
    {
        public ClampedIndexProvider(int min, int max) : this(min, max, min)
        {
        }

        public ClampedIndexProvider(int min, int max, int current)
        {
            this.min = min;
            this.max = max;

            Current = Mathf.Clamp(current, min, max);
        }

        public override int Next => Mathf.Clamp(Current + 1, min, max);
    }
}