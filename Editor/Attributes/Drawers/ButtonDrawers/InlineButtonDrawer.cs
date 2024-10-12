using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes.Editor {
    [CustomPropertyDrawer(typeof(InlineButtonAttribute))]
    public class InlineButtonDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            InlineButtonAttribute inlineButtonAttribute = (InlineButtonAttribute)attribute;
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width - 155, position.height), property, label);

            GUI.backgroundColor = AttributeEditorStyles.GetColor(inlineButtonAttribute.Color);
            if (GUI.Button(new Rect(position.x + position.width - 150, position.y, 150, position.height), inlineButtonAttribute.ButtonLabel)) {
                property.serializedObject.targetObject.GetType().GetMethod(inlineButtonAttribute.MethodName)?.Invoke(property.serializedObject.targetObject, null);
            }
            GUI.backgroundColor = Color.white;
        }
    }
}