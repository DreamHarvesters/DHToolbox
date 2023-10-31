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

            Current = (current - min) % Mathf.Max(1, max - min) + min;
        }

        public override int Next => (Current - min + 1) % (max - min) + min;
    }
}