using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes.Editor {
    public static class AttributesGUI {
        public static void PropertyField(SerializedProperty property, bool includeChildren = true,
            bool drawContainer = false) {
            DrawPropertyField(property, includeChildren, drawContainer);
        }
        
        private static void DrawPropertyField(SerializedProperty property, bool includeChildren, bool drawContainer) {
            if (!AttributeUtility.IsVisible(property)) return;
            ValidationUtility.ValidateProperty(property);

            if (drawContainer)
                EditorGUILayout.BeginVertical(AttributeEditorStyles.ContainerChildStyle(new RectOffset(0, 0, 0, 0),
                    new RectOffset(0, 0, 3, 0)));
            using (new EditorGUI.DisabledScope(!AttributeUtility.IsEnabled(property))) {
                EditorGUILayout.PropertyField(property, AttributeUtility.GetLabel(property), includeChildren);
            }

            if (drawContainer) EditorGUILayout.EndVertical();
        }
        
        public static void DrawHelpBox(string message, MessageType messageType, Color backgroundColor, Color textColor) {
            AttributeEditorStyles.DrawColouredHelpBox(() => {
                EditorGUILayout.HelpBox(message, messageType);
            }, backgroundColor, textColor);
        }

        public static void DrawValidatorErrorBox(string errorMessage) {
            AttributeEditorStyles.DrawColouredHelpBox(() => {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(errorMessage, MessageType.Warning);
            }, AttributeEditorStyles.GetColor(10), Color.black);
        }
    }
}

