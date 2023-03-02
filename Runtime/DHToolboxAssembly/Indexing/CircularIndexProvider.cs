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
            if (current < min || current > max)
                throw new Exception("Invalid next value. Must be between min and max");

            this.min = min;
            this.max = max;
            Current = current;
        }

        public override int Next => Mathf.Max(min, (Current + 1) % max);
    }
}