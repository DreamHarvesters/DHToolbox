using System;
using DHToolbox.Runtime.DHToolboxAssembly.Indexing;
using UniRx;

namespace DHToolbox.Runtime.DHToolboxAssembly.GameLevels
{
    public class CirculateAfterCompletingRangeProvider : IndexProvider
    {
        private IndexProvider concreteIndexProvider;
        private int last;
        private int circulateFrom;
        private int count;
        private int rangeStart;

        public CirculateAfterCompletingRangeProvider(int rangeStart, int count,
            int circulateFrom, int current, bool isCurrentValueZeroBased)
        {
            last = rangeStart + count - 1;
            this.circulateFrom = circulateFrom;
            this.count = count;
            this.rangeStart = rangeStart;

            var currentLevelSceneIndex = current;
            if (!isCurrentValueZeroBased)
                currentLevelSceneIndex = (current - 1) + this.rangeStart;

            if (currentLevelSceneIndex > last)
                concreteIndexProvider = CreateCircularIndexProvider(currentLevelSceneIndex);
            else
            {
                concreteIndexProvider =
                    new ClampedIndexProvider(rangeStart, last, currentLevelSceneIndex);
            }
        }

        CircularIndexProvider CreateCircularIndexProvider(int level) =>
            new(circulateFrom, rangeStart + count, level);

        public override int Current => concreteIndexProvider.Current;

        public override void MoveNext()
        {
            if (concreteIndexProvider.Current + 1 > last && concreteIndexProvider is ClampedIndexProvider)
            {
                concreteIndexProvider = CreateCircularIndexProvider(concreteIndexProvider.Current + 1);
                return;
            }

            concreteIndexProvider.MoveNext();
        }

        public override int Next => concreteIndexProvider.Next;
    }
}