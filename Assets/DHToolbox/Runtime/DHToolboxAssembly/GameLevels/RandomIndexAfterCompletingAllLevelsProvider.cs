using DHToolbox.Runtime.DHToolboxAssembly.Indexing;

namespace DHToolbox.Runtime.DHToolboxAssembly.GameLevels
{
    public class RandomIndexAfterCompletingAllLevelsProvider : IndexProvider
    {
        private IndexProvider concreteIndexProvider;
        private int lastLevelIndex;
        private int minRandom;
        private int levelCount;
        private int firstLevelIndex;

        public RandomIndexAfterCompletingAllLevelsProvider(int firstLevelIndex, int levelCount,
            int minRandom, int currentLevel, bool isLevelIndexZeroBased)
        {
            lastLevelIndex = firstLevelIndex + levelCount - 1;
            this.minRandom = minRandom;
            this.levelCount = levelCount;
            this.firstLevelIndex = firstLevelIndex;

            var currentLevelSceneIndex = currentLevel;
            if (!isLevelIndexZeroBased)
                currentLevelSceneIndex = (currentLevel - 1) + this.firstLevelIndex;

            if (currentLevelSceneIndex > lastLevelIndex)
                concreteIndexProvider = CreateRandomIndexProvider();
            else
            {
                concreteIndexProvider =
                    new ClampedIndexProvider(firstLevelIndex, lastLevelIndex, currentLevelSceneIndex);
            }
        }

        RandomIndexProvider CreateRandomIndexProvider() =>
            new(minRandom, firstLevelIndex + levelCount);

        public override int Current => concreteIndexProvider.Current;

        public override void MoveNext()
        {
            if (concreteIndexProvider.Current + 1 > lastLevelIndex && concreteIndexProvider is ClampedIndexProvider)
            {
                concreteIndexProvider = CreateRandomIndexProvider();
                return;
            }

            concreteIndexProvider.MoveNext();
        }

        public override int Next => concreteIndexProvider.Next;
    }
}