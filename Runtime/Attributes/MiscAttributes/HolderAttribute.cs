using System;
using System.Diagnostics;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class HolderAttribute : PropertyAttribute {
        
    }
}