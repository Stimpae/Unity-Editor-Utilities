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
                Rect fieldRect = new Rect(position.x, position.y, position.width - 70,
                    EditorGUIUtility.singleLineHeight);
                Rect buttonRect = new Rect(position.x + position.width - 65, position.y, 65,
                    EditorGUIUtility.singleLineHeight);

                EditorGUI.PropertyField(fieldRect, property, label);
                if (GUI.Button(buttonRect, "Expand")) {
                    m_expanded = !m_expanded;
                }

                if (!m_expanded) return;
                var boxRect = new Rect() {
                    x = 0,
                    y = position.y + EditorGUIUtility.singleLineHeight,
                    width = (position.width * 2),
                    height = (position.height) + GetInspectorHeight(property)
                };

                EditorGUI.DrawRect(boxRect, new Color(0.18f, 0.18f, 0.18f, 1.0f));
                DrawScriptableInspector(property);
            }
            else {
                Debug.LogWarning($"ScriptableObjectDrawer: {propertyType} is not a ScriptableObject");
            }
            EditorGUI.EndProperty();
        }

        private float GetInspectorHeight(SerializedProperty property) {
            float height = 0;
            
            var foldoutList = new List<string>();
            var serializedObject = new SerializedObject(property.objectReferenceValue);
            var list = PropertyUtility.GetSerializedProperties(serializedObject);
            foreach (var prop in list) {
                if (prop.name == "m_Script") continue;
                var foldoutGroupAttribute = AttributeUtility.GetAttribute<FoldoutGroupAttribute>(prop);
                if (foldoutGroupAttribute != null) {
                    var foldoutGroupName = foldoutGroupAttribute.GroupName;
                    if (foldoutList.Contains(foldoutGroupName)) continue;
                    foldoutList.Add(foldoutGroupName);
                    height += EditorGUI.GetPropertyHeight(prop, true) + 5f;
                    if (m_editor == null || m_editor is not TGAttributesEditor editor) continue;
                    if (editor == null || !editor.FoldoutStates.ContainsKey(foldoutGroupName) || !editor.FoldoutStates[foldoutGroupName].Value) continue;
                    var foldoutGroupProperties = list.Where(p =>
                        AttributeUtility.GetAttribute<FoldoutGroupAttribute>(p) != null &&
                        AttributeUtility.GetAttribute<FoldoutGroupAttribute>(p).GroupName ==
                        foldoutGroupName);

                    height += foldoutGroupProperties.Sum(props => EditorGUI.GetPropertyHeight(props, true) + 10f);
                }
                else {
                    height += EditorGUI.GetPropertyHeight(prop, true);
                }
            }
            return height + 7.5f;
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