using System;
using DHToolbox.Runtime.DHToolboxAssembly.Singleton;
using UniRx;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DHToolbox.Runtime.DHToolboxAssembly.WaveSystem
{
    public class WaveManager : MonoBehaviourSingleton<WaveManager>
    {
        [Serializable]
        public class WaveSetup
        {
            public WaveDifficultySetup WaveDifficultySetup;
            public Spawner Spawner;
        }

        [SerializeField] private WaveSetup[] waves;

        private IntReactiveProperty currentWaveIndex = new(-1);

        public IObservable<int> ObserveCurrentWaveIndex => currentWaveIndex;

        public int WavesLength => waves.Length;

        public int CurrentWaveIndex
        {
            get => currentWaveIndex.Value;
            private set { currentWaveIndex.Value = Mathf.Clamp(value, 0, waves.Length - 1); }
        }

        public WaveSetup CurrentWaveSetup => waves[Mathf.Clamp(CurrentWaveIndex, 0, waves.Length - 1)];
        public WaveSetup NextWaveSetup => waves[Mathf.Clamp(CurrentWaveIndex + 1, 0, waves.Length - 1)];

        public bool WavesCompleted => currentWaveIndex.Value == waves.Length - 1;

        public IObjectFactory Factory { get; set; } = new InstantiaterFactory();

#if ODIN_INSPECTOR
        [Button]
#endif
        public Wave StartNextWave()
        {
            var waveSpawning = NextWaveSetup.Spawner.StartSpawning(NextWaveSetup).Share();
            var spawnComplete = new Subject<Unit>();
            waveSpawning.DoOnCompleted(() =>
            {
                spawnComplete.OnNext(Unit.Default);
                spawnComplete.OnCompleted();
            }).Subscribe().AddTo(gameObject);
            var newWave = new Wave(waveSpawning, spawnComplete, NextWaveSetup);
            CurrentWaveIndex++;
            return newWave;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public Wave StartWave(int index)
        {
            var waveSpawning = waves[index].Spawner.StartSpawning(waves[index]);
            var spawnComplete = new Subject<Unit>();
            waveSpawning.DoOnCompleted(() =>
            {
                spawnComplete.OnNext(Unit.Default);
                spawnComplete.OnCompleted();
            }).Subscribe().AddTo(gameObject);
            var newWave = new Wave(waveSpawning, spawnComplete, NextWaveSetup);
            return newWave;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void StopCurrentWave()
        {
            CurrentWaveSetup.Spawner.Stop();
        }
    }
}