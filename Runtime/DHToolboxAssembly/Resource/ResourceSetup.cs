using DHToolbox.Runtime.DHToolboxAssembly.Identification;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Resource
{
    [CreateAssetMenu(fileName = nameof(ResourceSetup),
        menuName = Constants.CreateMenuCategory + "/" + nameof(ResourceSetup), order = 0)]
    public class ResourceSetup : ScriptableObject
    {
        [SerializeField] private Id id;
        [SerializeField] private Sprite icon;
        [SerializeField] private string uIName;

        public static ResourceSetup Create(Id id, Sprite icon, string uiName)
        {
            var newSetup = ScriptableObject.CreateInstance<ResourceSetup>();
            newSetup.id = id;
            newSetup.icon = icon;
            newSetup.uIName = uiName;
            return newSetup;
        }

        public Id Id => id;

        public Sprite Icon => icon;

        public string UIName => uIName;
    }
}