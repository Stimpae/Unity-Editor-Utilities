using System;
using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes.Editor {
    [CustomPropertyDrawer(typeof(TitleAttribute))]
    public class TitleDrawer : DecoratorDrawer {
    public override void OnGUI(Rect position) {
        var titleAttribute = (TitleAttribute)attribute;

        var textAnchor = titleAttribute.alignment switch {
            TitleAlignment.LEFT => TextAnchor.UpperLeft,
            TitleAlignment.CENTER => TextAnchor.UpperCenter,
            TitleAlignment.RIGHT => TextAnchor.UpperRight,
            _ => throw new ArgumentOutOfRangeException()
        };

        var titleStyle = new GUIStyle(EditorStyles.boldLabel) {
            alignment = textAnchor,
            fontStyle = titleAttribute.bold ? FontStyle.Bold : FontStyle.Normal,
        };
        
        var yOffset = position.y + 5f;
        EditorGUI.LabelField(new Rect(position.x, yOffset, position.width, EditorGUIUtility.singleLineHeight), titleAttribute.title, titleStyle);
        yOffset += EditorGUIUtility.singleLineHeight;

        if (!string.IsNullOrEmpty(titleAttribute.subtitle)) {
            var subtitleStyle = new GUIStyle(EditorStyles.label) {
                alignment = textAnchor,
                fontStyle = FontStyle.Normal
            };
            EditorGUI.LabelField(new Rect(position.x, yOffset, position.width, EditorGUIUtility.singleLineHeight), titleAttribute.subtitle, subtitleStyle);
            yOffset += EditorGUIUtility.singleLineHeight;
        }

        if (!titleAttribute.showLine) return;
        yOffset += 5; 
        EditorGUI.DrawRect(new Rect(position.x - 20f, yOffset, position.width + 20f, 1), new Color(0.12f, 0.12f, 0.12f, 1.333f));
    }

    public override float GetHeight() {
        var titleAttribute = (TitleAttribute)attribute;
        var height = 5f + EditorGUIUtility.singleLineHeight; 

        if (!string.IsNullOrEmpty(titleAttribute.subtitle)) {
            height += EditorGUIUtility.singleLineHeight;
        }

        if (titleAttribute.showLine) {
            height += 1 + 5; 
        }
        
        height += 5;
        
        return height;
    }
}
}