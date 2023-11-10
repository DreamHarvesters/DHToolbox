using DHToolbox.Runtime.DHToolboxAssembly.Indexing;

namespace DHToolbox.Runtime.DHToolboxAssembly.GameLevels
{
    public class RandomizeAfterCompletingRangeProvider : IndexProvider
    {
        private IndexProvider concreteIndexProvider;
        private int lastLevelIndex;
        private int minRandom;
        private int count;
        private int rangeStart;

        public RandomizeAfterCompletingRangeProvider(int rangeStart, int count,
            int minRandom, int current, bool isCurrentValueZeroBased)
        {
            lastLevelIndex = rangeStart + count - 1;
            this.minRandom = minRandom;
            this.count = count;
            this.rangeStart = rangeStart;

            var currentLevelSceneIndex = current;
            if (!isCurrentValueZeroBased)
                currentLevelSceneIndex = (current - 1) + this.rangeStart;

            if (currentLevelSceneIndex > lastLevelIndex)
                concreteIndexProvider = CreateRandomIndexProvider();
            else
            {
                concreteIndexProvider =
                    new ClampedIndexProvider(rangeStart, lastLevelIndex, currentLevelSceneIndex);
            }
        }

        RandomIndexProvider CreateRandomIndexProvider() =>
            new(minRandom, rangeStart + count);

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