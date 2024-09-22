using UnityEditor;
using UnityEngine;

namespace TG.Attributes.Editor {
    [CustomPropertyDrawer(typeof(HolderAttribute))]
    public class HolderDrawer : PropertyDrawer{
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.PropertyField(position, property, GUIContent.none);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return 0;
        }
    }
}