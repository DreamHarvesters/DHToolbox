using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DHToolbox.Runtime.DHToolboxAssembly.WaveSystem
{
    [CreateAssetMenu(fileName = nameof(WaveDifficultySetup),
        menuName = Constants.CreateMenuCategory + "/" + nameof(WaveDifficultySetup))]
    public class WaveDifficultySetup : ScriptableObject
    {
        [SerializeField] private WavePrefabSetup[] wavePrefabs;
        [SerializeField] private float duration;

        public WavePrefabSetup[] WavePrefabs => wavePrefabs;
        public float Duration => duration;
    }

    [Serializable]
    public class WavePrefabSetup
    {
        public GameObject[] Prefabs;
        [FormerlySerializedAs("amount")] public int Amount;
    }
}