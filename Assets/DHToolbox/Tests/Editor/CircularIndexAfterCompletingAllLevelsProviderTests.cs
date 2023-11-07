using DHToolbox.Runtime.DHToolboxAssembly.GameLevels;
using DHToolbox.Runtime.DHToolboxAssembly.Indexing;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace GameFoundations.Tests.Editor
{
    public class CircularIndexAfterCompletingAllLevelsProviderTests
    {
        [Test]
        public void SequentialIndexGeneration()
        {
            // Arrange
            int firstLevelIndex = 1;
            int levelCount = 5;
            int circulateFromLevelIndex = 3;
            var provider = new CircularIndexAfterCompletingAllLevelsProvider(
                firstLevelIndex, levelCount, circulateFromLevelIndex, firstLevelIndex, false);

            // Act and Assert
            for (int i = firstLevelIndex; i <= levelCount; i++)
            {
                Assert.AreEqual(i, provider.Current);
                provider.MoveNext();
            }
        }

        [Test]
        public void CircularIndexGeneration()
        {
            // Arrange
            int firstLevelIndex = 1;
            int levelCount = 5;
            int circulateFromLevelIndex = 3;
            int currentLevel = 6; // Beyond lastLevelIndex
            var provider = new CircularIndexAfterCompletingAllLevelsProvider(
                firstLevelIndex, levelCount, circulateFromLevelIndex, currentLevel, false);

            var testProvider =
                new CircularIndexProvider(circulateFromLevelIndex, firstLevelIndex + levelCount, currentLevel);
            // Act and Assert
            for (int i = circulateFromLevelIndex; i <= firstLevelIndex + levelCount; i++)
            {
                Assert.AreEqual(testProvider.Current, provider.Current);
                testProvider.MoveNext();
                provider.MoveNext();
            }
        }

        [Test]
        public void SwitchToCircularIndexFromClampedIndex()
        {
            // Arrange
            int firstLevelIndex = 1;
            int levelCount = 5;
            int circulateFromLevelIndex = 3;
            int currentLevel = 1; // Beyond lastLevelIndex
            var provider = new CircularIndexAfterCompletingAllLevelsProvider(
                firstLevelIndex, levelCount, circulateFromLevelIndex, currentLevel, false);

            var testCircularIndexProvider =
                new CircularIndexProvider(circulateFromLevelIndex, firstLevelIndex + levelCount,
                    circulateFromLevelIndex);
            for (int i = currentLevel; i < firstLevelIndex + 2 * levelCount; i++)
            {
                if (i <= firstLevelIndex + levelCount - 1)
                    Assert.AreEqual(i, provider.Current);
                else
                {
                    Assert.AreEqual(testCircularIndexProvider.Current, provider.Current);
                    testCircularIndexProvider.MoveNext();
                }

                Debug.Log($"Index: {provider.Current}");
                provider.MoveNext();
            }
        }
    }
}