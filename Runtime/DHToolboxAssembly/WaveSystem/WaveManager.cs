using System;
using UniRx;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DHToolbox.Runtime.DHToolboxAssembly.WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        [Serializable]
        public class WaveSetup
        {
            public WaveDifficultySetup WaveDifficultySetup;
            public Spawner Spawner;
        }

        [SerializeField] private WaveSetup[] waves;

        private IntReactiveProperty currentWaveIndex = new(0);

        public IObservable<int> ObserveCurrentWaveIndex => currentWaveIndex;

        public int CurrentWaveIndex
        {
            get => currentWaveIndex.Value;
            private set { currentWaveIndex.Value = Mathf.Clamp(currentWaveIndex.Value + 1, 0, waves.Length - 1); }
        }

        public WaveSetup CurrentWaveSetup => waves[Mathf.Clamp(CurrentWaveIndex, 0, waves.Length - 1)];
        public WaveSetup NextWaveSetup => waves[Mathf.Clamp(CurrentWaveIndex + 1, 0, waves.Length - 1)];

#if ODIN_INSPECTOR
        [Button]
#endif
        public Wave StartNextWave()
        {
            var waveSpawning = NextWaveSetup.Spawner.StartSpawning(NextWaveSetup);
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
    }
}