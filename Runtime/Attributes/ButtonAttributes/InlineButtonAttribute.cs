using System.Diagnostics;
using UnityEngine;

namespace TG.Attributes {
    [System.AttributeUsage(System.AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class InlineButtonAttribute : PropertyAttribute {
        public string ButtonLabel { get; private set;}
        public string MethodName { get; private set; }
        public int Color { get; }

        public InlineButtonAttribute(string buttonLabel, string methodName, int color = 0) {
            ButtonLabel = buttonLabel;
            MethodName = methodName;
            Color = color;
        }
        
    }
}