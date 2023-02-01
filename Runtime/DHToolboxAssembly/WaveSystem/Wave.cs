using System;
using UniRx;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.WaveSystem
{
    public class Wave
    {
        public IObservable<GameObject> ObserveSpawn { get; private set; }

        public IObservable<Unit> ObserveSpawnComplete { get; private set; }

        public WaveManager.WaveSetup WaveSetup { get; private set; }

        public Wave(IObservable<GameObject> observeSpawn, IObservable<Unit> observeSpawnComplete,
            WaveManager.WaveSetup waveSetup)
        {
            ObserveSpawn = observeSpawn;
            ObserveSpawnComplete = observeSpawnComplete;
            WaveSetup = waveSetup;
        }
    }
}