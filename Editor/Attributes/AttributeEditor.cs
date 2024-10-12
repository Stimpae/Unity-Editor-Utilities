
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using Object = System.Object;

namespace EditorUtilities.Attributes.Editor {
    
    [CustomEditor(typeof(Object), true)]
    public class AttributeEditor : UnityEditor.Editor {
        private List<SerializedProperty> m_serializedProperties = new List<SerializedProperty>();
        private readonly Dictionary<SerializedProperty, ReorderableList> m_reorderableLists = new Dictionary<SerializedProperty, ReorderableList>();
        private readonly List<SerializedProperty> m_resolvedProperties = new List<SerializedProperty>();
        private IEnumerable<MethodInfo> m_buttonMethods;
        private readonly Dictionary<string, EditorSavedBool> m_foldoutStates = new Dictionary<string, EditorSavedBool>();
        
        public Dictionary<string, EditorSavedBool> FoldoutStates => m_foldoutStates;

        protected virtual void OnEnable() {
            ValidationUtility.ClearFailedValidations();
            m_serializedProperties.Clear();
            m_serializedProperties = PropertyUtility.GetSerializedProperties(serializedObject);
            
            m_buttonMethods = ReflectionUtility.GetMethods(target, methodInfo
                => methodInfo.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0);
            
            InitializeReorderableLists();
        }
        
        protected virtual void OnDisable() {
            ValidationUtility.ClearFailedValidations();
        }
        
        private void InitializeReorderableLists() {
            foreach (var property in m_serializedProperties.Where(p => p.isArray && AttributeUtility.GetAttribute<ListViewAttribute>(p) != null)) {
                m_reorderableLists.Add(property, CreateReorderableList(property));
            }
        }

        private ReorderableList CreateReorderableList(SerializedProperty property) {
            var reorderableList = new ReorderableList(serializedObject, property, true, true, true, true);
            reorderableList.drawElementCallback = (rect, index, isActive, isFocused) => DrawElement(reorderableList, rect, index);
            reorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, property.displayName);
            return reorderableList;
        }

