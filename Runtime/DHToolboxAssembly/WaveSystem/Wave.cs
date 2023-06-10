using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.WaveSystem
{
    public class Wave
    {
        public IObservable<GameObject> ObserveSpawn { get; private set; }

        public IObservable<Unit> ObserveSpawnComplete { get; private set; }

        public WaveManager.WaveSetup WaveSetup { get; private set; }

        private Subject<Unit> allSpawnedItemsDestroyed = new();
        public IObservable<Unit> ObserveAllSpawnedItemsDestroyed => allSpawnedItemsDestroyed;

        private List<GameObject> spawnedItems = new();

        public Wave(IObservable<GameObject> observeSpawn, IObservable<Unit> observeSpawnComplete,
            WaveManager.WaveSetup waveSetup)
        {
            ObserveSpawn = observeSpawn.Share();
            ObserveSpawnComplete = observeSpawnComplete;
            WaveSetup = waveSetup;

            ObserveSpawn.Subscribe(spawned =>
            {
                spawned.OnDestroyAsObservable().Subscribe(_ =>
                {
                    spawnedItems.Remove(spawned);
                    if (spawnedItems.Count == 0)
                        allSpawnedItemsDestroyed.OnNext(Unit.Default);
                });
                spawnedItems.Add(spawned);
            });
        }
    }
}