using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
#if ODIN_INSPECTOR
#endif

namespace DHToolbox.Runtime.DHToolboxAssembly.Zoning
{
    [Serializable]
    public class ZonedProperty
    {
        [SerializeField]
        [HideInInspector]
        private string name;

        [SerializeField]
        [HideInInspector]
        private ShaderPropertyType propertyType;

#if ODIN_INSPECTOR
        [ShowIf("@this.propertyType == ShaderPropertyType.Color")]
        [LabelText("$name")]
#endif
        [SerializeField]
        private Color color;

#if ODIN_INSPECTOR
        [ShowIf("@this.propertyType == ShaderPropertyType.Texture")]
        [LabelText("$name")]
#endif
        [SerializeField]
        private Texture texture;

        public string Name => name;

        public Action<ZonedProperty> onRemoved;

        public ZonedProperty(
            string name,
            ShaderPropertyType propertyType,
            Color color,
            Texture texture,
            Action<ZonedProperty> onRemoved
        )
        {
            this.name = name;
            this.propertyType = propertyType;
            this.color = color;
            this.texture = texture;
            this.onRemoved = onRemoved;
        }

        public void Apply(Material material)
        {
            if (propertyType == ShaderPropertyType.Color)
                material.SetColor(name, color);
            else if (propertyType == ShaderPropertyType.Texture)
                material.SetTexture(name, texture);
        }

        public ZonedProperty Clone()
        {
            return new ZonedProperty(name, propertyType, color, texture, onRemoved);
        }

#if UNITY_EDITOR

#if ODIN_INSPECTOR
        [Button, GUIColor(1, 0, 0)]
#endif
        public void Remove()
        {
            onRemoved?.Invoke(this);
        }
#endif
    }
}
