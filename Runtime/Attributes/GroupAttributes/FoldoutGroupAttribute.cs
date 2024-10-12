using System;
using System.Diagnostics;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class FoldoutGroupAttribute : Attribute {
        public string GroupName { get; private set; }
        public int ColorIndex { get; private set; }
        
        public FoldoutGroupAttribute(string groupName, int colorIndex = 1) {
            this.GroupName = groupName;
            this.ColorIndex = colorIndex;
        }
    }
}