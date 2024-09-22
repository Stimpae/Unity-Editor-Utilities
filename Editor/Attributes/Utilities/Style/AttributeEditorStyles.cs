using System;
using UnityEditor;
using UnityEngine;

namespace TG.Attributes.Editor {
    public static class AttributeEditorStyles {
        private static Texture2D _savedHelpBoxBackground;
        static AttributeEditorStyles() {
            GUIStyle helpBoxStyle = GUI.skin.GetStyle("helpbox");
            _savedHelpBoxBackground = helpBoxStyle.normal.background;
        }
        
        public static GUIStyle ContainerStyle(RectOffset padding, bool dark = false) {
            var style = new GUIStyle(GUI.skin.box) { padding = padding };
            if (!dark) {
                style.normal.background = new Texture2D(0, 0);
            }
            return style;
        }
        
        public static GUIStyle ContainerChildStyle(RectOffset padding, RectOffset margin, bool dark = false) {
            var style = new GUIStyle(GUI.skin.box) {
                padding = padding, 
                margin = margin
            };
            if (!dark) {
                style.normal.background = new Texture2D(0, 0);
            }
            return style;
        }
        
        public static GUIStyle FoldoutStyle(bool bold, int fontSize) {
            var style = new GUIStyle(EditorStyles.foldout) {
                overflow = new RectOffset(100, 0, 0, 0),
                padding = new RectOffset(20, 0, 0, 0),
                fontStyle = FontStyle.Bold,
                fontSize = 12
            };
            if (!bold) style.fontStyle = FontStyle.Normal;
            if (fontSize != 0) style.fontSize = fontSize;
            return style;
        }
        
        public static GUIStyle HeaderStyle(bool bold, int fontSize, RectOffset padding) {
            var style = new GUIStyle(EditorStyles.label) {
                padding = padding,
                fontStyle = FontStyle.Bold,
                fontSize = 12
            };
            if (!bold) style.fontStyle = FontStyle.Normal;
            if (fontSize != 0) style.fontSize = fontSize;
            return style;
        }
        
        public static void DrawIdentifierLine(Rect target, Color targetColor, float thickness = 3f) {
            var identifierRect = new Rect {
                x = target.xMin + 5,
                y = target.yMin - 0,
                width = thickness,
                height = target.height + 0,
                xMin = 15f,
                xMax = 18f
            };
            
            EditorGUI.DrawRect(identifierRect, targetColor);
        }
        
        public static void DrawColouredHelpBox(Action action, Color32 backgroundColor, Color32 textColor) {
            GUIStyle redWarningBoxStyle = GUI.skin.GetStyle("helpbox");
            Texture2D redWarningBoxBackgroundTexture = new Texture2D(2, 2);
            redWarningBoxBackgroundTexture.CreateColouredTexture(backgroundColor);
            redWarningBoxStyle.normal.background = redWarningBoxBackgroundTexture;
            redWarningBoxStyle.normal.textColor = textColor;
            redWarningBoxStyle.fontSize = 11;
            redWarningBoxStyle.padding = new RectOffset(8, 8, 8, 2);
            
            action?.Invoke();
            
            redWarningBoxStyle.normal.background = _savedHelpBoxBackground;
            redWarningBoxStyle.normal.textColor = Color.white;
        }
        
        public static void CreateColouredTexture(this Texture2D texture2D, Color32 color) {
            var colorArray = texture2D.GetPixels32();
            for (var i = 0; i < colorArray.Length; ++i) {
                colorArray[i] = color;
            }
            
            texture2D.SetPixels32(colorArray);
            texture2D.Apply();
        }
        
        public static void DrawSplitter(float xMin= 0f) {
            var splitterRect = GUILayoutUtility.GetRect(1f, 1f);
            splitterRect.xMin = xMin;
            splitterRect.width += 4f;
            if (Event.current.type != EventType.Repaint) {
                return;
            }
            EditorGUI.DrawRect(splitterRect, new Color(0.12f, 0.12f, 0.12f, 1.333f));
        }
        
        public static Color GetColor(int index) {
            return index switch {
                0 => new Color32(128, 128, 128, 255),
                1 => new Color32(255, 165, 0, 255),
                2 => new Color32(30, 144, 255, 255),
                3 => new Color32(128, 128, 0, 255),
                4 => new Color32(255, 0, 0, 255),
                5 => new Color32(135, 206, 235, 255),
                6 => new Color32(255, 235, 205, 255),
                7 => new Color32(255, 127, 80, 255),
                8 => new Color32(139, 0, 139, 255),
                9 => new Color32(218, 165, 32, 255),
                10 => new Color32(255, 100, 20, 255),
                11 => new Color32(45, 45, 45, 255),
                _ => new Color32(255, 255, 255, 255)
            };
        }
    }
}