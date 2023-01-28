using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UniRx;
using UnityEngine;

namespace GameAssets.Scripts.Waves
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

        public int CurrentWaveIndex { get; private set; } = -1;

        public WaveSetup CurrentWaveSetup => waves[Mathf.Clamp(CurrentWaveIndex, 0, waves.Length - 1)];
        public WaveSetup NextWaveSetup => waves[Mathf.Clamp(CurrentWaveIndex + 1, 0, waves.Length - 1)];

#if ODIN_INSPECTOR
        [Button]
#endif
        public void StartNextWave() => NextWaveSetup.Spawner.StartSpawning(NextWaveSetup).Subscribe();

#if ODIN_INSPECTOR
        [Button]
#endif
        public void StartWave(int index) => waves[index].Spawner.StartSpawning(waves[index]).Subscribe();
    }
}