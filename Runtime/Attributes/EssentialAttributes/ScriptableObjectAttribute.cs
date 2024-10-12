using System.Diagnostics;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [System.AttributeUsage(System.AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class ScriptableObjectAttribute : PropertyAttribute {
        
    }
}