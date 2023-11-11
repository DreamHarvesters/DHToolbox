using System;
using System.Linq;
using DHToolbox.Runtime.DHToolboxAssembly.Utils.Extensions;
using UniRx;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.WaveSystem
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnTransform[] spawnTransforms;

        private Subject<Unit> stop = new();
        private bool paused;

        private Subject<( SpawnTransform, GameObject )> spawned = new();
        public IObservable<( SpawnTransform, GameObject )> ObserveSpawn => spawned;

        public IObservable<GameObject> StartSpawning(WaveManager.WaveSetup waveSetup)
        {
            var waveDifficultySetup = waveSetup.WaveDifficultySetup;

            var prefabs = waveDifficultySetup.WavePrefabs.SelectMany(setup =>
                Enumerable.Range(0, setup.Amount).Select(_ => setup.Prefabs.GetRandom())).ToArray().Shuffle();
            var totalCount = prefabs.Length;

            var frequency = TimeSpan.FromSeconds(waveDifficultySetup.Duration / totalCount);

            int instantiatedCount = 0;
            return Observable.Interval(frequency)
                .Where(_ => !paused)
                .Take(totalCount)
                .Select(_ =>
                {
                    var spawnTransform = spawnTransforms.GetRandom();
                    var newPrefab = WaveManager.Instance.Factory.Instantiate(prefabs[instantiatedCount],
                        spawnTransform.RandomPosition,
                        Quaternion.identity);

                    spawned.OnNext((spawnTransform, newPrefab));
                    instantiatedCount++;
                    return newPrefab;
                })
                .TakeUntil(stop);
        }

        public void Stop() => stop.OnNext(Unit.Default);

        public void Pause() => paused = true;

        public void Unpause() => paused = false;
    }
}