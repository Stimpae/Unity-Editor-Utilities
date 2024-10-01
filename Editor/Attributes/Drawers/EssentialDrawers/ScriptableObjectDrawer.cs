using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TG.Attributes.Editor {
    [CustomPropertyDrawer(typeof(ScriptableObjectAttribute))]
    public class ScriptableObjectDrawer : PropertyDrawer {
        private bool m_expanded;
        private int m_numberOfProperties;

        private UnityEditor.Editor m_editor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            if (property.objectReferenceValue == null) {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var propertyType = PropertyUtility.GetPropertyType(property);
            if (typeof(ScriptableObject).IsAssignableFrom(propertyType)) {
                var serializedObject = new SerializedObject(property.objectReferenceValue);
                var list = PropertyUtility.GetSerializedProperties(serializedObject);
                if (list.Count == 1) {
                    EditorGUI.PropertyField(position, property, label);
                    return;
                }
                
                Debug.Log($"ScriptableObjectDrawer: {propertyType} has {list.Count} properties");
                
                Rect fieldRect = new Rect(position.x, position.y, position.width - 70,
                    EditorGUIUtility.singleLineHeight);
                Rect buttonRect = new Rect(position.x + position.width - 65, position.y, 65,
                    EditorGUIUtility.singleLineHeight);

                EditorGUI.PropertyField(fieldRect, property, label);
                if (GUI.Button(buttonRect, "Expand")) {
                    m_expanded = !m_expanded;
                }

                if (!m_expanded) return;
                DrawScriptableInspector(property);
            }
            else {
                Debug.LogWarning($"ScriptableObjectDrawer: {propertyType} is not a ScriptableObject");
            }
            EditorGUI.EndProperty();
        }
        
        private void DrawScriptableInspector(SerializedProperty property) {
            var scriptableObject = property.objectReferenceValue as ScriptableObject;
            if (scriptableObject == null) {
                return;
            }
            
            m_editor = UnityEditor.Editor.CreateEditor(scriptableObject);
            m_editor.OnInspectorGUI();
            m_editor.serializedObject.ApplyModifiedProperties();
        }
    }
}