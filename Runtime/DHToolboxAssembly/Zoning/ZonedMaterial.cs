using System;
using System.Collections.Generic;
using System.Linq;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;
using UnityEngine.Rendering;

namespace TemplateAssets.Scripts.Zoning
{
    [Serializable]
    public class ZonedMaterial
    {
#if ODIN_INSPECTOR
        [OnValueChanged("UpdatePropertyList")]
#endif
        [SerializeField]
        private Material material;

#if ODIN_INSPECTOR
        [Title("Zoned Elements", Bold = true, TitleAlignment = TitleAlignments.Centered)]
        [GUIColor(0, 1, 0)]
        [ListDrawerSettings(HideAddButton = true, DraggableItems = false)]
#endif
        [SerializeField]
        private List<ZonedProperty> zonedProperties;

#if UNITY_EDITOR
        [Serializable]
        public class DrawableMaterialProperty
        {
            private ZonedMaterial material;

            [HideInInspector]
            public string name;

            [HideInInspector]
            public ShaderPropertyType propertyType;

#if ODIN_INSPECTOR
            [ShowIf("@this.propertyType == ShaderPropertyType.Color")]
            [LabelText("$name")]
            [ReadOnly]
#endif
            public Color color;

#if ODIN_INSPECTOR
            [ShowIf("@this.propertyType == ShaderPropertyType.Texture")]
            [ReadOnly]
            [LabelText("$name")]
            [PreviewField]
#endif
            public Texture texture;

            public DrawableMaterialProperty(ZonedMaterial material)
            {
                this.material = material;
            }

#if ODIN_INSPECTOR
            [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
#endif
            public void Zone()
            {
                material.AddZonedProperty(name, propertyType, color, texture);
                material.propertyList.Remove(this);
            }
        }

        [Space]
#if ODIN_INSPECTOR
        [Title(
            "Possible Properties To Be Zoned",
            Bold = true,
            TitleAlignment = TitleAlignments.Centered
        )]
        [GUIColor(0, 1, 1)]
        [InfoBox(
            "This is the list of properties in the selected material. Press Zone to enable zoning for a particular property"
        )]
        [ListDrawerSettings(IsReadOnly = true)]
#endif
        public List<DrawableMaterialProperty> propertyList;

#if ODIN_INSPECTOR
        [Button]
#endif
        private void UpdatePropertyList()
        {
            Shader shader = material.shader;

            int propertyCount = shader.GetPropertyCount();

            propertyList = new List<DrawableMaterialProperty>(propertyCount);

            for (int i = 0; i < propertyCount; i++)
            {
                string propertyName = shader.GetPropertyName(i);
                ShaderPropertyType propertyType = shader.GetPropertyType(i);

                if (
                    propertyType == ShaderPropertyType.Color
                    || propertyType == ShaderPropertyType.Texture
                )
                {
                    DrawableMaterialProperty property = new DrawableMaterialProperty(this)
                    {
                        name = propertyName,
                        propertyType = shader.GetPropertyType(i),
                    };

                    if (propertyType == ShaderPropertyType.Color)
                        property.color = material.GetColor(propertyName);
                    else if (propertyType == ShaderPropertyType.Texture)
                        property.texture = material.GetTexture(propertyName);

                    propertyList.Add(property);
                }
            }
        }

        public void Init()
        {
            UpdatePropertyList();

            foreach (var zonedProperty in zonedProperties)
            {
                zonedProperty.onRemoved = (deletedZoneProperty) =>
                {
                    zonedProperties.Remove(deletedZoneProperty);

                    UpdatePropertyList();
                };

                DrawableMaterialProperty property = propertyList.Find(
                    materialProperty => materialProperty.name == zonedProperty.Name
                );
                propertyList.Remove(property);
            }
        }
#endif

        public ZonedMaterial(Material material)
        {
            this.material = material;

            this.zonedProperties = new List<ZonedProperty>();

#if UNITY_EDITOR
            UpdatePropertyList();
#endif
        }

#if UNITY_EDITOR
        public void AddZonedProperty(
            string name,
            ShaderPropertyType propertyType,
            Color currentColor,
            Texture texture
        )
        {
            zonedProperties.Add(
                new ZonedProperty(
                    name,
                    propertyType,
                    currentColor,
                    texture,
                    (property) =>
                    {
                        zonedProperties.Remove(property);
                        UpdatePropertyList();
                    }
                )
            );
        }
#endif

        public void Apply()
        {
            foreach (ZonedProperty zonedProperty in zonedProperties)
            {
                zonedProperty.Apply(material);
            }
        }

#if UNITY_EDITOR
        public ZonedMaterial Clone()
        {
            List<ZonedProperty> cloneProps = Enumerable
                .Range(0, zonedProperties.Count)
                .Select(i => zonedProperties[i].Clone())
                .ToList();
            ZonedMaterial zm = new ZonedMaterial(material);
            zm.zonedProperties = cloneProps;
            zm.Init();
            return zm;
        }
#endif
    }
}
