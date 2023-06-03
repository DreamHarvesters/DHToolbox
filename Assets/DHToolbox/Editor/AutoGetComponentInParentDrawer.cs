using DHToolbox.Runtime.DHToolboxAssembly.Attributes;
using UnityEditor;
using UnityEngine;

namespace DHToolbox.Editor
{
    [CustomPropertyDrawer(typeof(AutoGetComponentInParentAttribute))]
    public class AutoGetComponentInParentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            UnityEditor.EditorGUI.PropertyField(position, property, label);

            if (property.objectReferenceValue == null)
            {
                var parentComponent = property.serializedObject.targetObject as Component;
                if (parentComponent != null)
                {
                    var componentType = fieldInfo.FieldType;
                    var component = parentComponent.GetComponentInChildren(componentType);
                    property.objectReferenceValue = component;
                }
            }

            EditorGUI.EndProperty();
        }
    }
}