using System;
using System.Diagnostics;
using UnityEngine;

namespace TG.Attributes {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class ButtonFieldAttribute : PropertyAttribute {
        public string FunctionName { get; }
        public string ButtonLabel { get; }
        public float ButtonHeight { get; }
        public int Color { get; }

        public ButtonFieldAttribute(string functionName, string buttonLabel, float buttonHeight = 25f, int color = 0) {
            this.ButtonLabel = buttonLabel;
            this.ButtonHeight = buttonHeight;
            this.FunctionName = functionName;
            this.Color = color;
        }
        
        
    }
}