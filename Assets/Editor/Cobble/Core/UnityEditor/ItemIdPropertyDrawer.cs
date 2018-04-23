using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Cobble.Core.Lib.Items;
using UnityEditor;
using UnityEngine;

namespace Cobble.Core.UnityEditor {
    
    [CustomPropertyDrawer(typeof(ItemIdSelectorAttribute))]
    public class ItemIdPropertyDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.propertyType != SerializedPropertyType.String) return;
            var optionValues = _getOptionValues();
            var optionNames = _getOptionNames();
            var propertyString = property.stringValue;
            var selectedIndex = Mathf.Clamp(ArrayUtility.IndexOf(optionValues, propertyString), 0, optionValues.Length);
            var index = EditorGUI.Popup(position, label, selectedIndex, optionNames);
            property.stringValue = index < optionValues.Length ? optionValues[index] : "";
        }

        private static IEnumerable<Item> _getItems() {
            return Resources.LoadAll<Item>("Items");
        }

        private static string[] _getOptionValues() {
            var valueArray = _getItems().Select(item => item.ItemId).ToArray();
            ArrayUtility.Insert(ref valueArray, 0, "");
            return valueArray;
        }
        
        private static GUIContent[] _getOptionNames() {
            var nameArray = _getItems().Select(item => new GUIContent(Regex.Replace(item.Name, "<.*?>", string.Empty))).ToArray();
            ArrayUtility.Insert(ref nameArray, 0, new GUIContent("Unknown"));
            return nameArray;
        }
    }
}