        private void DrawElement(ReorderableList reorderableList, Rect rect, int index) {
            reorderableList.elementHeight = EditorGUI.GetPropertyHeight(reorderableList.serializedProperty.GetArrayElementAtIndex(index));
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, rect.height), element, GUIContent.none);
        }
        
        public override void OnInspectorGUI() {
            serializedObject.Update();
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawProperties() {
            foreach (var property in m_serializedProperties) {
                CheckValidation(property);
            }
            
            GUILayout.Space(5);
            
            foreach (var property in m_serializedProperties) {
                if (property == null) continue;
                if (property.name == "m_Script") continue;
                if (m_resolvedProperties.Contains(property)) continue;
                
                var hasAttribute = HandleMetaAttributeChecks(property);
                if (hasAttribute) continue;
                
                // Default drawers if no custom attributes
                AttributesGUI.PropertyField(property, true, false);
            }
            
            DrawButtons();
            
            GUILayout.Space(5);
        }

        protected virtual void CheckValidation(SerializedProperty property) {
            var key = property.name + "-" + serializedObject.targetObject.GetInstanceID();
            var failedProperty = ValidationUtility.GetFailedValidation(key);
            if (failedProperty == null) return;
            AttributesGUI.DrawValidatorErrorBox($"Property {failedProperty?.name} has failed validation");
        }
        
        private bool HandleMetaAttributeChecks(SerializedProperty property) {
            // test each attribute type
            var boxGroupAttribute = AttributeUtility.GetAttribute<BoxGroupAttribute>(property);
            if(boxGroupAttribute != null) {
                DrawBoxGroup(boxGroupAttribute.GroupName);
                return true;
            }
            
            var foldoutGroupAttribute = AttributeUtility.GetAttribute<FoldoutGroupAttribute>(property);
            if(foldoutGroupAttribute != null) {
                DrawFoldoutGroup(foldoutGroupAttribute.GroupName);
                return true;
            }
            
            var listAttribute = AttributeUtility.GetAttribute<ListViewAttribute>(property);
            if(listAttribute != null) {
                DrawListView(property);
                return true;
            }
            
            return false;
        }
        
        protected virtual void DrawButtons() {
            foreach (var method in m_buttonMethods) {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
                if (buttonAttribute == null) continue;
                
                GUI.backgroundColor = AttributeEditorStyles.GetColor(buttonAttribute.Color);
                if (!GUILayout.Button(ObjectNames.NicifyVariableName(method.Name))) continue;
                GUI.backgroundColor = Color.white;
                
                method.Invoke(target, method.GetParameters().Select(p => p.DefaultValue).ToArray());
                EditorUtility.SetDirty(target);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
        
        protected virtual void DrawBoxGroup(string groupName) {
            EditorGUILayout.BeginVertical(AttributeEditorStyles.ContainerStyle(new RectOffset(5, 7, 0, 0), true));
            Rect verticalGroup = EditorGUILayout.BeginVertical();
            var serializedProperties = m_serializedProperties.Where(p =>
                AttributeUtility.GetAttribute<BoxGroupAttribute>(p)?.GroupName == groupName);
            
            // add all properties to resolved list except the first one
            var properties = serializedProperties.ToList();
            m_resolvedProperties.AddRange(properties.Skip(1));

            var boxGroupAttribute = AttributeUtility.GetAttribute<BoxGroupAttribute>(properties.First());
            var color = AttributeEditorStyles.GetColor(boxGroupAttribute.ColorIndex);
            AttributeEditorStyles.DrawIdentifierLine(verticalGroup, color);

            EditorGUILayout.LabelField(groupName,
                AttributeEditorStyles.HeaderStyle(true, 12, new RectOffset(-2, 0, 0, 0)));
            
            foreach (var property in properties) {
                AttributesGUI.PropertyField(property,true, true);
                if (properties.Last() == property) GUILayout.Space(5);
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawFoldoutGroup(string groupName) {
            if (!m_foldoutStates.ContainsKey(groupName)) {
                m_foldoutStates[groupName] = new EditorSavedBool($"{target.GetInstanceID()}.{groupName}", false);
            }

            EditorGUILayout.BeginVertical(AttributeEditorStyles.ContainerStyle(new RectOffset(15, 7, 0, 0)));
            Rect verticalGroup = EditorGUILayout.BeginVertical();
            var serializedProperties = m_serializedProperties.Where(p =>
                AttributeUtility.GetAttribute<FoldoutGroupAttribute>(p)?.GroupName == groupName);
            
            // add all properties to resolved list except the first one
            var enumerable = serializedProperties.ToList();
            var properties = enumerable.ToList();
            m_resolvedProperties.AddRange(properties.Skip(1));
            
            var foldoutGroupAttribute = AttributeUtility.GetAttribute<FoldoutGroupAttribute>(enumerable.First());
            var color = AttributeEditorStyles.GetColor(foldoutGroupAttribute.ColorIndex);
            AttributeEditorStyles.DrawIdentifierLine(verticalGroup, color);
            
            m_foldoutStates[groupName].Value = EditorGUILayout.Foldout(m_foldoutStates[groupName].Value, groupName, AttributeEditorStyles.FoldoutStyle(true, 12));
            if (m_foldoutStates[groupName].Value) {
                foreach (var property in enumerable) {
                    AttributesGUI.PropertyField(property,true, true);
                    if (enumerable.Last() == property) GUILayout.Space(5);
                }
            }
                
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawListView(SerializedProperty listProperty) {
            if(listProperty.propertyType != SerializedPropertyType.Generic) return;
            if (!listProperty.isArray) {
                Debug.LogWarning("Property is not an array"); return;
            }
            var reorderableList = m_reorderableLists[listProperty];
            reorderableList?.DoLayoutList();
        }
    }
}