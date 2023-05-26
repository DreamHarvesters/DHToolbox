using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace TemplateAssets.Scripts.Zoning
{
    [Serializable]
    public class ZonedProperty
    {
        [SerializeField] [HideInInspector] private string name;

        [SerializeField] [HideInInspector] private ShaderPropertyType propertyType;

        [ShowIf("@this.propertyType == ShaderPropertyType.Color")] [LabelText("$name")] [SerializeField]
        private Color color;

        [ShowIf("@this.propertyType == ShaderPropertyType.Texture")] [LabelText("$name")] [SerializeField]
        private Texture texture;

        public string Name => name;

        public Action<ZonedProperty> onRemoved;

        public ZonedProperty(string name, ShaderPropertyType propertyType, Color color, Texture texture,
            Action<ZonedProperty> onRemoved)
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
        [Button, GUIColor(1, 0, 0)]
        public void Remove()
        {
            onRemoved?.Invoke(this);
        }
#endif
    }
}