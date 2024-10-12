using System;
using System.Diagnostics;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [System.AttributeUsage(System.AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class LabelAttribute : Attribute{
        public string label;
        public bool bold;
        public LabelAttribute(string label = "", bool bold = false){
            this.label = label;
            this.bold = bold;
        }
    }
}