using System.Diagnostics;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [Conditional("UNITY_EDITOR")]
    public class ListViewAttribute : PropertyAttribute {
        
    }
}