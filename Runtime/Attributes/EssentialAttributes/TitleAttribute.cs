using UnityEngine;

namespace TG.Attributes {
    public enum TitleAlignment {
        LEFT,
        CENTER,
        RIGHT
    }
    
    public class TitleAttribute : PropertyAttribute {
        public readonly string title;
        public readonly string subtitle;
        public readonly TitleAlignment alignment;
        public readonly bool showLine;
        public readonly bool bold;
        
        public TitleAttribute(string title, string subtitle = "", TitleAlignment alignment = TitleAlignment.LEFT, bool showLine = false, bool bold = false) {
            this.title = title;
            this.alignment = alignment;
            this.subtitle = subtitle;
            this.showLine = showLine;
            this.bold = bold;
        }
    }
}