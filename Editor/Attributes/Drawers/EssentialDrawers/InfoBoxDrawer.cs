using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes.Editor {
    [CustomPropertyDrawer(typeof(InfoBoxAttribute))]
    public class InfoBoxDrawer : DecoratorDrawer {
        public override void OnGUI(Rect position) {
            base.OnGUI(position);
            var infoBoxAttribute = (InfoBoxAttribute)attribute;
            
            // set color
            var color = GUI.backgroundColor;
            GUI.backgroundColor = AttributeEditorStyles.GetColor(11);
            EditorGUI.HelpBox(position, infoBoxAttribute.text, (MessageType)infoBoxAttribute.messageType);
            GUI.backgroundColor = color;
        }

        public override float GetHeight() {
            return 45f;
        }
    }
}