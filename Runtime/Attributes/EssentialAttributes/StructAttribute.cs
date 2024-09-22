using System.Diagnostics;
using UnityEngine;

namespace TG.Attributes {
    [System.AttributeUsage(System.AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class StructAttribute : PropertyAttribute {

    }
}