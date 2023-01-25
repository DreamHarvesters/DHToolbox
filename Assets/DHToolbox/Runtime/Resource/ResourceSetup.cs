using Foundations.Scripts.Identification;
using UnityEngine;

namespace Foundations.Scripts.Resource
{
    [CreateAssetMenu(fileName = nameof(ResourceSetup), menuName = "Toolbox/" + nameof(ResourceSetup), order = 0)]
    public class ResourceSetup : ScriptableObject
    {
        [SerializeField] private Id id;
        [SerializeField] private Sprite icon;
        [SerializeField] private string uIName;

        public Id Id => id;

        public Sprite Icon => icon;

        public string UIName => uIName;
    }
}