using UnityEditor;
using UnityEngine;

namespace Cobble.Core.UnityEditor {
    
    [CustomPropertyDrawer(typeof(TagSelector))]
    public class TagSelectorPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.propertyType != SerializedPropertyType.String) return;
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
        }
    }
}