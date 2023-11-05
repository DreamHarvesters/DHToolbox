using DHToolbox.Runtime.DHToolboxAssembly.Indexing;
using NUnit.Framework;
using UnityEngine;

namespace GameFoundations.Tests.Editor
{
    public class IndexProviderTests
    {
        private int CalculateCircularCurrent(int iteration, int start, int end) =>
            (iteration - start) % (end - start) + start;

        private int CalculateCircularNext(int iteration, int start, int end)
        {
            int range = end - start;
            int circularValue = start + ((iteration % range + range) % range);
            return circularValue;
        }

        [Test]
        public void CircularIndex_FromZero2Ten()
        {
            int start = 3;
            int end = 10;

            int maxIndex = 6;

            var indexProvider = new CircularIndexProvider(start, maxIndex);
            for (int i = start; i < end; i++)
            {
                Debug.Log($"Iteration: {i} - Index: {indexProvider.Current}");
                var expectedNext = CalculateCircularNext((i + 1), start, maxIndex);
                Assert.IsTrue(expectedNext == indexProvider.Next,
                    $"Iteration: {i} - Expected Next: {expectedNext} - Provided Next: {indexProvider.Next}");

                var expectedCurrent = CalculateCircularCurrent(i, start, maxIndex);
                Assert.IsTrue(expectedCurrent == indexProvider.Current,
                    $"Iteration: {i} - ExpectedCurrent: {expectedCurrent} - Provided Current: {indexProvider.Current}");
                indexProvider.MoveNext();
            }
        }

        [Test]
        public void CircularIndex_From3ToFive()
        {
            int min = 3;
            int max = 5;

            var indexProvider = new CircularIndexProvider(min, max);
            Assert.AreEqual(3, indexProvider.Current);
            Assert.AreEqual(4, indexProvider.Next);

            indexProvider.MoveNext();
            Assert.AreEqual(4, indexProvider.Current);
            Assert.AreEqual(3, indexProvider.Next);
        }

        [Test]
        public void CircularIndex_WithInitialNext()
        {
            int start = 0;
            int end = 30;

            int minIndex = 10;
            int maxIndex = 15;
            int current = 12;

            var indexProvider = new CircularIndexProvider(minIndex, maxIndex, current);
            for (int i = current; i < 2 * end; i++)
            {
                var expectedNext = CalculateCircularNext(i + 1, minIndex, maxIndex);
                Assert.IsTrue(expectedNext == indexProvider.Next);
                Assert.IsTrue(CalculateCircularCurrent(i, minIndex, maxIndex) == indexProvider.Current,
                    $"Iteration: {i} - Provided Index: {indexProvider.Current}");
                indexProvider.MoveNext();
            }
        }

        [Test]
        public void ClampedIndex()
        {
            int start = 0;
            int end = 10;

            var indexProvider = new ClampedIndexProvider(start, end);
            for (int i = 0; i < 2 * end; i++)
            {
                Assert.IsTrue(Mathf.Clamp(i + 1, start, end) == indexProvider.Next);
                Assert.IsTrue(Mathf.Clamp(i, start, end) == indexProvider.Current,
                    $"Iteration: {i} - Provided Index: {indexProvider.Current}");
                indexProvider.MoveNext();
            }
        }
    }
}