using System;
using DHToolbox.Runtime.DHToolboxAssembly.Indexing;
using UniRx;

namespace DHToolbox.Runtime.DHToolboxAssembly.GameLevels
{
    public class CircularIndexAfterCompletingAllLevelsProvider : IndexProvider
    {
        private IndexProvider concreteIndexProvider;
        private int lastLevelIndex;
        private int circulateFromLevelIndex;
        private int levelCount;
        private int firstLevelIndex;

        public CircularIndexAfterCompletingAllLevelsProvider(int firstLevelIndex, int levelCount,
            int circulateFromLevelIndex, int currentLevel)
        {
            lastLevelIndex = firstLevelIndex + levelCount - 1;
            this.circulateFromLevelIndex = circulateFromLevelIndex;
            this.levelCount = levelCount;
            this.firstLevelIndex = firstLevelIndex;

            if (currentLevel > lastLevelIndex)
                concreteIndexProvider = CreateCircularIndexProvider(currentLevel);
            else
            {
                concreteIndexProvider = new ClampedIndexProvider(firstLevelIndex, lastLevelIndex);
            }

            Current = concreteIndexProvider.Current;
        }

        CircularIndexProvider CreateCircularIndexProvider(int level) =>
            new(circulateFromLevelIndex, firstLevelIndex + levelCount, level);

        public override int Current => concreteIndexProvider.Current;

        public override void MoveNext()
        {
            if (concreteIndexProvider.Current + 1 > lastLevelIndex && concreteIndexProvider is ClampedIndexProvider)
            {
                concreteIndexProvider = CreateCircularIndexProvider(concreteIndexProvider.Current + 1);
                return;
            }

            concreteIndexProvider.MoveNext();
        }

        public override int Next => concreteIndexProvider.Next;
    }
}