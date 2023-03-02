using DHToolbox.Runtime.DHToolboxAssembly.Indexing;
using NUnit.Framework;
using UnityEngine;

namespace GameFoundations.Tests.Editor
{
    public class IndexProviderTests
    {
        private int CalculateCircularCurrent(int iteration, int start, int end) =>
            (iteration - start) % (end - start) + start;

        private int CalculateCircularNext(int iteration, int start, int end) =>
            (iteration - start + 1) % (end - start) + start;

        [Test]
        public void CircularIndex_FromZero2Ten()
        {
            int start = 0;
            int end = 10;

            var indexProvider = new CircularIndexProvider(start, end);
            for (int i = start; i < 2 * end; i++)
            {
                Assert.IsTrue(CalculateCircularNext(i, start, end) == indexProvider.Next,
                    $"Iteration: {i} - Expected Next: {CalculateCircularNext(i, start, end)} - Provided Next: {indexProvider.Next}");
                Assert.IsTrue(CalculateCircularCurrent(i, start, end) == indexProvider.Current,
                    $"Iteration: {i} - Provided Index: {indexProvider.Current}");
                indexProvider.MoveNext();
            }
        }

        [Test]
        public void CircularIndex_RandomRange()
        {
            int start = Random.Range(0, 100);
            int end = Random.Range(100, 200);

            Debug.Log($"Testing from {start} to {end}");

            var indexProvider = new CircularIndexProvider(start, end);
            for (int i = start; i < 2 * end; i++)
            {
                var expectedNext = CalculateCircularNext(i, start, end);
                Assert.IsTrue(expectedNext == indexProvider.Next,
                    $"Iteration: {i} - Expected Next: {CalculateCircularNext(i, start, end)} - Provided Next: {indexProvider.Next}");
                Assert.IsTrue(CalculateCircularCurrent(i, start, end) == indexProvider.Current);
                indexProvider.MoveNext();
            }
        }

        [Test]
        public void CircularIndex_WithInitialNext()
        {
            int start = 0;
            int end = 30;
            int next = 12;

            var indexProvider = new CircularIndexProvider(start, end, next);
            for (int i = next; i < 2 * end; i++)
            {
                Assert.IsTrue(CalculateCircularNext(i, start, end) == indexProvider.Next);
                Assert.IsTrue(CalculateCircularCurrent(i, start, end) == indexProvider.Current,
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