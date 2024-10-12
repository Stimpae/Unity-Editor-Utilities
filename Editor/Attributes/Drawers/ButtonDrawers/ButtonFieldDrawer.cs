using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes.Editor {
    [CustomPropertyDrawer(typeof(ButtonFieldAttribute))]
    public class ButtonFieldDrawer : PropertyDrawer{
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            ButtonFieldAttribute buttonFieldAttribute = (ButtonFieldAttribute) attribute;
            
            // set the button height 
            position.height = buttonFieldAttribute.ButtonHeight;
            
            GUI.backgroundColor = AttributeEditorStyles.GetColor(buttonFieldAttribute.Color);
            if (GUI.Button(position, buttonFieldAttribute.ButtonLabel)) {
                property.serializedObject.targetObject.GetType().GetMethod(buttonFieldAttribute.FunctionName)?.Invoke(property.serializedObject.targetObject, null);
            }
            GUI.backgroundColor = Color.white;
            
            if (property.propertyType == SerializedPropertyType.Generic) return;
            position.y += buttonFieldAttribute.ButtonHeight + 5;
            EditorGUI.PropertyField(position, property, label);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            ButtonFieldAttribute buttonFieldAttribute = (ButtonFieldAttribute) attribute;
            
            float height = buttonFieldAttribute.ButtonHeight;
            if (property.propertyType == SerializedPropertyType.Generic) return height;
            return height + 6.5f + EditorGUIUtility.singleLineHeight;
        }
    }
}