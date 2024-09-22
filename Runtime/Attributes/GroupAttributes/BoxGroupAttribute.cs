using System;
using System.Diagnostics;
using UnityEngine;

namespace TG.Attributes {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class BoxGroupAttribute : Attribute {
        public string GroupName { get; private set; }
        public int ColorIndex { get; private set; }

        public BoxGroupAttribute(string groupName, int colorIndex = 1) {
            this.GroupName = groupName;
            this.ColorIndex = colorIndex;
        }
    }
}