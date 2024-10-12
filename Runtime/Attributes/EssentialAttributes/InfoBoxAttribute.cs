using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class InfoBoxAttribute : PropertyAttribute {
        public readonly string text;
        public readonly MessageType messageType;

        public InfoBoxAttribute(string text, MessageType messageType = MessageType.Info) {
            this.text = text;
            this.messageType = messageType;
        }
    }
}