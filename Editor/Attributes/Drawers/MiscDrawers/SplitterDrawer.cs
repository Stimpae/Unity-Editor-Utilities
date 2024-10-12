using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes.Editor {
    [CustomPropertyDrawer(typeof(SplitterAttribute))]
    public class SplitterDrawer : DecoratorDrawer {
        public override void OnGUI(Rect position) {
            var splitterAttribute = (SplitterAttribute) attribute;

            position.xMin -= 20f;
            position.width += 4f;
            
            position.y += splitterAttribute.padding / 2f;
            position.height = splitterAttribute.thickness;
            EditorGUI.DrawRect(position, new Color(0.12f, 0.12f, 0.12f, 1.333f));
        }
        
        public override float GetHeight() {
            var splitterAttribute = (SplitterAttribute) attribute;
            return Mathf.Max(splitterAttribute.padding, splitterAttribute.thickness);
        }
    }
}