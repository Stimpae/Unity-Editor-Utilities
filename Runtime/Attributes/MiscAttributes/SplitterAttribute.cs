using System;
using System.Diagnostics;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class SplitterAttribute : PropertyAttribute {
        public readonly int thickness;
        public readonly int padding;
        public SplitterAttribute(int thickness = 1, int padding = 0) {
            this.thickness = thickness;
            this.padding = padding;
        }
    }
}