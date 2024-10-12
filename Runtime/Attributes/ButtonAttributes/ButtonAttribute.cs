using System;
using System.Diagnostics;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class ButtonAttribute : PropertyAttribute {
        public string ButtonText { get; }
        public int Color { get; }
        
        public ButtonAttribute(string buttonText = null, int color = 0) {
            Color = color;
            this.ButtonText = buttonText;
        }
    }
}
