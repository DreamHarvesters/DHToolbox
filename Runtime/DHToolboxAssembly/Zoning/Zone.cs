using System;
using System.Collections.Generic;
using System.Linq;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace TemplateAssets.Scripts.Zoning
{
    [Serializable]
    public class Zone
    {
        private Zoning zoning;

#if ODIN_INSPECTOR
        [ListDrawerSettings(DraggableItems = false, ShowIndexLabels = true)]
#endif
        [SerializeField]
        private List<ZonedMaterial> materials;

        [SerializeField]
        private string tag;

        public string Tag => tag;

        public Zone(Zoning zoning, List<ZonedMaterial> materials)
        {
            this.zoning = zoning;
            this.materials = materials;
        }

        public void Apply()
        {
            foreach (ZonedMaterial zonedMaterial in materials)
            {
                zonedMaterial.Apply();
            }
        }

#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button(ButtonSizes.Large), GUIColor(1, 0, 0)]
#endif
        public void Remove()
        {
            zoning.Remove(this);
        }

#if ODIN_INSPECTOR
        [Button(ButtonSizes.Large)]
#endif
        public void Duplicate()
        {
            zoning.AddZone(Clone());
        }

        public void Init(Zoning zoning)
        {
            this.zoning = zoning;

            foreach (ZonedMaterial zonedMaterial in materials)
            {
                zonedMaterial.Init();
            }
        }

        public void AddMaterial(Material m)
        {
            materials.Add(new ZonedMaterial(m));
        }

        private Zone Clone()
        {
            List<ZonedMaterial> duplicatedMaterials = Enumerable
                .Range(0, materials.Count)
                .Select(i => materials[i].Clone())
                .ToList();
            return new Zone(zoning, duplicatedMaterials);
        }
#endif
    }
}
