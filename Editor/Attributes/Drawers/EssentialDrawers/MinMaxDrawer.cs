using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes.Editor {
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var minMax = attribute as MinMaxAttribute;
            if (property.propertyType == SerializedPropertyType.Vector2) {
                var minValue = property.vector2Value.x;
                var maxValue = property.vector2Value.y;
                if (minMax == null) return;
                var minLimit = minMax.MinLimit;
                var maxLimit = minMax.MaxLimit;
                EditorGUI.MinMaxSlider(position, label, ref minValue, ref maxValue, minLimit, maxLimit);
                
                var vec = Vector2.zero;
                vec.x = minValue;
                vec.y = maxValue;

                property.vector2Value = vec;
                position.y += EditorGUIUtility.singleLineHeight;

                GUI.enabled = false;
                float[] values = { minLimit, minValue, maxValue, maxLimit };
                EditorGUI.MultiFloatField(position, new GUIContent(""),
                    new[] {
                        new GUIContent("Min"), new GUIContent("MinValue"), new GUIContent("MaxValue"),
                        new GUIContent("Max")
                    }, values);
                GUI.enabled = true;
            }
            else if (property.propertyType == SerializedPropertyType.Vector2Int) {
                float minValue = property.vector2IntValue.x;
                float maxValue = property.vector2IntValue.y;
                if (minMax == null) return;

                var minLimit = Mathf.RoundToInt(minMax.MinLimit);
                var maxLimit = Mathf.RoundToInt(minMax.MaxLimit);
                EditorGUI.MinMaxSlider(position, label, ref minValue, ref maxValue, minLimit, maxLimit);

                var vec = Vector2Int.zero;
                vec.x = Mathf.RoundToInt(minValue);
                vec.y = Mathf.RoundToInt(maxValue);

                property.vector2IntValue = vec;

                GUI.enabled = false;
                float[] values = { minLimit, minValue, maxValue, maxLimit };
                EditorGUI.MultiFloatField(position, new GUIContent(""),
                    new[] {
                        new GUIContent("Min"), new GUIContent("MinValue"), new GUIContent("MaxValue"),
                        new GUIContent("Max")
                    }, values);
                GUI.enabled = true;
            }
            else {
                EditorGUI.LabelField(position, label.text, "Use MinMax with Vector2 or Vector2Int");
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var size = 0f;
            size += EditorGUIUtility.singleLineHeight * 2;
            return size;
        }
    }
}