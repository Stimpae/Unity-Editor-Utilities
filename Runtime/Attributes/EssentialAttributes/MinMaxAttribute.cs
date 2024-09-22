using System.Diagnostics;
using UnityEngine;

namespace TG.Attributes {
    [System.AttributeUsage(System.AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class MinMaxAttribute : PropertyAttribute {
        public float MinLimit { get; }
        public float MaxLimit { get; }
        public MinMaxAttribute(float minLimit, float maxLimit) {
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }
    }
}