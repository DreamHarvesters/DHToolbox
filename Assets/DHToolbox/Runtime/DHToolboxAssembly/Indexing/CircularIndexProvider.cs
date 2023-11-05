using System;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Indexing
{
    /// <summary>
    /// Max exclusive
    /// </summary>
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

            int range = max - min;
            Current = min + ((current - min) % range + range) % range;
        }

        public override int Next
        {
            get
            {
                int range = max - min;
                int circularValue = min + ((Current + 1 - min) % range + range) % range;
                return circularValue;
            }
        }
    }
}