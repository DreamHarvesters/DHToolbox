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
            int circulateFromLevelIndex, int currentLevel, bool isLevelIndexZeroBased)
        {
            lastLevelIndex = firstLevelIndex + levelCount - 1;
            this.circulateFromLevelIndex = circulateFromLevelIndex;
            this.levelCount = levelCount;
            this.firstLevelIndex = firstLevelIndex;

            var currentLevelSceneIndex = currentLevel;
            if (!isLevelIndexZeroBased)
                currentLevelSceneIndex = (currentLevel - 1) + this.firstLevelIndex;

            if (currentLevelSceneIndex > lastLevelIndex)
                concreteIndexProvider = CreateCircularIndexProvider(currentLevelSceneIndex);
            else
            {
                concreteIndexProvider =
                    new ClampedIndexProvider(firstLevelIndex, lastLevelIndex, currentLevelSceneIndex);
            }
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