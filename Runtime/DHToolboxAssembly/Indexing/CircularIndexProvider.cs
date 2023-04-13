using System;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Indexing
{
    public class CircularIndexProvider : IndexProvider
    {
        public CircularIndexProvider(int min, int max)
        {
            this.min = min;
            this.max = max;

            Current = min;
        }

        public CircularIndexProvider(int min, int max, int current)
        {
            this.min = min;
            this.max = max;
            Current = Mathf.Max(min, current % max);
        }

        public override int Next => Mathf.Max(min, (Current + 1) % max);
    }
}